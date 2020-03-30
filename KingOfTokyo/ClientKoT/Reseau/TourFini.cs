
using System;
using System.Collections.Generic;

namespace Client.Reseau
{

    public class TourFini : StreamObject
    {
        private string s;

        public TourFini()
        {
        }

        public TourFini(string s)
        {
            this.s = s;
        }

        public override string IntoString()
        {
            return $"";
        }

       
    }
}