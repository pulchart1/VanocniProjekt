using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main()
        {
            WriteFriendsToFile();
            WriteHidingPlacesToFile();
            Console.WriteLine("Data byla zapsána do souborů.");
            List<Friend> friends = ReadFriendsFromFile("friends.txt");
            List<HidingPlace> hidingPlaces = ReadHidingPlacesFromFile("hidingPlaces.txt");
            OptimizeGiftPlacement(friends, hidingPlaces);
            Console.WriteLine("Optimalizace dokončena. Výsledek uložen do gift_placement.txt.");
        }
        /// <summary>
        /// Metoda pro zápis přátel do souboru.
        /// </summary>
        static void WriteFriendsToFile()
        {
            Friend f = new Friend("Pavel", "Pocitac", new Dictionary<string, int> { { "Pod Posteli", 20 }, { "Kuchyň", 30 }, { "Koupelna", 60 }, { "Šatna", 10 } });
            Friend f2 = new Friend("Marek", "Kolo", new Dictionary<string, int> { { "Pod Posteli", 30 }, { "Kuchyň", 70 }, { "Koupelna", 60 }, { "Šatna", 85 } });
            Friend f3 = new Friend("Honza", "Ponozky", new Dictionary<string, int> { { "Pod Posteli", 25 }, { "Kuchyň", 45 }, { "Koupelna", 95 }, { "Šatna", 15 } });
            Friend f4 = new Friend("Pepa", "Sluchatka", new Dictionary<string, int> { { "Pod Posteli", 40 }, { "Kuchyň", 95 }, { "Koupelna", 95 }, { "Šatna", 45 } });

            using (StreamWriter friendWriter = new StreamWriter("friends.txt"))
            {
                WriteFriendToFile(f, friendWriter);
                WriteFriendToFile(f2, friendWriter);
                WriteFriendToFile(f3, friendWriter);
                WriteFriendToFile(f4, friendWriter);
            }
        }
        /// <summary>
        /// Metoda pro zápis jednoho přítele do souboru.
        /// </summary>
        /// <param name="friend">Přítel, který se má zapsat.</param>
        /// <param name="writer">StreamWriter pro zápis do souboru.</param>
        static void WriteFriendToFile(Friend friend, StreamWriter writer)
        {
            foreach (var entry in friend.HidingPlaceProbabilities)
            {
                writer.WriteLine($"{friend.Name},{friend.Present},{entry.Key},{entry.Value}");
            }
        }
        /// <summary>
        /// Metoda pro zápis skrýší do souboru.
        /// </summary>
        static void WriteHidingPlacesToFile()
        { 
            HidingPlace hp4 = new HidingPlace("Šatna");
            HidingPlace hp1 = new HidingPlace("Pod Posteli");
            HidingPlace hp2 = new HidingPlace("Kuchyň" );
            HidingPlace hp3 = new HidingPlace("Koupelna");
           

            using (StreamWriter hidingPlaceWriter = new StreamWriter("hidingPlaces.txt"))
            {
                WriteHidingPlaceToFile(hp1, hidingPlaceWriter);
                WriteHidingPlaceToFile(hp2, hidingPlaceWriter);
                WriteHidingPlaceToFile(hp3, hidingPlaceWriter);
                WriteHidingPlaceToFile(hp4, hidingPlaceWriter);
            }
        }
        /// <summary>
        /// Metoda pro zápis jedné skrýše do souboru.
        /// </summary>
        /// <param name="hidingPlace">Skrýš, která se má zapsat.</param>
        /// <param name="writer">StreamWriter pro zápis do souboru.</param>
        static void WriteHidingPlaceToFile(HidingPlace hidingPlace, StreamWriter writer)
        {
            writer.WriteLine($"{hidingPlace.Location}");
        }
        /// <summary>
        /// Metoda pro čtení přátel ze souboru.
        /// </summary>
        /// <param name="filePath">Cesta k souboru s přáteli.</param>
        /// <returns>Seznam přátel.</returns>
        static List<Friend> ReadFriendsFromFile(string filePath)
        {
            List<Friend> friends = new List<Friend>();
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    while (!reader.EndOfStream)
                    {
                        string[] friendData = reader.ReadLine().Split(',');
                        string name = friendData[0];
                        string present = friendData[1];
                        string location = friendData[2];
                        int probability = int.Parse(friendData[3]);

                        Friend friend = friends.FirstOrDefault(f => f.Name == name);
                        if (friend == null)
                        {
                            friend = new Friend(name, present, new Dictionary<string, int> { { location, probability } });
                            friends.Add(friend);
                        }
                        else
                        {
                            friend.HidingPlaceProbabilities.Add(location, probability);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return friends;
        }
        /// <summary>
        /// Metoda pro čtení skrýší ze souboru.
        /// </summary>
        /// <param name="filePath">Cesta k souboru se skrýšemi.</param>
        /// <returns>Seznam skrýší.</returns>
        static List<HidingPlace> ReadHidingPlacesFromFile(string filePath)
        {
            List<HidingPlace> hidingPlaces = new List<HidingPlace>();
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    while (!reader.EndOfStream)
                    {
                        string[] hidingPlaceData = reader.ReadLine().Split(',');
                        HidingPlace hidingPlace = new HidingPlace(hidingPlaceData[0]);
                        hidingPlaces.Add(hidingPlace);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return hidingPlaces;
        }
        /// <summary>
        /// Metoda pro nejvhodnější umístění dárků mezi přátele.
        /// </summary>
        /// <param name="friends">Seznam přátel.</param>
        /// <param name="hidingPlaces">Seznam skrýší.</param>
        static void OptimizeGiftPlacement(List<Friend> friends, List<HidingPlace> hidingPlaces)
        {
            foreach (var friend in friends)
            {
                var bestSpot = friend.HidingPlaceProbabilities.OrderByDescending(entry => entry.Value).Last();
                friend.HidingPlaceProbabilities[bestSpot.Key] = bestSpot.Value;

                Console.WriteLine($"{friend.Present} pro {friend.Name} schován do {bestSpot.Key}");
                using (StreamWriter writer = new StreamWriter("gift_placement.txt",true))
                {
                    writer.WriteLine($"{friend.Name},{friend.Present},{bestSpot.Key},{bestSpot.Value}");
                }
            }
        }


    }
}




        
    

