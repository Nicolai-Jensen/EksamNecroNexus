using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    internal class Graph<T>
    {
       
        /* NODE LIST
         
        The list containing our nodes, built into a property.
        Every class can now access the list, but only Graph can manipulate, as the setter is private.

        */
        public List<Node<T>> NodesList { get; private set; } = new List<Node<T>>();

        /// <summary>
        /// ADD NODE: 
        /// 
        /// En metode som kan bruges i nodeklassen til at skabe nye noder. 
        /// Denne har vi lavet generisk lige som resten af klassen, 
        /// for at gøre den mere compatible med resten af spillet.
        /// </summary>
        public void AddNode(T data)
        {
            NodesList.Add(new Node<T>(data));
        }
    }
}
