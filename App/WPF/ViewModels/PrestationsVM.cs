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
    class PrestationsVM
    {
        public ObservableCollection<Prestation> Prestations { get; set; }

        public string Titre { get; set; }
        public string Duree { get; set; }
        public string Tarif { get; set; }
        public string Description { get; set; }
        public Prestation SelectedPrestation { get; set; }


        public PrestationsVM()
        {
            Prestations = new ObservableCollection<Models.Prestation>();
        }

        public void AjouterPrestation()
        {
            if (string.IsNullOrWhiteSpace(Titre) &&
                string.IsNullOrWhiteSpace(Duree) &&
                string.IsNullOrWhiteSpace(Tarif) &&
                string.IsNullOrWhiteSpace(Description))
            {
                Prestations.Add(new Prestation
                {
                    Titre = Titre,
                    Duree = Duree,
                    Tarif = Tarif,
                    Description = Description
                });

                Titre = Duree = Tarif = Description = string.Empty;
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
