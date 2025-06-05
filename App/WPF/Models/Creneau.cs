using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WPF.Models
{
   public class Creneau(int id, TimeSpan heureDebut, TimeSpan heureFin, bool cabinet, int dayId, int prestationId)
    {
        public int Id { get; set; } = id;
        public TimeSpan HeureDebut { get; set; } = heureDebut;
        public TimeSpan HeureFin { get; set; } = heureFin;

        public int DayId { get; set; } = dayId; // Foreign key to CalendarDay

        public bool Cabinet { get; set; } = cabinet;
        public int PrestationId { get; set; }= prestationId; // Foreign key to Prestation  


    }
}
