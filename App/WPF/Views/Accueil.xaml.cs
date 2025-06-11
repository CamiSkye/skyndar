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
//HEAD
using WPF.ViewModels; // Assuming AcueilVM is defined in this namespace

using WPF.ViewModels;

// 3ac3ca1786f8791b23300e43bebabd050ebb879c
namespace WPF.Views
{
    /// <summary>
    /// Logique d'interaction pour Accueil.xaml
    /// </summary>
    public partial class Accueil : Window
    {
        public Accueil()
        {
            InitializeComponent();
            this.DataContext = new AccueilVM();
        }
    }
}
