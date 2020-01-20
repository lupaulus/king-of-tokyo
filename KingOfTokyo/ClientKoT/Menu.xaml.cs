using Client.Reseau;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Page
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void ButtonConnect_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxPseudo.Text.Length == 0)
            {
                errormessage.Text = "Entrer un Pseudonyme.";
                textBoxPseudo.Focus();
            }
            if (textBoxIP.Text.Length == 0)
            {
                errormessage.Text = "Entrer une IP.";
                textBoxIP.Focus();
            }
            if (textBoxPort.Text.Length == 0)
            {
                errormessage.Text = "Entrer un Port.";
                textBoxPort.Focus();
            }
            if (!Regex.IsMatch(textBoxIP.Text, @"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$"))
            {
                errormessage.Text = "Entrer une adresse IP valide.";
                textBoxIP.Select(0, textBoxIP.Text.Length);
                textBoxIP.Focus();
            }
            if (!Regex.IsMatch(textBoxPort.Text, @"^([0-9]{1,4}|[1-5][0-9]{4}|6[0-4][0-9]{3}|65[0-4][0-9]{2}|655[0-2][0-9]|6553[0-5])$"))
            {
                errormessage.Text = "Entrer un Port valide.";
                textBoxPort.Select(0, textBoxPort.Text.Length);
                textBoxPort.Focus();
            }

            string pseudo = textBoxPseudo.Text;
            string ip = textBoxIP.Text;
            int port = int.Parse(textBoxPort.Text);
            // TODO: Validations -> ip = ipduserv() -> port = ... etc
        }
    }
}
