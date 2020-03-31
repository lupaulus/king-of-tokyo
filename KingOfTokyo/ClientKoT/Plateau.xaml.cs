using Client.Reseau;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        private ActionTour actionTour;
        private bool[] listDesRoll;

        public Plateau(HelperServeur h)
        {
            InitializeComponent();
            helperServeur = h;
            h.PartieStart += H_PartieStart;
            h.TourSuivant += H_TourSuivant;
            h.ResultatDes += H_ResultatDes;
            h.UpdateInfo += H_UpdateInfo;
            h.PartieFini += H_PartieFini;
            actionTour = new ActionTour();
            listDesRoll = new bool[6];
            
        }

        private void H_PartieFini(object sender, EventFinPartieArgs args)
        {
            string etat = helperServeur.ActualPlayer == args.FPartie.JoueurGagnant ? "Gagné" : "Perdu";
            MessageBox.Show($"Vous avez {etat}");
            Application.Current.Shutdown();
        }

        private void H_UpdateInfo(object sender, EventArgs args)
        {
            Dispatcher.BeginInvoke(new Action(delegate ()
            {
                UpdateData();
            }));
        }

        

        private void H_ResultatDes(object sender, EventDesArgs args)
        {
            actionTour = args.Action;
            Dispatcher.BeginInvoke(new Action(delegate ()
            {
                switch (actionTour.EtatDes)
                {
                    case EtatLancerDes.PremierLance:
                        actionTour.EtatDes = EtatLancerDes.DeuxiemeLance;
                        break;
                    case EtatLancerDes.DeuxiemeLance:
                        actionTour.EtatDes = EtatLancerDes.TroisiemeLance;
                        break;
                    case EtatLancerDes.TroisiemeLance:
                        // On desactive le bouton
                        LancerDes.IsEnabled = false;
                        EnabledDes(false);
                        break;
                }

                UpdateDes();
            }));
           
               
        }

       

        private void H_TourSuivant(object sender, EventArgs args)
        {
            Dispatcher.BeginInvoke(new Action(delegate ()
            {
                Debut_Tour();
                UpdateMagasin();
            }));
        }

        private void H_PartieStart(object sender, EventArgs e)
        {
            MessageBox.Show("La partie va commencer");
            Dispatcher.BeginInvoke(new Action(delegate ()
            {
                Hide_Joueur();
                UpdateData();
                Debut_Tour();
                UpdateMagasin();
            }));
        }

        
        private void UpdateMagasin()
        {
            string path1 = String.Concat("/Images/imgCartes/", helperServeur.ImageCarte1);
            string path2 = String.Concat("/Images/imgCartes/", helperServeur.ImageCarte2);
            string path3 = String.Concat("/Images/imgCartes/", helperServeur.ImageCarte3);

            Uri uri1 = new Uri(path1, UriKind.Relative);
            Uri uri2 = new Uri(path2, UriKind.Relative);
            Uri uri3 = new Uri(path3, UriKind.Relative);

            Carte1.Source = new BitmapImage(uri1);
            Carte2.Source = new BitmapImage(uri2);
            Carte3.Source = new BitmapImage(uri3);
        }

        private void UpdateDes()
        {
            
            string path1 = String.Concat("/Images/imgDes/","Des_" ,actionTour.Des1.ToString(),".png");
            string path2 = String.Concat("/Images/imgDes/", "Des_", actionTour.Des2.ToString(), ".png");
            string path3 = String.Concat("/Images/imgDes/", "Des_", actionTour.Des3.ToString(), ".png");
            string path4 = String.Concat("/Images/imgDes/", "Des_", actionTour.Des4.ToString(), ".png");
            string path5 = String.Concat("/Images/imgDes/", "Des_", actionTour.Des5.ToString(), ".png");
            string path6 = String.Concat("/Images/imgDes/", "Des_", actionTour.Des6.ToString(), ".png");

            Uri uri1 = new Uri(path1, UriKind.Relative);
            Uri uri2 = new Uri(path2, UriKind.Relative);
            Uri uri3 = new Uri(path3, UriKind.Relative);
            Uri uri4 = new Uri(path4, UriKind.Relative);
            Uri uri5 = new Uri(path5, UriKind.Relative);
            Uri uri6 = new Uri(path6, UriKind.Relative);

            if (actionTour.Des1 != ValeurDes.Unknown)
            {
                imgDe1.Source = new BitmapImage(uri1);
            }
            if (actionTour.Des1 != ValeurDes.Unknown)
            {
                imgDe2.Source = new BitmapImage(uri2);
            }
            if (actionTour.Des1 != ValeurDes.Unknown)
            {
                imgDe3.Source = new BitmapImage(uri3);
            }
            if (actionTour.Des1 != ValeurDes.Unknown)
            {
                imgDe4.Source = new BitmapImage(uri4);
            }
            if (actionTour.Des1 != ValeurDes.Unknown)
            {
                imgDe5.Source = new BitmapImage(uri5);
            }
            if (actionTour.Des1 != ValeurDes.Unknown)
            {
                imgDe6.Source = new BitmapImage(uri6);
            }         
        }

        
        private void Debut_Tour()
        {
            Thread.Sleep(1000);

            EnabledDes(false);
            actionTour = new ActionTour();
            InfoJoueur infoJoueur = FindJoueur(helperServeur.ActualPlayer);
            if(!infoJoueur.AToiDeJouer)
            {
                btnFinTour.IsEnabled = false;
                Acheter1.IsEnabled = false;
                Acheter2.IsEnabled = false;
                Acheter3.IsEnabled = false;
                EntrerBay.IsEnabled = false;
                EntrerVille.IsEnabled = false;
                LancerDes.IsEnabled = false;
                Reroll.IsEnabled = false;
                MessageBox.Show($"C'est au tour de {FindJoueurAToiJouer().Pseudo} de jouer");
                return;
            }

            btnFinTour.IsEnabled = true;
            Acheter1.IsEnabled = true;
            Acheter2.IsEnabled = true;
            Acheter3.IsEnabled = true;
            EntrerBay.IsEnabled = true;
            EntrerVille.IsEnabled = true;
            LancerDes.IsEnabled = true;
            Reroll.IsEnabled = true;

            // Alerte message
            MessageBox.Show("C'est à votre tour de jouer");

        }

        private InfoJoueur FindJoueurAToiJouer()
        {
            while(true)
            {
                foreach (InfoJoueur info in helperServeur.ListInfoJoueur)
                {
                    if (info.AToiDeJouer)
                    {
                        return info;
                    }
                }
                Thread.Sleep(100);
            }
            
           
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

        public void UpdateData()
        {
            Thread.Sleep(1000);
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
            while(true)
            {
                foreach (InfoJoueur info in helperServeur.ListInfoJoueur)
                {
                    if (info.IdJoueur == actualPlayerPosition)
                    {
                        return info;
                    }
                }
                Thread.Sleep(100);
            }
           
        }

        public void Hide_Joueur()
        {
            btnFinTour.Visibility = Visibility.Visible;
            btnPret.Visibility = Visibility.Hidden;
            lblPret.Visibility = Visibility.Hidden;

            switch(helperServeur.NombreJoueurs())
            {
                case 0: // 0 joueur
                    GVille.Visibility = Visibility.Visible;
                    GBay.Visibility = Visibility.Visible;
                    GDeck.Visibility = Visibility.Visible;
                    GDes.Visibility = Visibility.Visible;
                    j1.Visibility = Visibility.Hidden;
                    j2.Visibility = Visibility.Hidden;
                    j3.Visibility = Visibility.Hidden;
                    j4.Visibility = Visibility.Hidden;
                    j5.Visibility = Visibility.Hidden;
                    j6.Visibility = Visibility.Hidden;
                    break;
                case 1: // 1 joueur
                    GVille.Visibility = Visibility.Visible;
                    GBay.Visibility = Visibility.Visible;
                    GDeck.Visibility = Visibility.Visible;
                    GDes.Visibility = Visibility.Visible;
                    j1.Visibility = Visibility.Visible;
                    j2.Visibility = Visibility.Hidden;
                    j3.Visibility = Visibility.Hidden;
                    j4.Visibility = Visibility.Hidden;
                    j5.Visibility = Visibility.Hidden;
                    j6.Visibility = Visibility.Hidden;
                    break;
                case 2: // 2 joueurs
                    GVille.Visibility = Visibility.Visible;
                    GBay.Visibility = Visibility.Visible;
                    GDeck.Visibility = Visibility.Visible;
                    GDes.Visibility = Visibility.Visible;
                    j1.Visibility = Visibility.Visible;
                    j2.Visibility = Visibility.Visible;
                    j3.Visibility = Visibility.Hidden;
                    j4.Visibility = Visibility.Hidden;
                    j5.Visibility = Visibility.Hidden;
                    j6.Visibility = Visibility.Hidden;
                    break;
                case 3: // 3 joueurs
                    GVille.Visibility = Visibility.Visible;
                    GBay.Visibility = Visibility.Visible;
                    GDeck.Visibility = Visibility.Visible;
                    GDes.Visibility = Visibility.Visible;
                    j1.Visibility = Visibility.Visible;
                    j2.Visibility = Visibility.Visible;
                    j3.Visibility = Visibility.Visible;
                    j4.Visibility = Visibility.Hidden;
                    j5.Visibility = Visibility.Hidden;
                    j6.Visibility = Visibility.Hidden;
                    break;
                case 4: // 4 joueurs
                    GVille.Visibility = Visibility.Visible;
                    GBay.Visibility = Visibility.Visible;
                    GDeck.Visibility = Visibility.Visible;
                    GDes.Visibility = Visibility.Visible;
                    j1.Visibility = Visibility.Visible;
                    j2.Visibility = Visibility.Visible;
                    j3.Visibility = Visibility.Visible;
                    j4.Visibility = Visibility.Visible;
                    j5.Visibility = Visibility.Hidden;
                    j6.Visibility = Visibility.Hidden;
                    break;
                case 5: // 5 joueurs
                    GVille.Visibility = Visibility.Visible;
                    GBay.Visibility = Visibility.Visible;
                    GDeck.Visibility = Visibility.Visible;
                    GDes.Visibility = Visibility.Visible;
                    j1.Visibility = Visibility.Visible;
                    j2.Visibility = Visibility.Visible;
                    j3.Visibility = Visibility.Visible;
                    j4.Visibility = Visibility.Visible;
                    j5.Visibility = Visibility.Visible;
                    j6.Visibility = Visibility.Hidden;
                    break;
                case 6: // 6 joueurs
                    GVille.Visibility = Visibility.Visible;
                    GBay.Visibility = Visibility.Visible;
                    GDeck.Visibility = Visibility.Visible;
                    GDes.Visibility = Visibility.Visible;
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
            helperServeur.FinTour();
        }

        private void LancerDes_Click(object sender, RoutedEventArgs e)
        {
            for(int i = 0;i<listDesRoll.Length;i++)
            {
                if(listDesRoll[i])
                {
                    switch(i)
                    {
                        case 0:
                            actionTour.Des1 = ValeurDes.Unknown;
                            break;
                        case 1:
                            actionTour.Des2 = ValeurDes.Unknown;
                            break;
                        case 2:
                            actionTour.Des3 = ValeurDes.Unknown;
                            break;
                        case 3:
                            actionTour.Des4 = ValeurDes.Unknown;
                            break;
                        case 4:
                            actionTour.Des5 = ValeurDes.Unknown;
                            break;
                        case 5:
                            actionTour.Des6 = ValeurDes.Unknown;
                            break;

                    }
                }
            }

            helperServeur.RollDes(actionTour);
            listDesRoll = new bool[6]; // Clear List;
            De1.Background = Brushes.Green;
            De2.Background = Brushes.Green;
            De3.Background = Brushes.Green;
            De4.Background = Brushes.Green;
            De5.Background = Brushes.Green;
            De6.Background = Brushes.Green;

            EnabledDes(true);

            
        }

        public void EnabledDes(bool etat)
        {
            De1.IsEnabled = etat;
            De2.IsEnabled = etat;
            De3.IsEnabled = etat;
            De4.IsEnabled = etat;
            De5.IsEnabled = etat;
            De6.IsEnabled = etat;
        }

        private void De1_Click(object sender, RoutedEventArgs e)
        {
            listDesRoll[0] = listDesRoll[0] ? false : true;
            De1.Background = listDesRoll[0] ? Brushes.Violet : Brushes.Green;
        }

        private void De2_Click(object sender, RoutedEventArgs e)
        {
            listDesRoll[1] = listDesRoll[1] ? false : true;
            De2.Background = listDesRoll[1] ? Brushes.Violet : Brushes.Green;
        }

        private void De3_Click(object sender, RoutedEventArgs e)
        {
            listDesRoll[2] = listDesRoll[2] ? false : true;
            De3.Background = listDesRoll[2] ? Brushes.Violet : Brushes.Green;
        }

        private void De4_Click(object sender, RoutedEventArgs e)
        {
            listDesRoll[3] = listDesRoll[3] ? false : true;
            De4.Background = listDesRoll[3] ? Brushes.Violet : Brushes.Green;
        }

        private void De5_Click(object sender, RoutedEventArgs e)
        {
            listDesRoll[4] = listDesRoll[4] ? false : true;
            De5.Background = listDesRoll[4] ? Brushes.Violet : Brushes.Green;
        }

        private void De6_Click(object sender, RoutedEventArgs e)
        {
            listDesRoll[5] = listDesRoll[5] ? false : true;
            De6.Background = listDesRoll[5] ? Brushes.Violet : Brushes.Green;
        }

    }
}
