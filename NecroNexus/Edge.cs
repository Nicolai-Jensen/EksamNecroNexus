using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace NecroNexus
{
    public class Edge<T>
    {
        public Node<T> From { get; set; }

        public Node<T> To { get; set; }

        public bool WallBuilt { get; set; }

        //Edge constructor 
        public Edge(Node<T> From, Node<T> To, bool WallBuilt)
        {
            this.From = From;
            this.To = To;
            this.WallBuilt = WallBuilt;
        }

    }
}
