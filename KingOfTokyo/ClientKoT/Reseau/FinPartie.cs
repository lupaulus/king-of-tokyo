using System;
using System.Collections.Generic;

namespace Client.Reseau
{

    public class FinPartie : StreamObject
    {
        private string s;

        public FinPartie()
        {
        }

        public FinPartie(string s)
        {
            this.s = s;
        }

        public override string IntoString()
        {
            return $"";
        }

       
    }
}