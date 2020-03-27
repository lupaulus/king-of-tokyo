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
                lblPret.Content = "Vous êtes prêt !";
            }
            else
            {
                helperServeur.JoueurPasPret();
                lblPret.Content = "Vous n'êtes pas prêt !";
            }
        }

        private void btnHide_Click(object sender, RoutedEventArgs e)
        {
            switch(int.Parse(nbHideJoueur.Text))
            {
                case 0: // 0 joueur
                    j1.Visibility = Visibility.Hidden;
                    j2.Visibility = Visibility.Hidden;
                    j3.Visibility = Visibility.Hidden;
                    j4.Visibility = Visibility.Hidden;
                    j5.Visibility = Visibility.Hidden;
                    j6.Visibility = Visibility.Hidden;
                    break;
                case 1: // 1 joueur
                    j1.Visibility = Visibility.Visible;
                    j2.Visibility = Visibility.Hidden;
                    j3.Visibility = Visibility.Hidden;
                    j4.Visibility = Visibility.Hidden;
                    j5.Visibility = Visibility.Hidden;
                    j6.Visibility = Visibility.Hidden;
                    break;
                case 2: // 2 joueurs
                    j1.Visibility = Visibility.Visible;
                    j2.Visibility = Visibility.Visible;
                    j3.Visibility = Visibility.Hidden;
                    j4.Visibility = Visibility.Hidden;
                    j5.Visibility = Visibility.Hidden;
                    j6.Visibility = Visibility.Hidden;
                    break;
                case 3: // 3 joueurs
                    j1.Visibility = Visibility.Visible;
                    j2.Visibility = Visibility.Visible;
                    j3.Visibility = Visibility.Visible;
                    j4.Visibility = Visibility.Hidden;
                    j5.Visibility = Visibility.Hidden;
                    j6.Visibility = Visibility.Hidden;
                    break;
                case 4: // 4 joueurs
                    j1.Visibility = Visibility.Visible;
                    j2.Visibility = Visibility.Visible;
                    j3.Visibility = Visibility.Visible;
                    j4.Visibility = Visibility.Visible;
                    j5.Visibility = Visibility.Hidden;
                    j6.Visibility = Visibility.Hidden;
                    break;
                case 5: // 5 joueurs
                    j1.Visibility = Visibility.Visible;
                    j2.Visibility = Visibility.Visible;
                    j3.Visibility = Visibility.Visible;
                    j4.Visibility = Visibility.Visible;
                    j5.Visibility = Visibility.Visible;
                    j6.Visibility = Visibility.Hidden;
                    break;
                case 6: // 6 joueurs
                    j1.Visibility = Visibility.Visible;
                    j2.Visibility = Visibility.Visible;
                    j3.Visibility = Visibility.Visible;
                    j4.Visibility = Visibility.Visible;
                    j5.Visibility = Visibility.Visible;
                    j6.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }
    }
}
