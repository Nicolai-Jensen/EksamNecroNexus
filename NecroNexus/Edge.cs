using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    internal class Edge<T>
    {
        public Node<T> From { get; private set; }

        public Node<T> To { get; private set; }

        //Edge constructor 
        public Edge(Node<T> From, Node<T> To)
        {
            this.From = From;
            this.To = To;
        }

    }
}
