using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GezginZeplin
{
    class Node
    {
        public City city;
        public LinkedList<int> adjacent = new LinkedList<int>();

        public Node(City city)
        {
            this.city = city;
        }

        public void addConnection(Node node)
        {
            adjacent.AddLast(node.city.plate);
        }

        public void showConnection()
        {
            for (int i = 0; i < adjacent.Count; i++)
            {
                Console.Write(adjacent.ElementAt(i) + " ");
            }
        }

        public string ToString => city.plate.ToString();

        public double distanceTo(Node node)
        {
            double p = Math.PI / 180;
            double a = 0.5 - Math.Cos((node.city.lat - city.lat) * p) / 2 + Math.Cos(node.city.lat * p) * Math.Cos(city.lat * p) * (1 - Math.Cos((node.city.lng - city.lng) * p)) / 2;
            double distance = 12742 * Math.Asin(Math.Sqrt(a));
            return distance;
        }

        public double distanceTo(City city)
        {
            double p = Math.PI / 180;
            double a = 0.5 - Math.Cos((city.lat - this.city.lat) * p) / 2 + Math.Cos(city.lat * p) * Math.Cos(this.city.lat * p) * (1 - Math.Cos((city.lng - this.city.lng) * p)) / 2;
            double distance = 12742 * Math.Asin(Math.Sqrt(a));
            return distance;
        }
    }
}
