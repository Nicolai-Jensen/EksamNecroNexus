using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    internal class Node<T>
    {

        public T Data { get; private set; } 

        // EDGE LIST
        public List<Edge<T>> EdgesList { get; private set; } = new List<Edge<T>>();

        public bool Discovered { get; set; } = false;

        public Vector2 NodePosition { get; private set; }

        public Node<T> Parent { get; set; }

        //Constructor 
        public Node(T data, Vector2 nodePosition)
        {
            this.Data = data;
            this.NodePosition = nodePosition;
        }

        /// <summary>
        /// ADD EDGE: 
        /// Skaber en edge fra "this" node, til noden skrevet i parametren som "other".
        /// </summary>
        public void AddEdge(Node<T> other)
        {
            EdgesList.Add(new Edge<T>(this, other));

        }


    }
}
