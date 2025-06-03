using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace WPF.Views
{
    /// <summary>
    /// Logique d'interaction pour Prestation.xaml
    /// </summary>
    public partial class Prestation : UserControl
    {
        
        public Prestation()
        {
            InitializeComponent();
          //  PrestationData();
        }

/*        private void PrestationData()
        {
            // Ton code de récupération de la BDD ici
            // dataGridView1.ItemsSource = maListe;
        }*/

        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(titre_pres.Text) ||
                string.IsNullOrWhiteSpace(duree_pres.Text) ||
                string.IsNullOrWhiteSpace(tarif_pres.Text) ||
                string.IsNullOrWhiteSpace(descip_pres.Text))
            {
                MessageBox.Show("Veuillez remplir tous les champs", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }

}
