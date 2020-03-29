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
            h.PartieStart += H_PartieStart;
            
        }

        

        private void H_PartieStart(object sender, EventArgs e)
        {
            MessageBox.Show("La partie va commencer");
            Dispatcher.BeginInvoke(new Action(delegate ()
            {
                Hide_Joueur();
                Update_Data();
                Debut_Tour();
            }));
        }

        private void Debut_Tour()
        {

            InfoJoueur infoJoueur = FindJoueur(helperServeur.ActualPlayer);
            if(!infoJoueur.AToiDeJouer)
            {
                btnFinTour.IsEnabled = false;
                Acheter1.IsEnabled = false;
                Acheter2.IsEnabled = false;
                Acheter3.IsEnabled = false;
                De1.IsEnabled = false;
                De2.IsEnabled = false;
                De3.IsEnabled = false;
                De4.IsEnabled = false;
                De5.IsEnabled = false;
                MessageBox.Show($"C'est au tour de {FindJoueurAToiJouer().Pseudo} de jouer");
                return;
            }

            btnFinTour.IsEnabled = true;
            Acheter1.IsEnabled = true;
            Acheter2.IsEnabled = true;
            Acheter3.IsEnabled = true;
            De1.IsEnabled = true;
            De2.IsEnabled = true;
            De3.IsEnabled = true;
            De4.IsEnabled = true;
            De5.IsEnabled = true;

            // Alerte message
            MessageBox.Show("C'est à votre tour de jouer");

            // 

            // Envoyer Action Tour
        }

        private InfoJoueur FindJoueurAToiJouer()
        {
            foreach (InfoJoueur info in helperServeur.ListInfoJoueur)
            {
                if (info.AToiDeJouer)
                {
                    return info;
                }
            }
            throw new KeyNotFoundException();
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

        public void Update_Data()
        {
            int actualPlayerPosition = (int)helperServeur.ActualPlayer;
            for(int i=0; i<helperServeur.ListInfoJoueur.Count;i++)
            {
                InfoJoueur infoJoueur = FindJoueur((Monstre)actualPlayerPosition);
                switch(i+1)
                {
                    case 1: // 1 joueur
                        nomJoueur1.Text = infoJoueur.Pseudo;
                        NbViesJ1.Text = infoJoueur.PtsVie.ToString();
                        NbEnergieJ1.Text = infoJoueur.PtsEnergie.ToString();
                        NbVictoireJ1.Text = infoJoueur.PtsVictoire.ToString();

                        break;
                    case 2: // 2 joueurs
                        nomJoueur2.Text = infoJoueur.Pseudo;
                        NbViesJ2.Text = infoJoueur.PtsVie.ToString();
                        NbEnergieJ2.Text = infoJoueur.PtsEnergie.ToString();
                        NbVictoireJ2.Text = infoJoueur.PtsVictoire.ToString();
                        break;
                    case 3: // 3 joueurs
                        nomJoueur3.Text = infoJoueur.Pseudo;
                        NbViesJ3.Text = infoJoueur.PtsVie.ToString();
                        NbEnergieJ3.Text = infoJoueur.PtsEnergie.ToString();
                        NbVictoireJ3.Text = infoJoueur.PtsVictoire.ToString();
                        break;
                    case 4: // 4 joueurs
                        nomJoueur4.Text = infoJoueur.Pseudo;
                        NbViesJ4.Text = infoJoueur.PtsVie.ToString();
                        NbEnergieJ4.Text = infoJoueur.PtsEnergie.ToString();
                        NbVictoireJ4.Text = infoJoueur.PtsVictoire.ToString();
                        break;
                    case 5: // 5 joueurs
                        nomJoueur5.Text = infoJoueur.Pseudo;
                        NbViesJ5.Text = infoJoueur.PtsVie.ToString();
                        NbEnergieJ5.Text = infoJoueur.PtsEnergie.ToString();
                        NbVictoireJ5.Text = infoJoueur.PtsVictoire.ToString();
                        break;
                    case 6: // 6 joueurs
                        nomJoueur6.Text = infoJoueur.Pseudo;
                        NbViesJ6.Text = infoJoueur.PtsVie.ToString();
                        NbEnergieJ6.Text = infoJoueur.PtsEnergie.ToString();
                        NbVictoireJ6.Text = infoJoueur.PtsVictoire.ToString();
                        break;
                    default:
                        break;
                }
                // Important
                actualPlayerPosition = (actualPlayerPosition + 1) % (helperServeur.ListInfoJoueur.Count+1);
                if(actualPlayerPosition == 0)
                {
                    actualPlayerPosition = 1;
                }

            }
                
        }

        private InfoJoueur FindJoueur(Monstre actualPlayerPosition)
        {
            foreach(InfoJoueur info in helperServeur.ListInfoJoueur)
            {
                if(info.IdJoueur == actualPlayerPosition)
                {
                    return info;
                }
            }
            throw new KeyNotFoundException();
        }

        public void Hide_Joueur()
        {
            btnFinTour.Visibility = Visibility.Visible;
            btnPret.Visibility = Visibility.Hidden;
            lblPret.Visibility = Visibility.Hidden;

            switch(helperServeur.NombreJoueurs())
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

        private void btnFinTour_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
