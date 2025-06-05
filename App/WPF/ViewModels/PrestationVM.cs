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
using Microsoft.VisualBasic;
using System.Windows.Input;
using WPF.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPF.ViewModels
{
    class PrestationVM
    {
        public ObservableCollection<Prestation> Prestations { get; set; }
        public int Id { get; set; } 
        public string Titre { get; set; }
        public int Duree { get; set; }
        public int Tarif { get; set; }
        public string Description { get; set; }
        public Prestation SelectedPrestation { get; set; }


        public PrestationVM()
        {
            Prestations = [];
        }

        public void AjouterPrestation()
        {
            if (Id > 0 &&
     !string.IsNullOrWhiteSpace(Titre) &&
     Duree > 0 &&
     Tarif > 0 &&
     !string.IsNullOrWhiteSpace(Description))
            {
                Prestations.Add(
                    new Prestation(
                    Id =Id,
                    Titre = Titre,
                    Duree = Duree,
                    Description = Description,
                    Tarif = Tarif
                  
                ));

                // Réinitialisation (selon le contexte)
                Id = 0;
                Duree = 0;
                Tarif = 0;
                Titre = Description = string.Empty;
            }
        }
        //Je ne suis pas sûre de l'efficacité de ce bout de code...
        public void SupprimerPrestation()
        {
            if (SelectedPrestation != null)
            {
                Prestations.Remove(SelectedPrestation);
                SelectedPrestation = null;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
