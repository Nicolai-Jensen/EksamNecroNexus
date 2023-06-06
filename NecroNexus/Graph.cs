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
       
        
        public List<Node<T>> NodesList { get; private set; } = new List<Node<T>>();

        public void AddNode(T data, Vector2 nodePosition)
        {
            NodesList.Add(new Node<T>(data, nodePosition));
        }

        public void AddNewEdge(T from, T to, bool WallBuilt)
        {
            // Vi søger efter de specifikke noder med en lambda expression sammenligning:
            Node<T> fromNode = NodesList.Find(x => x.Data.Equals(from));

            Node<T> toNode = NodesList.Find(x => x.Data.Equals(to));

            if (!fromNode.Equals(default(T)) && !toNode.Equals(default(T)))
            {
                fromNode.AddEdge(toNode, WallBuilt);
                toNode.AddEdge(fromNode, WallBuilt);
            }
            


        }

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
