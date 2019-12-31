
using ServeurKoT.Modele;
using SimpleLogger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ServeurKoT.Controleur{
    public class FactoryCarteAction : FactoryCarte {

        public FactoryCarteAction() : base() 
        {
        }

        public override List<Carte> AjouterCarte(string filename)
        {

            // Create an instance of the XmlSerializer class;
            // specify the type of object to be deserialized.
            XmlSerializer serializer = new XmlSerializer(typeof(CarteActionCollection));
            /* If the XML document has been altered with unknown 
            nodes or attributes, handle them with the 
            UnknownNode and UnknownAttribute events.*/
            serializer.UnknownNode += new XmlNodeEventHandler(serializer_UnknownNode);
            serializer.UnknownAttribute += new XmlAttributeEventHandler(serializer_UnknownAttribute);

            // A FileStream is needed to read the XML document.
            FileStream fs = new FileStream(filename, FileMode.Open);
            // Declare an object variable of the type to be deserialized.
            CarteActionCollection ct;
            /* Use the Deserialize method to restore the object's state with
            data from the XML document. */
            ct = (CarteActionCollection)serializer.Deserialize(fs);
            
            // C'est pas beau mais devrait marcher
            return ct.Cartes.Cast<Carte>().ToList();
        }

        private void serializer_UnknownNode(object sender, XmlNodeEventArgs e)
        {
            Logger.Log("Unknown Node:" + e.Name + "\t" + e.Text);
        }

        private void serializer_UnknownAttribute(object sender, XmlAttributeEventArgs e)
        {
            System.Xml.XmlAttribute attr = e.Attr;
            Logger.Log("Unknown attribute " + attr.Name + "='" + attr.Value + "'");
        }


    }
}