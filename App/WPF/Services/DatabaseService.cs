using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Models;
using MySql.Data.MySqlClient;

namespace WPF.Services
{
    public class DatabaseService
    {
        private MySqlConnection connection;
        private string connectionString = "Server=localhost;Database=Skyndar;User ID=root;Password=;";
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
        public List<User> GetAllUsers()

        {
            List<User> users = new List<User>();

            string query = "SELECT username, email FROM Users";

            OpenConnection();

            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
              users.Add(new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetDateTime(3)));
                ;
            }
            reader.Close();
            return users;
        }
        public bool GetAdmin(string username, string password)

        {
            string query = "select * from users where username = @username and password =@password";
            OpenConnection();
           
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);
            if ( reader.Read())
            {
                return true;  
            }
            return false;
            
        }
        public List<Prestation> GetPrestations()
        {
            string query = "SELECT * FROM Prestations";
            OpenConnection();
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            List<Prestation> prestations = new List<Prestation>();

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
            return prestations;
        }
        
    }
}
