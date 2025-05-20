using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF.Models;

namespace WPF.ViewModels
{
    public class DisponibiliteVM : MainVM
    {
        private DateTime currentMonth = DateTime.Today;
        public string CurrentMonth => currentMonth.ToString("MMMM yyyy", CultureInfo.GetCultureInfo("fr-FR"));
        
        
       
        public ObservableCollection<CalendarDay> Days { get; set; }
        public ObservableCollection<CalendarDay> DaysInWeeks { get; set; }
        public ObservableCollection<Creneau> Creneaux { get; set; }
        public ObservableCollection<Prestation> Prestations { get; set; } 
        public ICommand PreviousMonthCommand { get; set; }
        public ICommand NextMonthCommand { get; set; }
        public ICommand SelectDayCommand { get; set; }
        public ICommand AjouterCreneauCommand { get; set; }

        public void PreviousMonthAction()
        {
            currentMonth = currentMonth.AddMonths(-1);
            OnPropertyChanged(nameof(CurrentMonth));
            LoadCalendar();
        }
        public void NextMonthAction()
        {
            currentMonth = currentMonth.AddMonths(1);
            OnPropertyChanged(nameof(CurrentMonth));
            LoadCalendar();
        }
        public void SelectDayAction()
        {
            // Code pour sélectionner un jour
        }
        public void AjouterCreneauAction()
        {
            // Code pour ajouter un créneau
        }
        public void GenererCreneaux( Prestation prestation)
        {
            
            Creneaux.Clear();
            DaysInWeeks.Clear();

            DateTime DayInWeek = new DateTime(currentMonth.Year, currentMonth.Month, 1);

            int OffsetDay = ((int)DayInWeek.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
            

            DateTime firstMonday = DayInWeek.AddDays(OffsetDay);

            // Ajouter 7 jours (lundi à dimanche)
            for (int i = 1; i <= 7; i++)
            {
                DateTime jour = firstMonday.AddDays(i);
                CalendarDay day = new CalendarDay(jour, jour.Day, true);
                DaysInWeeks.Add(day);
            }

            foreach (var day in DaysInWeeks)
            {
                if (!day.IsValid) continue;

                TimeOnly startTime = new TimeOnly(8, 0);

                for (int j = 1; j < 4; j++)
                {
                    
                    TimeOnly endTime = startTime.AddMinutes(prestation.Duree);
                    Creneaux.Add(new Creneau(j, startTime, endTime, true, null, prestation.Id, day, 0));
                    startTime = endTime.AddMinutes(15);
                }
            }


        }
        public void LoadCalendar()
        {
            Days.Clear();

           
            DateTime FirstDay = new (currentMonth.Year, currentMonth.Month,1);
            int OffsetDay = ((int)FirstDay.DayOfWeek - (int)DayOfWeek.Monday+7)%7;

            int DaysInMonth = DateTime.DaysInMonth(currentMonth.Year, currentMonth.Month);


            for (int i = 0; i < OffsetDay; i++)
            {
                DateTime PreMonthdate = new DateTime(currentMonth.Year, currentMonth.Month, 1).AddDays(-OffsetDay +i );

                Days.Add(new CalendarDay(PreMonthdate, PreMonthdate.Day, false));
            }

            for (int i = 1; i <= DaysInMonth; i++)
            {
                DateTime date = new DateTime(currentMonth.Year, currentMonth.Month, i );

                Days.Add(new CalendarDay(date, i ));
            }


            int totalCells = 42;
            int remainingDays = totalCells - Days.Count;
            DateTime nextMonthDate = FirstDay.AddMonths(1);
            for (int i = 0; i < remainingDays; i++)
            {
                Days.Add(new CalendarDay(nextMonthDate, nextMonthDate.Day, false));
                nextMonthDate = nextMonthDate.AddDays(1);
            }



        }
        public DisponibiliteVM()


        {
           
            PreviousMonthCommand = new RelayCommand(PreviousMonthAction);
              NextMonthCommand = new RelayCommand(NextMonthAction);
                SelectDayCommand = new RelayCommand(SelectDayAction);
                AjouterCreneauCommand = new RelayCommand(AjouterCreneauAction);
                Days = new ObservableCollection<CalendarDay>();
                Creneaux = new ObservableCollection<Creneau>();
                DaysInWeeks = new ObservableCollection<CalendarDay>();
            LoadCalendar();
            Prestations = new ObservableCollection<Prestation>();
            Prestations.Add(
                new(1,"Consultation",60,"description",700)
                );

            GenererCreneaux(Prestations[0]);
            Console.WriteLine( Creneaux);
            Console.WriteLine(DaysInWeeks);
        }

    }
}
