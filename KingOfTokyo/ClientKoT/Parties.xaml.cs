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
    /// Interaction logic for Parties.xaml
    /// </summary>
    public partial class Parties : Page
    {
        public Parties()
        {
            InitializeComponent();


            // Binding du nombre de joueurs dans une partie
            GridViewColumn gvc1 = new GridViewColumn
            {
                DisplayMemberBinding = new Binding("NbJoueurs"),
                Header = "NbJoueurs",
                Width = 200
            };

            // Binding du nombre du nom des parties
            GridViewColumn gvc2 = new GridViewColumn
            {
                DisplayMemberBinding = new Binding("NomPartie"),
                Header = "NomPartie",
                Width = 200
            };
        }
    }
}
