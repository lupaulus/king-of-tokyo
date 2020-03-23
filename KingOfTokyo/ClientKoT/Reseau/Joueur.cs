
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.Reseau
{
    public class Joueur : StreamObject{

        private string pseudo;
        public Joueur(string ps) 
        {
            pseudo = ps;
        }

        public override string IntoString()
        {
            return new string();
        }
    }
}