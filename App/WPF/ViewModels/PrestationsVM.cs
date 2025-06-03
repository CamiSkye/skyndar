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

namespace WPF.ViewModels
{
    class PrestationsVM
    {
        public ObservableCollection<Prestation> Prestations { get; set; }

        public string Titre { get; set; }
        public string Duree { get; set; }
        public string Tarif { get; set; }
        public string Description { get; set; }

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
    }
}
