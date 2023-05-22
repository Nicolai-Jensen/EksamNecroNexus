using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    internal class Node<T>
    {

        public T Data { get; private set; } 

        // EDGE LIST
        public List<Edge<T>> EdgesList { get; private set; } = new List<Edge<T>>();

        //Constructor 
        public Node(T data)
        {
            this.Data = data;
        }


    }
}
