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
            StreamReader adjacent = new StreamReader("adjacent.txt");
            StreamReader cities = new StreamReader("cities.txt");
            cities.ReadLine();

            while (!cities.EndOfStream)
            {
                string line = cities.ReadLine();
                double[] tempString = Array.ConvertAll(line.Split(','), Double.Parse);
                City c = new City(tempString[0]/10000,tempString[1]/10000,(int)tempString[2],(int)tempString[3]);
                c.writeCity(); //DEBUG
            }

            Console.Read();
        }
    }
}
