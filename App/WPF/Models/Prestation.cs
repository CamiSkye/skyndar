using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Models
{
    public class Prestation
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public int Duree { get; set; }
        public string Description { get; set; }
        public double Tarif { get; set; }
        
        public Prestation(int id, string titre,int duree, string description, double tarif)
        {
            Id = id;                                                                                
            Titre = titre;
            Description = description;
            Tarif = tarif;
            Duree = duree;
            
        }
       

    }
}
