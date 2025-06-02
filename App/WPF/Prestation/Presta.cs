using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace TestSkyndar
{
    class Presta
    {

        public int id { get; set; }
        public string Titre { get; set; }
        public string Duree { get; set; }
        public string Tarif { get; set; }
        public string Descriptif { get; set; }

        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\jpegl\source\repos\TestSkyndar\TestSkyndar\Bdd.mdf;Integrated Security=True");

        public List<Presta> prestations()
        {
            List<Presta> liste = new List<Presta>();

            if(connect.State != System.Data.ConnectionState.Open)
            {
                try
                {
                    connect.Open();

                    string select = "SELECT * FROM prestation";
                    using(SqlCommand cmd = new SqlCommand(select, connect))
                    {
                        SqlDataReader lire = cmd.ExecuteReader();

                        while(lire.Read())
                        {
                            Presta ed = new Presta();
                            ed.id = (int)lire["id"];
                            ed.Titre = lire["titre"].ToString();
                            ed.Duree = lire["duree"].ToString();
                            ed.Tarif = lire["tarif"].ToString();
                            ed.Descriptif = lire["descriptif"].ToString();

                            liste.Add(ed);
                        }
                    }
                }catch(Exception ex)
                {
                    Console.WriteLine("Erreur " + ex);
                }
                finally
                {
                    connect.Close();
                }
            }

            return liste;
        }
    }
}
