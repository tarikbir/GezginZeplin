using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GezginZeplin
{
    class Node
    {

        City nodeCity;
        LinkedList<Node> adjacent = new LinkedList<Node>();

        public Node(City nodeCity)
        {
            this.nodeCity = nodeCity;
        }

        public void addConnection(Node node)
        {
            
        }
    }
}
