using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WPF.Models
{
   public class Creneau
    {
        public int Id { get; set; }
        public TimeOnly HeureDebut { get; set; }
        public TimeOnly HeureFin { get; set; }

        public DateTime Date { get; set; }
       
        public bool Cabinet { get; set; } = true;
        public int PrestationId { get; set; }   
        public Prestation Prestation { get; set; }
        public Creneau(int id, TimeOnly heureDebut, TimeOnly heureFin, DateTime date,  bool cabinet, Prestation prestation, int prestationId)
        {
            Id = id;
            HeureDebut = heureDebut;
            HeureFin = heureFin;
            Cabinet = cabinet;
            Prestation = prestation;
            PrestationId = prestationId;
            Date = date;
           
        }
    }
}
