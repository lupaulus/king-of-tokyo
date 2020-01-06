
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
using static ServeurKoT.Modele.CartePouvoir;

namespace ServeurKoT.Controleur{
    public class FactoryCartePouvoir : FactoryCarte {

        public FactoryCartePouvoir() : base() {}


        public static string FileName = "Cartes_tokyo.xls";

        public override List<Carte> AjouterCarte()
        {

            var fileName = string.Format("{0}\\{1}", Directory.GetCurrentDirectory(), FileName);
            var connectionString = string.Format("Provider = Microsoft.Jet.OLEDB.4.0; data source = {0}; Extended Properties = Excel 8.0; ", fileName);

            OleDbConnection dbConnection = new OleDbConnection(connectionString);

            var adapter = new OleDbDataAdapter("SELECT * FROM [Pouvoirs$]", connectionString);
            var ds = new DataSet();
            adapter.Fill(ds, "Pouvoirs");
            DataTable dataPouvoirs = ds.Tables["Pouvoirs"];



            //**************
            //Cartes Actions
            //**************


            List<Carte> list = new List<Carte>();

            foreach (DataRow dr in dataPouvoirs.Rows)
            {
                if (!dataPouvoirs.Rows[0].Equals(dr))
                {
                    
                    // Base d'une carte
                    string name = dr[0].ToString();
                    int coutEnergie = Int32.Parse(dr[1].ToString());
                    string description = dr[2].ToString();
                    string image = dr[3].ToString();


                    CartePouvoir carte = new CartePouvoir(name, coutEnergie, description, image);

                    //int nombreDeCartes = Int32.Parse(dr[8].ToString());

                    //for (int i = 0; i < nombreDeCartes; i++)
                    //{
                    list.Add(carte);
                    //}



                }

            }
            dbConnection.Dispose();
            return list;

        }

    }
}