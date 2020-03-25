using Client.Reseau;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace ClientKoT
{
    /// <summary>
    /// Interaction logic for MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        private UserControl controlPrincipal;
        private bool collapse = false; // eval l'état du menu
        private Plateau ActualPlateau;

        public MenuWindow()
        {
            InitializeComponent();
            controlPrincipal = new Acceuil();
            this.ContentPrincipal.Content = controlPrincipal;

        }

        private void ButtonMenu_Click(object sender, RoutedEventArgs e)
        {
            // Collapse et ouvrir menu
            if(collapse)
            {
                this.c1.Width = new GridLength(15, GridUnitType.Star);
                this.c2.Width = new GridLength(85, GridUnitType.Star);
                collapse = false;
            }
            else
            {
                this.c1.Width = new GridLength(0, GridUnitType.Star);
                this.c2.Width = new GridLength(100, GridUnitType.Star);
                collapse = true;
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        /// <summary>
        /// Gestion de la sélection dans le menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count > 0)
            {
                ListViewItem viewItem = (ListViewItem)e.AddedItems[0];
                switch (int.Parse(viewItem.Tag.ToString()))
                {
                    case 0: //Acceuil
                        controlPrincipal = new Acceuil();
                        this.ContentPrincipal.Content = controlPrincipal;
                        break;
                    case 1: // Connexion Serveur
                        controlPrincipal = new Parties();
                        this.ContentPrincipal.Content = controlPrincipal;
                        break;
                    case 2: // Regles
                        controlPrincipal = new Regles();
                        this.ContentPrincipal.Content = controlPrincipal;
                        break;
                    case 3: // Plateau
                        controlPrincipal = ActualPlateau;
                        this.ContentPrincipal.Content = controlPrincipal;
                        break;
                    default:

                        break;
                }
            }
        }

        public void ChangeMenuToPlateau(HelperServeur h)
        {
            ActualPlateau = new Plateau(h);
            this.ContentPrincipal.Content = ActualPlateau;
            listViewItemPlateau.IsEnabled = true;
        }


        /// <summary>
        ///  Deplacement de la fenetre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
