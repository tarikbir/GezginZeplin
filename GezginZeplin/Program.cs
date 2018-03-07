using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GezginZeplin
{
    class Program
    {
        public static Node[] cityArray = new Node[81];
        public static Stopwatch stopWatch = new Stopwatch();

        public static Node findCity(int plate)
        {
            //Console.WriteLine("findCity() searching for :" + plate);
            for (int i = 0; i < cityArray.Length; i++)
            {
                if (cityArray[i].city.plate == plate)
                    return cityArray[i];
            }
            return null;
        }

        public static double distanceHorizontal(LinkedList<Node> route)
        {
            double total=0;
            for (int i = 1; i < route.Count; i++)
            {
                total += route.ElementAt(i).distanceTo(route.ElementAt(i - 1));
            }
            return total;
        }

        public static double distanceZeppelin(LinkedList<Node> route, int passenger)
        {
            if (route == null) return Double.MaxValue;
            double total = 0;
            Node end = route.ElementAt(0);
            Node start = route.ElementAt(route.Count - 1);
            for (int i = 1; i < route.Count; i++)
            {
                double distance, h;
                Node current = route.ElementAt(i-1);
                Node next = route.ElementAt(i);
                double d = current.distanceTo(next);
                if (current.city.plate == start.city.plate) //First city flight height
                {
                    h = Math.Abs(current.city.altitude - next.city.altitude - 50);
                }
                else if (current.city.plate == end.city.plate) //Last city flight height
                {
                    h = Math.Abs(current.city.altitude - next.city.altitude + 50);
                }
                else //Cities in-between
                {
                    h = Math.Abs(current.city.altitude - next.city.altitude);
                }
                double angle = Math.Atan(h / d) / Math.PI * 180;
                if (angle < (80 - passenger)) //Zeppelin can travel with load
                {
                    distance = Math.Sqrt((h * h) + (d * d));
                }
                else //Zeppelin can't travel with load
                {
                    distance = Double.MaxValue;
                }
                total += distance;
            }
            return total;
        }

        public static LinkedList<Node> shortestPath(Node start, Node end, int passenger)
        {
            double[,] weights = new double[81,2]; //PLATE, WEIGHT/PREV.NODE
            bool[] visited = new bool[81];
            bool done = false;
            Node current = start;
            LinkedList<Node> citiesToReturn = new LinkedList<Node>();
            //Initialize
            for (int i = 0; i < 81; i++){ weights[i,0] = Double.MaxValue; }
            weights[current.city.plate - 1, 0] = 0;
            weights[current.city.plate - 1, 1] = current.city.plate;
            visited[current.city.plate - 1] = true;
            //Console.WriteLine("DEBUG: Start plate: " + current.city.plate + " | End plate: " + end.city.plate);
            //Main loop
            int debugLoopCounter = 0;
            while (!done)
            {
                //Getting the weights of adjacent cities
                int adjCount = current.adjacent.Count;
                for (int i = 0; i < adjCount; i++)
                {
                    Node adjNode = findCity(current.adjacent.ElementAt(i));
                    City adjCity = adjNode.city;
                    int adjPlate = adjCity.plate;
                    double d = current.distanceTo(adjNode); //Horizontal distance the zeppelin MUST travel (h)
                    double h, distance;
                    if (current.city.plate == start.city.plate) //First city flight height
                    {
                        h = Math.Abs(current.city.altitude - adjCity.altitude - 50);
                    }
                    else if (current.city.plate == end.city.plate) //Last city flight height
                    {
                        h = Math.Abs(current.city.altitude - adjCity.altitude + 50);
                    }
                    else //Cities in-between
                    {
                        h = Math.Abs(current.city.altitude - adjCity.altitude);
                    }
                    double angle = Math.Atan(h / d)/Math.PI*180;
                    //Console.WriteLine("DEBUG: Angle from "+current.city.plate+" to "+adjPlate+": " + angle);
                    if (angle < (80-passenger)) //Zeppelin can travel with load
                    {
                        distance = Math.Sqrt((h * h) + (d * d));
                    }
                    else //Zeppelin can't travel with load
                    {
                        distance = 99999d;
                    }

                    if (distance+weights[current.city.plate - 1, 0] < weights[adjPlate-1, 0] && !visited[adjPlate-1])
                    {
                        weights[adjPlate - 1, 0] = weights[current.city.plate-1,0]+distance; //WEIGHT CALCULATION
                        weights[adjPlate - 1, 1] = current.city.plate; //FROM
                        //Console.WriteLine("DEBUG: Adjacent (" + current.city.plate + ") found at: " + adjPlate + " by the distance of " + weights[adjPlate - 1, 0] + " kms");
                    }
                }

                //Find the minimum weight
                double min = Double.MaxValue;
                int nextPlate = current.city.plate;
                for (int i = 0; i < cityArray.Length; i++)
                {
                    if (weights[i,0] < min && !visited[i])
                    {
                        min = weights[i, 0];
                        nextPlate = i+1;
                    }
                }
                //Console.WriteLine("Next city is: " + nextPlate+"\n");

                //Initialize next iteration.
                visited[nextPlate - 1] = true;
                current = findCity(nextPlate);

                //Check for the end of the loop (Travel to each city in array).
                done = true;
                for (int i = 0; i < cityArray.Length; i++){ if (!visited[i]) { done = false;  break; } }
                debugLoopCounter++;
            }
            Console.WriteLine("Debug loop counter: " + debugLoopCounter);
            //Output
            int endPlate = end.city.plate;
            int writingPlate = endPlate;
            //Console.WriteLine("Fastest route: \n");
            //Console.Write(writingPlate + " ");
            citiesToReturn.AddLast(findCity(writingPlate));

            do
            {
                writingPlate = (int)weights[writingPlate - 1, 1];
                citiesToReturn.AddLast(findCity(writingPlate));
                //Console.Write(writingPlate+" ");
            } while (writingPlate != start.city.plate);
            if (distanceZeppelin(citiesToReturn, passenger) >= Double.MaxValue) return null;
            return citiesToReturn;
        }

        public static string getList(LinkedList<Node> array)
        {
            if (array == null) return "Cannot travel.";
            string s = "";
            int n = array.Count;
            for (int i = n-1; i > 0; i--)
            {
                s = s + array.ElementAt(i).ToString + " > "; //City plates except 0.
            }
            s = s + array.ElementAt(0).ToString; //Last one without the character ">".
            Console.WriteLine("DEBUG: getList() returns :"+s);
            return s;
        }

        static void Main(string[] args)
        {
            //Initialization
            string citiesFileName = "cities.txt";
            string adjacentFileName = "adjacent.txt";
            stopWatch.Start();

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

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Menu());

            //END OF PROGRAM
            Console.Read();
        }
    }
}
