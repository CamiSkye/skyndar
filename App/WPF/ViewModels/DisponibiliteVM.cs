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
using Microsoft.VisualBasic; // Nécessite référence à Microsoft.VisualBasic

using System.Windows.Input;
using WPF.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        private bool iscabinetchecked;
        public bool IsCabinetChecked
        {
            get { return iscabinetchecked; }
            set
            {
                iscabinetchecked = value;
                OnPropertyChanged(nameof(IsCabinetChecked));
                foreach (CalendarDay day in DaysInWeeks)
                {
                    filtrerCreneaux(day);
                    OnPropertyChanged(nameof(DaysInWeeks));
                }
            }
        }
        private bool isvisiochecked;
        public bool IsVisioChecked
        {
            get { return isvisiochecked; }
            set
            {
                isvisiochecked = value;
                OnPropertyChanged(nameof(IsVisioChecked));
                foreach (CalendarDay day in DaysInWeeks)
                {
                    filtrerCreneaux(day);
                    OnPropertyChanged(nameof(DaysInWeeks));
                }
            }
        }

        private string selectedPrestation { get; set; }
        public string SelectedPrestation
        {
            get { return selectedPrestation; }
            set
            {
                selectedPrestation = value;
                OnPropertyChanged(nameof(SelectedPrestation));
                DateTime DayInWeek = new DateTime(currentMonth.Year, currentMonth.Month, 1);
                if (value != null)
                {
                    AfficherCreneaux(DayInWeek);

                }
                else
                {
                    GenererCreneaux(Prestations[0], DayInWeek);
                }
            }



        }

        public ObservableCollection<CalendarDay> Days { get; set; }
        public ObservableCollection<CalendarDay> DaysInWeeks { get; set; }
        public ObservableCollection<Creneau> Creneaux { get; set; }
        public ObservableCollection<Creneau> DayCreneaux { get; set; }
        public ObservableCollection<Prestation> Prestations { get; set; } 
        public ICommand PreviousMonthCommand { get; set; }
        public ICommand NextMonthCommand { get; set; }
        public ICommand SelectDayCommand { get; set; }
        public ICommand AjouterCreneauCommand { get; set; }

        public Prestation GetPrestationByTitre(string titre)
        {
            return Prestations.FirstOrDefault(p => p.Titre == titre);
        }
        public void filtrerCreneaux(CalendarDay day)
        {
            IEnumerable<Creneau> newDayCreneaux = Creneaux.Where(c => c.Date.Date == day.Date.Date).OrderBy(c=>c.HeureDebut);

            IEnumerable<Creneau> filtredCreneaux = newDayCreneaux;

            if (IsCabinetChecked && !IsVisioChecked)
            {
                filtredCreneaux = newDayCreneaux.Where(c => c.Cabinet == true);
            }
            else if (!IsCabinetChecked && IsVisioChecked)
            {
                filtredCreneaux = newDayCreneaux.Where(c => c.Cabinet == false);
            }
            else 
            {
                filtredCreneaux = newDayCreneaux;
            }


            day.DayCreneaux.Clear();
            foreach (Creneau creneau in filtredCreneaux)
            {
                day.DayCreneaux.Add(creneau);
            }
        }

        public void PreviousMonthAction()
        {
            DateTime DayInWeek = new (currentMonth.Year, currentMonth.Month, 1);
            currentMonth = currentMonth.AddMonths(-1);
            OnPropertyChanged(nameof(CurrentMonth));
            LoadCalendar();
            AfficherCreneaux(DayInWeek);
        }
        
        public void NextMonthAction()
        {
            DateTime DayInWeek = new (currentMonth.Year, currentMonth.Month, 1);

            currentMonth = currentMonth.AddMonths(1);
            OnPropertyChanged(nameof(CurrentMonth));
            LoadCalendar();
            AfficherCreneaux(DayInWeek);
        }
        
        public void AjouterCreneauAction(CalendarDay day)
        {
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxResult result = MessageBox.Show("Voulez-vous ajouter un créneau ?", "Confirmation", buttons, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;
           
            string startTimeInput = Interaction.InputBox("Heure de début (HH:mm)", "Créneau", "08:00", -1, -1);
           
            if (!TimeOnly.TryParse(startTimeInput, out TimeOnly startTime) )
            {
                MessageBox.Show("Format d'heure invalide. Utilisez HH:mm.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (startTime < day.DayCreneaux.Last().HeureFin.AddMinutes(15))
            {
                MessageBox.Show("Ajouter 15 min au minumun au dernier créneau ");
                return;
            }
            MessageBoxButton actions = MessageBoxButton.YesNo;
                MessageBoxResult resultat = MessageBox.Show("Est-ce en  Cabinet ? ", "Confirmation", actions, MessageBoxImage.Question);
                bool cabinet = (resultat == MessageBoxResult.Yes) ? true : false;
            
                 Prestation prestation = GetPrestationByTitre(SelectedPrestation);
                TimeOnly endTime = startTime.AddMinutes(prestation.Duree);
            for (int i = 0; i < DaysInWeeks.Count; i++)
            {

                if (DaysInWeeks[i].Date.Date == day.Date)
                {

                    Creneau creneau = new(i, startTime, endTime, day.Date, cabinet, prestation, prestation.Id);
                    Creneaux.Add(creneau);

                    DaysInWeeks[i].DayCreneaux.Add(creneau);
                    break;

                }
            }
            MessageBox.Show($"Créneau ajouté : {startTime} - {endTime} ({(cabinet ? " Cabinet " : " Visio ")})");


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
                
                nextMonthDate = nextMonthDate.AddDays(1);
                Days.Add(new CalendarDay(nextMonthDate, nextMonthDate.Day, false));
            }

        }
        public void GenererCreneaux(Prestation prestation, DateTime DayInWeek)
        {
            
            DaysInWeeks.Clear();

            int OffsetDay = ((int)DayInWeek.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;

            DateTime firstMonday = DayInWeek.AddDays(-OffsetDay);

            for (int i = 0; i < 7; i++)
            {

                DateTime jour = firstMonday.AddDays(i);

                CalendarDay day = new CalendarDay(jour, jour.Day, true);
                DaysInWeeks.Add(day);
            }

            
            foreach (CalendarDay day in DaysInWeeks)
            {
                if (!day.IsValid) continue;

                TimeOnly startTime = new TimeOnly(8, 0);

                for (int j = 1; j < 4; j++)
                {
                    DateTime  date = day.Date;
                 
                    TimeOnly endTime = startTime.AddMinutes(prestation.Duree);
                    Creneau creneau = new Creneau(j, startTime, endTime,date, true, prestation, prestation.Id);
                    day.DayCreneaux.Add(creneau);
                    startTime = endTime.AddMinutes(15);
                }
            }
        }

        
        
        public void SelectDayAction(CalendarDay day)

        {
            SelectedDate = day.Date;
            AfficherCreneaux(SelectedDate);
            
        }


        public void AfficherCreneaux(DateTime date)

        {
            foreach (Prestation prestation in Prestations)
            {
                if (prestation.Titre == SelectedPrestation)
                {
                    GenererCreneaux(prestation, date);
                    break;
                }
            }
        }


        public DisponibiliteVM()
        {
            Prestations = new ObservableCollection<Prestation>()
            {
                 new(1, "Mentorat Transitionnel ~ 1h", 60, "description", 700),
                new(1, "Mentorat Transitionnel ~ 2h", 120, "description", 700),
                 new(1, "Mentorat Transitionnel ~ 1h30", 90, "description", 700)
               
            };
           
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
                DayCreneaux = new ObservableCollection<Creneau>();

            DateTime DayInWeek = new DateTime(currentMonth.Year, currentMonth.Month, 1);

            LoadCalendar();
        }

    }
}
