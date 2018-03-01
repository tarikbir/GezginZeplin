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
        public static Node[] cityArray = new Node[81];

        public static Node findCity(int plate)
        {
            for (int i = 0; i < cityArray.Length; i++)
            {
                if (cityArray[i].city.plate == plate)
                    return cityArray[i];
            }
            return null;
        }

        public static void shortestPath(Node start, Node end)
        {
            double[,] weights = new double[81,2]; //CITY, INFO
            bool[] visited = new bool[81];
            bool done = false;
            Node current = start;
            //Initialize
            for (int i = 0; i < 81; i++){ weights[i,0] = Double.MaxValue; }
            weights[current.city.plate - 1, 0] = 0;
            weights[current.city.plate - 1, 1] = current.city.plate;
            visited[current.city.plate - 1] = true;
            Console.WriteLine("Start plate: " + current.city.plate + " End plate: " + end.city.plate);
            //Main loop
            while (!done)
            {
                int nextPlate = current.city.plate;
                Console.WriteLine();
                //Getting the weights of path
                for (int i = 0; i < current.adjacent.Count; i++)
                {
                    int thisPlate = current.adjacent.ElementAt(i);
                    double distance = current.distanceTo(findCity(thisPlate));
                    if (distance < weights[thisPlate-1, 0])
                    {
                        weights[thisPlate - 1, 0] = weights[thisPlate-1,0]+distance; //WEIGHT CALCULATION
                        weights[thisPlate - 1, 1] = current.city.plate; //FROM
                    }
                    Console.WriteLine("Adjacent (" + current.city.plate + ") found at: " + thisPlate+" by the distance of "+distance+" kms");
                }
                //Find the minimum weight
                double min = Double.MaxValue;
                for (int i = 0; i < 81; i++)
                {
                    if (weights[i,0] < min && !visited[i])
                    {
                        weights[i, 0] = min;
                        nextPlate = i+1;
                    }
                }
                Console.WriteLine("Next city is: " + nextPlate);
                //Initialize next iteration.
                visited[nextPlate - 1] = true;
                current = findCity(nextPlate);
                //Check for the end of the loop.
                done = true;
                for (int i = 0; i < 81; i++){ if (weights[i, 0] == Double.MaxValue) { done = false;  break; } }
            }
        }

        static void Main(string[] args)
        {
            //Initialization
            string citiesFileName = "cities.txt";
            string adjacentFileName = "adjacent.txt";

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
                    City city = new City(tempString[0] / 10000, tempString[1] / 10000, (int)tempString[2], (int)tempString[3]);
                    cityArray[i++] = new Node(city);
                }
            }
            
            //File reading, connecting nodes
            using (StreamReader adjacent = new StreamReader(adjacentFileName))
            {
                adjacent.ReadLine(); //Pass the first line
                int i = 0;
                while (!adjacent.EndOfStream)
                {
                    string line = adjacent.ReadLine();
                    int[] tempString = Array.ConvertAll(line.Split(','),Int32.Parse);
                    for (int p = 1; p < tempString.Length; p++)
                    {
                        cityArray[tempString[0] - 1].addConnection(findCity(tempString[p]));
                    }
                }
            }

            shortestPath(cityArray[33], cityArray[21]);

            //END OF PROGRAM
            Console.Read();
        }
    }
}
