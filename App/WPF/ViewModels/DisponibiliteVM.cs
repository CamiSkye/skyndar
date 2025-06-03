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
using WPF.Services;

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

        private int selectedPrestationId { get; set; }
        public int SelectedPrestationId
        {
            get { return selectedPrestationId; }
            set
            {
                selectedPrestationId = value;
                OnPropertyChanged(nameof(SelectedPrestationId));
                DateTime DayInWeek = new (currentMonth.Year, currentMonth.Month, 1);
                if (selectedPrestationId == null)
                {
                    GenererCreneaux(Prestations[0], DayInWeek);

                }
                else
                {
                    AfficherCreneaux(DayInWeek);
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
        readonly DatabaseService BDD = new();
        public int GetPrestationDureeById(int Id)
        {
            return Prestations.FirstOrDefault(p => p.Id == Id)?.Duree ?? 0;
        }
        public DateTime GetDateById(int Id)
        {
            return DaysInWeeks.FirstOrDefault(d => d.Id == Id)?.Date ?? DateTime.MinValue;
        }
        public void filtrerCreneaux(CalendarDay day)
        {
            DateTime date= GetDateById(day.Id);

            IEnumerable<Creneau> newDayCreneaux = Creneaux.Where(c => c.DayId == day.Id);

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
                Creneaux.Add(creneau);
                BDD.AddCreneau(creneau);
            }
        }

        public void PreviousMonthAction()
        {
            DateTime DayInWeek = new(currentMonth.Year, currentMonth.Month, 1);
            currentMonth = currentMonth.AddMonths(-1);
            OnPropertyChanged(nameof(CurrentMonth));
            LoadCalendar();
            AfficherCreneaux(DayInWeek);
        }

        public void NextMonthAction()
        {
            DateTime DayInWeek = new(currentMonth.Year, currentMonth.Month, 1);

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

            string startTimeInput = Interaction.InputBox("Heure de début (HH:mm)", "Créneau", "14:00", -1, -1);

            if (!TimeSpan.TryParse(startTimeInput, out TimeSpan startTime))
            {
                MessageBox.Show("Format d'heure invalide. Utilisez HH:mm.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            if (startTime < day.DayCreneaux.Last().HeureFin.Add(TimeSpan.FromMinutes(15)))
            {
                MessageBox.Show("Ajouter 15 min au minumun au dernier créneau ");
                return;
            }
            MessageBoxButton actions = MessageBoxButton.YesNo;
            MessageBoxResult resultat = MessageBox.Show("Est-ce en  Cabinet ? ", "Confirmation", actions, MessageBoxImage.Question);
            bool cabinet = (resultat == MessageBoxResult.Yes);

            int duree = GetPrestationDureeById(SelectedPrestationId);
            TimeSpan endTime = startTime.Add(TimeSpan.FromMinutes(duree));

            int id = day.DayCreneaux.Count > 0 ? day.DayCreneaux.Max(c => c.Id) + 1 : 1; 
            Creneau creneau = new(id, startTime, endTime, cabinet,day.Id, SelectedPrestationId);


            day.DayCreneaux.Add(creneau);
            Creneaux.Add(creneau);
            BDD.AddCreneau(creneau);

            MessageBox.Show($"Créneau ajouté : {startTime} - {endTime} ({(cabinet ? " Cabinet " : " Visio ")})");


        }

        public void LoadCalendar()
        {
            Days.Clear();

            DateTime FirstDay = new (currentMonth.Year, currentMonth.Month,1); 
            int OffsetDay = ((int)FirstDay.DayOfWeek - (int)DayOfWeek.Monday+7)%7;

            int DaysInMonth = DateTime.DaysInMonth(currentMonth.Year, currentMonth.Month);


            for (int i = 1; i <= OffsetDay; i++)
            {
                DateTime PreMonthdate = new DateTime(currentMonth.Year, currentMonth.Month, 1).AddDays(-OffsetDay +i );

                CalendarDay PreMonthDay = new (i, PreMonthdate, PreMonthdate.Day, false);
                Days.Add(PreMonthDay);
                
            }

            for (int i = 1; i <= DaysInMonth; i++)
            {
                DateTime date = new (currentMonth.Year, currentMonth.Month, i );

                CalendarDay Day = new (i, date, i, true);
                Days.Add(Day);
              
               
            }


            int totalCells = 42;
            int remainingDays = totalCells - Days.Count;
            DateTime nextMonthDate = FirstDay.AddMonths(1);
            for (int i = 0; i < remainingDays; i++)
            {
                
                nextMonthDate = nextMonthDate.AddDays(1);
                CalendarDay NextMonthDay = new CalendarDay(i, nextMonthDate, nextMonthDate.Day, false);
                Days.Add(NextMonthDay);
                
              

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

                CalendarDay day = new (i,jour, jour.Day, true);
                DaysInWeeks.Add(day);
                BDD.insertDay(day);
            }

            
            for (int i = 0; i< DaysInWeeks.Count; i++)
            {
                 CalendarDay day= DaysInWeeks[i];

                if (!day.IsValid) continue;

                TimeSpan startTime = new (8,0, 0);

                for (int j = 1; j < 5; j++)
                {
                    DateTime  date = day.Date;
                 
                    TimeSpan endTime = startTime.Add(TimeSpan.FromMinutes(prestation.Duree));

                    Creneau creneau = new (j, startTime, endTime, true,day.Id, prestation.Id);
                    day.DayCreneaux.Add(creneau);
                    startTime = endTime.Add(TimeSpan.FromMinutes(15));
                    Creneaux.Add(creneau);
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
                if (prestation.Id == SelectedPrestationId)
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
                new(2, "Mentorat Transitionnel ~ 2h", 120, "description", 700),
                 new(3, "Mentorat Transitionnel ~ 1h30", 90, "description", 700)
               
            };
            for (int i = 0; i < Prestations.Count; i++)
            {
                BDD.AddPrestation(Prestations[i]);
            }
            
            PreviousMonthCommand = new RelayCommand(PreviousMonthAction);

            NextMonthCommand = new RelayCommand(NextMonthAction);

            SelectDayCommand = new RelayCommand((param) => {
                    if (param is CalendarDay day) SelectDayAction(day); 
                });


            AjouterCreneauCommand = new RelayCommand((param) => {

                if (param is CalendarDay day) AjouterCreneauAction(day);
            });
                Days = [];
                Creneaux = [];
                DaysInWeeks = [];
                DayCreneaux = [];

            DateTime DayInWeek = new DateTime(currentMonth.Year, currentMonth.Month, 1);

            LoadCalendar();
        }

    }
}
