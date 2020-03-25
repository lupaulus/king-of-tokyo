using Client.Reseau;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClientKoT
{
    /// <summary>
    /// Interaction logic for Plateau.xaml
    /// </summary>
    public partial class Plateau : UserControl
    {
        private bool joueurPret = false;

        private HelperServeur helperServeur;
        public Plateau(HelperServeur h)
        {
            InitializeComponent();
            helperServeur = h;
        }

        private void btnPret_Click(object sender, RoutedEventArgs e)
        {
            joueurPret = !joueurPret;
            if(joueurPret)
            {
                helperServeur.JoueurPret();
            }
            else
            {
                helperServeur.JoueurPasPret();
            }
            
        }
    }
}
