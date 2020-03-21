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

        public MenuWindow()
        {
            InitializeComponent();
            controlPrincipal = new Acceuil();
            this.ContentPrincipal.Content = controlPrincipal;
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonMenu_Click(object sender, RoutedEventArgs e)
        {
            // Collapse et ouvrir menu
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
                        controlPrincipal = new Plateau();
                        this.ContentPrincipal.Content = controlPrincipal;
                        break;
                    default:

                        break;
                }
            }
        }

    }
}
