using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    internal class Graph<T>
    {
        /// <summary>
        /// NODE LIST
        /// The list containing our nodes, built into a property.
        /// Every class can now access the list, but only Graph can manipulate, as the setter is private.
        /// </summary>

        public List<Node<T>> NodesList { get; private set; } = new List<Node<T>>();

    }
}
