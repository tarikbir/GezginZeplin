using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GezginZeplin
{
    class Node
    {

        public City nodeCity;
        public LinkedList<Node> adjacent = new LinkedList<Node>();

        public Node(City nodeCity)
        {
            this.nodeCity = nodeCity;
        }

        public void addConnection(Node node)
        {
            adjacent.AddLast(node);
        }

        public void showConnection()
        {
            Console.Write(adjacent);
            for (int i = 0; i < adjacent.Count; i++)
            {
                Console.Write(adjacent.ElementAt(i).nodeCity.plate + " ");
            }
        }

        public string ToString => nodeCity.plate.ToString();

        public double distanceTo(Node node)
        {
            double p = Math.PI / 180;
            double a = 0.5 - Math.Cos((node.nodeCity.lat - nodeCity.lat) * p) / 2 + Math.Cos(node.nodeCity.lat * p) * Math.Cos(nodeCity.lat * p) * (1 - Math.Cos((node.nodeCity.lng - nodeCity.lng) * p)) / 2;
            double distance = 12742 * Math.Asin(Math.Sqrt(a));
            Console.WriteLine(distance); //DEBUG, WILL DELETE
            return distance;
        }
    }
}
