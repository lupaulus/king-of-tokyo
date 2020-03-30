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
            actionTour = new ActionTour();
            listDesRoll = new bool[6];
            
        }

        private void H_ResultatDes(object sender, EventDesArgs args)
        {
            actionTour = args.Action;
            Dispatcher.BeginInvoke(new Action(delegate ()
            {
                UpdateValeurDes();
            }));
           
               
        }

        private void UpdateValeurDes()
        {
            if(actionTour.Des1 != ValeurDes.Unknown)
            {
                De1.Content = actionTour.Des1.ToString();
            }
            if (actionTour.Des2 != ValeurDes.Unknown)
            {
                De2.Content = actionTour.Des2.ToString();
            }
            if (actionTour.Des3 != ValeurDes.Unknown)
            {
                De3.Content = actionTour.Des3.ToString();
            }
            if (actionTour.Des4 != ValeurDes.Unknown)
            {
                De4.Content = actionTour.Des4.ToString();
            }
            if (actionTour.Des5 != ValeurDes.Unknown)
            {
                De5.Content = actionTour.Des5.ToString();
            }
            if (actionTour.Des6 != ValeurDes.Unknown)
            {
                De6.Content = actionTour.Des6.ToString();
            }
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
                Update_Data();
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

        private void UpdateDes(object sender, EventArgs e)
        {
            string path1 = String.Concat("/Images/imgDes/", helperServeur.ImageCarte1);
            string path2 = String.Concat("/Images/imgDes/", helperServeur.ImageCarte2);
            string path3 = String.Concat("/Images/imgDes/", helperServeur.ImageCarte3);
            string path4 = String.Concat("/Images/imgDes/", helperServeur.ImageCarte3);
            string path5 = String.Concat("/Images/imgDes/", helperServeur.ImageCarte3);
            string path6 = String.Concat("/Images/imgDes/", helperServeur.ImageCarte3);

            Uri uri1 = new Uri(path1, UriKind.Relative);
            Uri uri2 = new Uri(path2, UriKind.Relative);
            Uri uri3 = new Uri(path3, UriKind.Relative);
            Uri uri4 = new Uri(path4, UriKind.Relative);
            Uri uri5 = new Uri(path5, UriKind.Relative);
            Uri uri6 = new Uri(path6, UriKind.Relative);

            imgDe1.Source = new BitmapImage(uri1);
            imgDe2.Source = new BitmapImage(uri2);
            imgDe3.Source = new BitmapImage(uri3);
            imgDe4.Source = new BitmapImage(uri1);
            imgDe5.Source = new BitmapImage(uri2);
            imgDe6.Source = new BitmapImage(uri3);
        }


        private void Debut_Tour()
        {
            Thread.Sleep(500);

            EnabledDes(false);
            InfoJoueur infoJoueur = FindJoueur(helperServeur.ActualPlayer);
            if(!infoJoueur.AToiDeJouer)
            {
                btnFinTour.IsEnabled = false;
                Acheter1.IsEnabled = false;
                Acheter2.IsEnabled = false;
                Acheter3.IsEnabled = false;
                EntrerBanlieue.IsEnabled = false;
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
            EntrerBanlieue.IsEnabled = true;
            EntrerVille.IsEnabled = true;
            LancerDes.IsEnabled = true;
            Reroll.IsEnabled = true;

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
            helperServeur.FinTour(actionTour);
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

            EnabledDes(true);

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
