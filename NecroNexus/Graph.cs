using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    public class Graph<T>
    {

        /* NODE LIST

       The list containing our nodes, built into a property.
       Every class can now access the list, but only Graph can manipulate, as the setter is private.

       */
        public List<Node<T>> NodesList { get; private set; } = new List<Node<T>>();

        /// <summary>
        /// ADD NODE: 
        /// 
        /// En metode som kan bruges i Map klassen til at skabe nye noder. 
        /// Denne har vi lavet generisk lige som resten af klassen, 
        /// for at gøre den mere compatible med resten af spillet.
        /// </summary>
        public void AddNode(T data, Vector2 nodePosition)
        {
            NodesList.Add(new Node<T>(data, nodePosition));
        }

        /// <summary>
        /// ADD EDGE:
        /// Indsæt to noder (TO og FROM). Metoden skaber en NY EDGE mellem dem som går begge veje. 
        /// </summary>
        public void AddNewEdge(T from, T to, bool WallBuilt)
        {
            // Vi søger efter de specifikke noder med en lambda expression sammenligning:
            Node<T> fromNode = NodesList.Find(x => x.Data.Equals(from));

            Node<T> toNode = NodesList.Find(x => x.Data.Equals(to));

            /* Vi laver en directed edge hvor "fromNode" tager pladsen som "this"
            og "toNode" tager pladsen som "other". Det sker kun hvis de ikke er null. 
            Dvs hvis noden ikke er sat, kan den ikke lave en edge af den. */

            if (!fromNode.Equals(default(T)) && !toNode.Equals(default(T)))
            {
                fromNode.AddEdge(toNode, WallBuilt);
                toNode.AddEdge(fromNode, WallBuilt);
            }
            


        }

        /// <summary>
        /// Hider edges fra listen ved at sætte en væg. 
        /// USE: From node string, To node string, og så en True eller False på om denne edge har en væg eller ej. */
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="wallBuilt"></param>
        public void BuildWall(T from, T to, bool wallBuilt)
        {
            Node<T> fromNode = NodesList.Find(x => x.Data.Equals(from));
            Node<T> toNode = NodesList.Find(x => x.Data.Equals(to));

            if (fromNode != null && toNode != null)
            {
                Edge<T> edge = fromNode.EdgesList.Find(e => e.To == toNode);
                if (edge != null)
                {
                    edge.WallBuilt = wallBuilt;
                }
            }
        }


    }
}
