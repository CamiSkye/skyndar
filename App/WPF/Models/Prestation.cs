using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Models
{
    public class Prestation(int id, string titre, int duree, string description, double tarif)
    {
        public int Id { get; set; } = id;
        public string Titre { get; set; } = titre;
        public int Duree { get; set; } = duree; // Duree in minutes
        public string Description { get; set; } = description;
        public double Tarif { get; set; } = tarif; // Tarif in euros

    }
}
