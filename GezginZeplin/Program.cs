using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GezginZeplin
{
    class Program
    {
        static void Main(string[] args)
        {
            //Initialization
            string citiesFileName = "cities.txt";
            string adjacentFileName = "adjacent.txt";
            Node[] cityArray = new Node[81];

            //File checks
            if (!File.Exists(citiesFileName))
            {
                Console.WriteLine("Cities file not found... Press any key to exit.");
                Console.ReadKey();
                Environment.Exit(0);
            }
            if (!File.Exists(adjacentFileName))
            {
                Console.WriteLine("Adjacent file not found... Press any key to exit.");
                Console.ReadKey();
                Environment.Exit(0);
            }

            //File reading: Cities
            using (StreamReader cities = new StreamReader(citiesFileName))
            {
                cities.ReadLine(); //Pass the first line
                int i = 0;
                while (!cities.EndOfStream)
                {
                    string line = cities.ReadLine();
                    double[] tempString = Array.ConvertAll(line.Split(','), Double.Parse);
                    City c = new City(tempString[0] / 10000, tempString[1] / 10000, (int)tempString[2], (int)tempString[3]);
                    cityArray[i++] = new Node(c);
                }
            }

            cityArray[53].distanceTo(cityArray[40]); //DEBUG, WILL DLEETE
            /*
            //File reading, connecting nodes
            using (StreamReader adjacent = new StreamReader(adjacentFileName))
            {
                adjacent.ReadLine(); //Pass the first line
                int i = 0;
                while (!adjacent.EndOfStream)
                {
                    string line = adjacent.ReadLine();
                    double[] tempString = Array.ConvertAll(line.Split(','), Double.Parse);
                }
            }
            */
            //END OF PROGRAM
            Console.Read();
        }
    }
}
