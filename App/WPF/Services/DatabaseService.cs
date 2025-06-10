using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Models;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;

namespace WPF.Services
{
    public class DatabaseService
    {
        private readonly MySqlConnection connection;
        private readonly string connectionString = "Server=localhost;Database=Skyndar;User ID=root;Password=;";
        public DatabaseService()
        {
            connection = new MySqlConnection(connectionString);
        }
        public void OpenConnection()
        {
            try
            {
                connection.Open();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

        }
        public void CloseConnection()
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        public List<User> GetAllUsers()

        {
            List<User> users = [];

            string query = "SELECT username, email FROM Users";

            OpenConnection();

            MySqlCommand cmd = new(query, connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                users.Add(new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetDateTime(3)));
                ;
            }
            reader.Close();
            CloseConnection();
            return users;

        }
        public bool GetAdmin(string username, string password)

        {
            string query = "select * from users where username = @username and password =@password";
            OpenConnection();

            MySqlCommand cmd = new(query, connection);
            MySqlDataReader reader = cmd.ExecuteReader();

            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);
            if (reader.Read())
            {
                return true;
            }
            return false;

        }
        public ObservableCollection<Prestation> GetPrestations()
        {
            string query = "SELECT * FROM prestation";
            OpenConnection();
            MySqlCommand cmd = new(query, connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            ObservableCollection<Prestation> prestations = [];

            while (reader.Read())
            {
                prestations.Add(
                    new Prestation(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetInt32(2),
                    reader.GetString(3),
                    reader.GetDouble(4)

                    ));
            }
            CloseConnection();
            return prestations;
        }

        public void AddPrestation(Prestation prestation)
        {
            string query = "INSERT INTO prestation (Titre, Duree, Description, Tarif) VALUES (@Titre, @Duree, @Description, @Tarif)";
            OpenConnection();
            MySqlCommand cmd = new(query, connection);
            cmd.Parameters.AddWithValue("@Titre", prestation.Titre);
            cmd.Parameters.AddWithValue("@Duree", prestation.Duree);
            cmd.Parameters.AddWithValue("@Description", prestation.Description);
            cmd.Parameters.AddWithValue("@Tarif", prestation.Tarif);
            cmd.ExecuteNonQuery();
            CloseConnection();
        }
        public int AddCreneau(Creneau creneau)
        {
            string query = "INSERT INTO creneau( day_id, prestation_id,starthour, endhour, cabinet ) VALUES (@DayId, @PrestationId, @HeureDebut, @HeureFin,@Cabinet)";
            OpenConnection();
            MySqlCommand cmd = new(query, connection);

            cmd.Parameters.AddWithValue("@HeureDebut", creneau.HeureDebut);
            cmd.Parameters.AddWithValue("@HeureFin", creneau.HeureFin);
            cmd.Parameters.AddWithValue("@Cabinet", creneau.Cabinet);
            cmd.Parameters.AddWithValue("@DayId", creneau.DayId);
            cmd.Parameters.AddWithValue("@PrestationId", creneau.PrestationId);
            cmd.ExecuteNonQuery();
            CloseConnection();
            return  (int)cmd.LastInsertedId; // Get the last inserted ID
        }
        public ObservableCollection<Creneau> GetCreneauxForPrestation(
     int prestationId,
     DateTime startDate,  // ex. premier lundi
     DateTime endDate)    // ex. dimanche suivant
        {
            ObservableCollection<Creneau> creneaux = [];
            const string query = @"
        SELECT cr.`id`, cr.`day_id`, cr.`prestation_id`,
               cr.`starthour`, cr.`endhour`, cr.`cabinet`
        FROM `creneau` AS cr
        INNER JOIN `calendarday` AS cd
            ON cr.`day_id` = cd.`id`
        WHERE cr.`prestation_id` = @PrestationId
          AND cd.`date` BETWEEN @StartDate AND @EndDate
        ORDER BY cd.`date`, cr.`starthour`
    ";

            OpenConnection();
            MySqlCommand cmd = new (query, connection);
                
                    cmd.Parameters.AddWithValue("@PrestationId", prestationId);
                    cmd.Parameters.AddWithValue("@StartDate", startDate.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@EndDate", endDate.ToString("yyyy-MM-dd"));

                    MySqlDataReader reader = cmd.ExecuteReader();
                    
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("id");
                            int dayId = reader.GetInt32("day_id");
                            int prestation_Id = reader.GetInt32("prestation_id");
                            TimeSpan startHour = reader.GetTimeSpan("starthour");
                            TimeSpan endHour = reader.GetTimeSpan("endhour");
                            bool cabinet = reader.GetBoolean("cabinet");

                            var cr = new Creneau(id, dayId, prestation_Id, startHour, endHour, cabinet);
                            creneaux.Add(cr);
                        }
            CloseConnection();
            return creneaux;

        }






        public int GetOrInsertId(CalendarDay day)
        {
            int id = 0;

            // First, try to retrieve existing ID
            OpenConnection();
            string selectQuery = "SELECT `id` FROM `calendarday` WHERE `date` = @Date;";
            MySqlCommand selectCmd = new(selectQuery, connection);
            
             selectCmd.Parameters.AddWithValue("@Date", day.Date);
                object result = selectCmd.ExecuteScalar();
                if (result != null)
                {
                    id = Convert.ToInt32(result);
                }
            
            CloseConnection();

            // Return if found
            if (id != 0)
                return id;

            // Insert new record if not found
            OpenConnection();
            string insertQuery = "INSERT INTO calendarday (date, daynumber, isvalid) VALUES (@Date, @DayNumber, @IsValid);";
            MySqlCommand insertCmd = new(insertQuery, connection);
            insertCmd.Parameters.AddWithValue("@Date", day.Date);
            insertCmd.Parameters.AddWithValue("@DayNumber", day.DayNumber);
            insertCmd.Parameters.AddWithValue("@IsValid", day.IsValid);
            insertCmd.ExecuteNonQuery();
            id = (int)insertCmd.LastInsertedId;
            
            CloseConnection();

            return id;
        }

        public ObservableCollection<CalendarDay> GetDayInWeeks(DateTime date )
        {
            string query = "SELECT * FROM calendarday where date >= @date  LIMIT 7 ";
            OpenConnection();
            ObservableCollection<CalendarDay> days = [];
            MySqlCommand cmd = new(query, connection);
            cmd.Parameters.AddWithValue("@date", date.Date);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                CalendarDay day = new (
                    reader.GetInt32(0),
                    reader.GetDateTime(1),
                    reader.GetInt32(2),
                    reader.GetBoolean(3)
                );
                days.Add(day);
            }
            CloseConnection();
            return days;
            
        }

        public ObservableCollection<RendezVous> GetRendezVous()
        {
            ObservableCollection<RendezVous> historique = [];

            string query = "SELECT u.id, u.username, u.email,u.created_at, p.titre, p.tarif,cr.* " +
                "FROM rendezvous r " +
                "JOIN user u ON r.user_id = u.id " +
                "JOIN creneau cr ON r.creneau_id = cr.id " +
                "JOIN prestation p ON p.id = cr.prestation_id ";

            OpenConnection();

            MySqlCommand cmd = new(query, connection);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
               User user = new 
                (
                    reader.GetInt32("id"),
                    reader.GetString("username"),
                    reader.GetString("email"),
                    reader.GetDateTime("created_at")
                );

                Creneau creneau = new 
                (
                   reader.GetInt32("id"),
                   reader.GetInt32("day_id"),
                   reader.GetInt32("prestation_id"),
                   reader.GetTimeSpan("starthour"),
                   reader.GetTimeSpan("endhour"),
                   reader.GetBoolean("cabinet")

                );

              RendezVous rendezvous = new (user, creneau, user.Id, creneau.Id);
                historique.Add(rendezvous);
            }
            CloseConnection();
            return historique;
        }
    }
}
