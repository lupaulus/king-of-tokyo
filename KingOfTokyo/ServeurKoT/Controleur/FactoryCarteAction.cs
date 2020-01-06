
using ServeurKoT.Modele;
using SimpleLogger;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ServeurKoT.Controleur{
    public class FactoryCarteAction : FactoryCarte {

        public FactoryCarteAction() : base() 
        {
        }


        public static string FileName = "Cartes_tokyo.xls";

        public override List<Carte> AjouterCarte()
        {

            var fileName = string.Format("{0}\\{1}", Directory.GetCurrentDirectory(), FileName);
            var connectionString = string.Format("Provider = Microsoft.Jet.OLEDB.4.0; data source = {0}; Extended Properties = Excel 8.0; ", fileName);

            OleDbConnection dbConnection = new OleDbConnection(connectionString);

            var adapter2 = new OleDbDataAdapter("SELECT * FROM [Actions$]", connectionString);
            var ds = new DataSet();
            adapter2.Fill(ds, "Actions");
            DataTable dataActions = ds.Tables["Actions"];


            //**************
            //Cartes Actions
            //**************


            List<Carte> list = new List<Carte>();

            foreach (DataRow dr in dataActions.Rows)
            {
                if (!dataActions.Rows[0].Equals(dr))
                {

                    // Base d'une carte
                    string name = dr[0].ToString();
                    int coutEnergie = Int32.Parse(dr[1].ToString());
                    string description = dr[2].ToString();
                    string image = dr[3].ToString();

                    // Effet PdVie
                    int effectValuePdVie = Int32.Parse(dr[4].ToString());
                    // Effet PdVictoire
                    int effectValuePdVictoire = Int32.Parse(dr[5].ToString());
                    // Effet PdEnergie
                    int effectValuePdEnergie = Int32.Parse(dr[6].ToString());

                    // Gestion du dictionnaires
                    Dictionary<Effet, int> keyValues = new Dictionary<Effet, int>();
                    keyValues.Add(Effet.PointDeVie, effectValuePdVie);
                    keyValues.Add(Effet.Energie, effectValuePdEnergie);
                    keyValues.Add(Effet.PointDeVictoire, effectValuePdVictoire);

                    // Cartes Spéciale si 0 Carte normale 
                    // SI 1 : Tour supplémentaire
                    // SI 2 : /2 par point energie adversaire
                    // SI 3 : Rentre dans Paris
                    int specialCard = Int32.Parse(dr[7].ToString());


                    // Cible effet
                    ApplicationEffet applicationEffect;
                    switch (Int32.Parse(dr[8].ToString()))
                    {
                        case 1:
                            applicationEffect = ApplicationEffet.SurMoi;
                            break;
                        case 2:
                            applicationEffect = ApplicationEffet.SurAutreMonstre;
                            break;
                        case 3:
                            applicationEffect = ApplicationEffet.ToutLeMonde;
                            break;
                        default:
                            throw new ArgumentException("Erreur lecture fichier Application effet");
                    }



                    CarteAction carte = new CarteAction(name, coutEnergie, description, image, keyValues,
                        applicationEffect, specialCard);
                    int nombreDeCartes = Int32.Parse(dr[8].ToString());

                    for (int i = 0; i < nombreDeCartes; i++)
                    {
                        list.Add(carte);
                    }

                    

                }

            }
            dbConnection.Dispose();
            return list;

            

        }
    }
}

