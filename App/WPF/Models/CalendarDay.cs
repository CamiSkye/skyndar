using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Models
{
    public class CalendarDay
    {
        public DateTime Date { get; set; }
        public int DayNumber { get; set; }
        public bool IsValid { get; set; } = true;
        public ObservableCollection<Creneau> DayCreneaux { get; set; }
        public CalendarDay(DateTime date, int daynumber, bool isvalid = true)
        {
            
            Date = date;
            DayNumber = daynumber;
            IsValid = isvalid;
            DayCreneaux = new ObservableCollection<Creneau>();
        }
    }
}
