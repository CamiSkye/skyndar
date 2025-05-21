using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPF.Models;

namespace WPF.ViewModels
{
    public class DisponibiliteVM : MainVM
    {
        private DateTime currentMonth = DateTime.Today;
        public string CurrentMonth => currentMonth.ToString("MMMM yyyy", CultureInfo.GetCultureInfo("fr-FR"));
        
        private DateTime selectedDate { get; set; }
        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set
            {
                selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }

        public ObservableCollection<CalendarDay> Days { get; set; }
        public ObservableCollection<CalendarDay> DaysInWeeks { get; set; }
        public ObservableCollection<Creneau> Creneaux { get; set; }
        public ObservableCollection<Creneau> DaysCreneaux { get; set; }
        public ObservableCollection<Prestation> Prestations { get; set; } 
        public ICommand PreviousMonthCommand { get; set; }
        public ICommand NextMonthCommand { get; set; }
        public ICommand SelectDayCommand { get; set; }
        public ICommand AjouterCreneauCommand { get; set; }

        public void PreviousMonthAction()
        {
            DateTime DayInWeek = new DateTime(currentMonth.Year, currentMonth.Month, 1);
            currentMonth = currentMonth.AddMonths(-1);
            OnPropertyChanged(nameof(CurrentMonth));
            LoadCalendar();
            GenererCreneaux(Prestations[0], DayInWeek);
        }
        public void NextMonthAction()
        {
            DateTime DayInWeek = new DateTime(currentMonth.Year, currentMonth.Month, 1);
            currentMonth = currentMonth.AddMonths(1);
            OnPropertyChanged(nameof(CurrentMonth));
            LoadCalendar();
            GenererCreneaux(Prestations[0], DayInWeek);
        }
        public void SelectDayAction(CalendarDay day)

        {
            SelectedDate = day.Date;
            GenererCreneaux(Prestations[0], SelectedDate);
        }
        public void AjouterCreneauAction(CalendarDay day)
        {
           MessageBox.Show("Ajouter un créneau");
MessageBoxButton buttons = MessageBoxButton.OKCancel;
            MessageBoxResult result = MessageBox.Show("Ajouter un créneau", "Confirmation", buttons, MessageBoxImage.Question);
            if (result == MessageBoxResult.OK)
            {
                // Code à exécuter si l'utilisateur clique sur "OK"
                
                TimeOnly startTime = new TimeOnly(8, 0);
                TimeOnly endTime = startTime.AddMinutes(60);
                for (int i = 0; i < DaysInWeeks.Count; i++)
                {
                    if (DaysInWeeks[i].Date.Date == day.Date)
                    {
                        startTime = DaysInWeeks[i].DayCreneaux.Last().HeureFin.AddMinutes(15);
                        endTime = startTime.AddMinutes(60);

                        Creneau creneau = new Creneau(1, startTime, endTime, true, Prestations[0], Prestations[0].Id, DaysInWeeks[i], 0);
                        Creneaux.Add(creneau);

                        DaysInWeeks[i].DayCreneaux.Add(creneau);
                        break;

                    }
                }
               
                MessageBox.Show("Créneau ajouté !");
            }
            else
            {
                // Code à exécuter si l'utilisateur clique sur "Annuler"
                Console.WriteLine("Créneau non ajouté.");
            }
        }
        public void GenererCreneaux( Prestation prestation, DateTime DayInWeek)
        {
            
            Creneaux.Clear();
            DaysInWeeks.Clear();

             

            int OffsetDay = ((int)DayInWeek.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
            

            DateTime firstMonday = DayInWeek.AddDays(-OffsetDay);

            // Ajouter 7 jours (lundi à dimanche)
            for (int i = 0; i < 7; i++)
            {
                DateTime jour = firstMonday.AddDays(i);
                CalendarDay day = new CalendarDay(jour, jour.Day, true);
                DaysInWeeks.Add(day);
            }

            foreach ( var day in DaysInWeeks)
            {
                if (!day.IsValid) continue;

                TimeOnly startTime = new TimeOnly(8, 0);

                for (int j = 1; j < 4; j++)
                {
                    
                    TimeOnly endTime = startTime.AddMinutes(prestation.Duree);
                    Creneau creneau = new Creneau(j, startTime, endTime, true, prestation, prestation.Id, day, 0);
                    day.DayCreneaux.Add(creneau);
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
           
            Prestations = new ObservableCollection<Prestation>();
            Prestations.Add(
                new(1, "Consultation", 60, "description", 700)
                );
            PreviousMonthCommand = new RelayCommand(PreviousMonthAction);
              NextMonthCommand = new RelayCommand(NextMonthAction);

                SelectDayCommand = new RelayCommand((param) => {
                    if (param is CalendarDay day) SelectDayAction(day); 
                });

            AjouterCreneauCommand = new RelayCommand((param) => {
                if (param is CalendarDay day) AjouterCreneauAction(day);
            });
                Days = new ObservableCollection<CalendarDay>();
                Creneaux = new ObservableCollection<Creneau>();
                DaysInWeeks = new ObservableCollection<CalendarDay>();
           
            LoadCalendar();
            DateTime DayInWeek = new DateTime(currentMonth.Year, currentMonth.Month, 1);
            GenererCreneaux(Prestations[0],DayInWeek);
            
            Console.WriteLine( Creneaux);
            Console.WriteLine(DaysInWeeks);
        }

    }
}
