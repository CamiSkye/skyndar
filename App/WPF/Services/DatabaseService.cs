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
        public void DeleteCreneauByPrestationId(int prestationId)
        {
            string query = "DELETE FROM creneau WHERE prestation_id = @PrestationId";
            OpenConnection();
            MySqlCommand cmd = new(query, connection);
            cmd.Parameters.AddWithValue("@PrestationId", prestationId);
            cmd.ExecuteNonQuery();
            CloseConnection();
        }
        public void InsertDay(CalendarDay day)
        {
            string query = "INSERT INTO calendarday (date, daynumber, isvalid) VALUES (@Date, @DayNumber, @IsValid)";
            OpenConnection();
            MySqlCommand cmd = new(query, connection);
            cmd.Parameters.AddWithValue("@Date", day.Date);
            cmd.Parameters.AddWithValue("@DayNumber", day.DayNumber);
            cmd.Parameters.AddWithValue("@IsValid", day.IsValid);
            cmd.ExecuteNonQuery();
            CloseConnection();
        }
        public ObservableCollection<CalendarDay> GetDayInWeeks()
        {
            string query = "SELECT * FROM calendarday ORDER BY date ASC  LIMIT 7 ";
            OpenConnection();
            ObservableCollection<CalendarDay> days = [];
            MySqlCommand cmd = new(query, connection);
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
    }
}
