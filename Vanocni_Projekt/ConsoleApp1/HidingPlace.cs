using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class HidingPlace
    {
        private string location;
        

        public string Location { get => location; set => location = value; }

        /// <summary>
        /// Konstruktor skrýše.
        /// </summary>
        /// <param name="location">Název skrýše.</param>
        public HidingPlace(string location) { 
            this.location = location;
          
        }
    }
}
