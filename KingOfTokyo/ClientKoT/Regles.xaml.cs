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
    public partial class Regles : UserControl
    {
        public Regles()
        {
            InitializeComponent();

            // pdfWebViewer.Navigate(new Uri("about:blank"));
            // pdfWebViewer.Navigate("https://ludos.brussels/ludo-cocof/opac_css/doc_num.php?explnum_id=918");

            // Pour chez moi. Il faut IE + addon Adobe.
            // pdfWebViewer.Navigate(new Uri("file:///D:/Projet%20POO/king-of-tokyo/rules/rules_short.pdf"));


        }
    }
}
