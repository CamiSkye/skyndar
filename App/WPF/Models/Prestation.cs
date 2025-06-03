using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace WPF.Models
{
    public class Prestation(int id, string titre, int duree, string description, double tarif)
    {
<<<<<<< HEAD
        public int Id { get; set; } = id;
        public string Titre { get; set; } = titre;
        public int Duree { get; set; } = duree; // Duree in minutes
        public string Description { get; set; } = description;
        public double Tarif { get; set; } = tarif; // Tarif in euros

=======
        public int Id { get; set; }
        public string Titre { get; set; }
        public int Duree { get; set; }
        public string Description { get; set; }
        public double Tarif { get; set; }

        public Prestation(int id, string titre, int duree, string description, double tarif)
        {
            Id = id;
            Titre = titre;
            Description = description;
            Tarif = tarif;
            Duree = duree;
        }
>>>>>>> 2fb65b1488a76745b63e7af96b814ae9411851b2
    }
}
       