using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class Dictelement
    {

        public int ID { get; set; }
        public string Keyelement { get; set; }
        public string Valueelement { get; set; }


        public Dictelement(string keyelement, string valueelement)
        {
            Keyelement = keyelement;
            Valueelement = valueelement;
        }

    }



}
