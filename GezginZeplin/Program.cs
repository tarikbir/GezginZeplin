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
            StreamReader adjecent = new StreamReader("adjacent.txt");
            StreamReader cities = new StreamReader("cities.txt");
            cities.ReadLine();

            while (!cities.EndOfStream)
            {
                string line = cities.ReadLine();
                Console.WriteLine(line);
            }
        }
    }
}
