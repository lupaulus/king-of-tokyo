using Client.Reseau;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Parties.xaml
    /// </summary>
    public partial class Parties : UserControl
    {
        private List<HelperServeur> listServeur { get; set; }

        public Parties()
        {

            InitializeComponent();

            listServeur = new List<HelperServeur>();
            //Serveur Add serveur par défaut

            // Binding du nombre de joueurs dans une partie
            listServeur.Add(new HelperServeur("J1","localhost", "127.0.0.1", 13670));
            lvServeur.ItemsSource = listServeur;
        }

        private void ButtonCreerServeur_Click(object sender, RoutedEventArgs e)
        {
            // Récupération des champs
            string txtAdresse = tbAdresseServeur.Text.Trim();
            string txtNom = tbNomServeur.Text.Trim();

            // Verification des champs
            if (txtAdresse.Equals(String.Empty))
            {
                MessageBox.Show("Erreur saisie Adresse", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txtNom.Equals(String.Empty))
            {
                MessageBox.Show("Erreur saisie Nom", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Creation du serveur dans la liste
            listServeur.Add(new HelperServeur("J1",txtNom, txtAdresse, 13670)); // Serveur par defaut
            MessageBox.Show("Serveur ajouté", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            lvServeur.Items.Refresh();
        }

        private void ButtonConnexion_Click(object sender, RoutedEventArgs e)
        {
            if(lvServeur.SelectedItem == null)
            {
                MessageBox.Show("Merci de selectionner un serveur", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            HelperServeur val = (HelperServeur)lvServeur.SelectedItem;
            try
            {
                val.InitConnexion();
                if(val.CheckServeurRep()) // Réponse ok
                {
                    Window.GetWindow(this).Content = new Plateau();
                }
                else // Si reponse not ok
                {
                    MessageBox.Show("Le serveur ne vous a pas accepter", "Cheh", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Erreur Connexion : {ex.ToString()}", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
    }
}
