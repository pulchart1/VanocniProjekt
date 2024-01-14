using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    public class Friend
    {
        private string name;
        private string present;
        private Dictionary<string, int> hidingPlaceProbabilities;

        public string Name { get => name; set => name = value; }
        public string Present { get => present; set => present = value; }
        public Dictionary<string, int> HidingPlaceProbabilities { get => hidingPlaceProbabilities; set => hidingPlaceProbabilities = value; }
        /// <summary>
        /// Konstruktor přítele.
        /// </summary>
        /// <param name="name">Jméno přítele.</param>
        /// <param name="present">Dárek pro přítele.</param>
        /// <param name="hidingPlaceProbabilities">Pravděpodobnosti nalezení dárku v různých skrýších.</param>
        public Friend(string name, string present, Dictionary<string, int> hidingPlaceProbabilities)
        {
            this.name = name;
            this.present = present;
            this.hidingPlaceProbabilities = hidingPlaceProbabilities;
        }
    }
}

