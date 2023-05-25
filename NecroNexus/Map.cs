using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    public class Map
    {
        

        /*  Vi bygger graph1 instansen af Graph, som mapper ruten for bane nr 1. 
            Det er rammen som holder på vores noder og edges i denne bane.
            dvs at både noder og edges også bliver kreeret i denne metode.
            Heads up: Graph klassen er generisk.
            

            
        */
        public List<Node<string>> Graph1()
        {
            //instans af graph:
            Graph<string> graph1 = new Graph<string>();

            //nye nodes bundet op på den nye instans:
            
            graph1.AddNode("A", new Vector2(1725, 0)); //0 - Spawn
            graph1.AddNode("B", new Vector2(1575, 175)); //1
            graph1.AddNode("C", new Vector2(1700, 500)); //2 (off track triangle)
            graph1.AddNode("D", new Vector2(1400, 225)); //3
            graph1.AddNode("E", new Vector2(700, 225)); //4
            graph1.AddNode("F", new Vector2(375, 225)); //5 (off track rectangle)
            graph1.AddNode("G", new Vector2(375, 625)); //6 (off track rectangle)
            graph1.AddNode("H", new Vector2(700, 625)); //7
            graph1.AddNode("I", new Vector2(1400, 625)); //8 - Lair

            //Main track (No walls on map) Nodes:(A,B,D,E,H,I)
            graph1.AddNewEdge("A", "B");
            graph1.AddNewEdge("B", "D");
            graph1.AddNewEdge("D", "E");
            graph1.AddNewEdge("E", "H");
            graph1.AddNewEdge("H", "I");

            //off track Triangle (B,D Wall) Nodes:(C)
            graph1.AddNewEdge("B", "C");
            graph1.AddNewEdge("C", "D");

            //off track Rectangle (E,H Wall) Nodes: (F,G)
            graph1.AddNewEdge("E", "F");
            graph1.AddNewEdge("F", "G");
            graph1.AddNewEdge("G", "H");

            Node<string> n = BFS<string>(graph1.NodesList.Find(x => x.Data == "A"),
                                         graph1.NodesList.Find(x => x.Data == "I"));

            List<Node<string>> pathList = TrackPath<string>(n, graph1.NodesList.Find(x => x.Data == "A"));
            foreach (Node<string> pathNode in pathList)
            {
                //Console.WriteLine(pathNode.Data);
                //Console.WriteLine(pathNode.NodePosition);
                //Console.WriteLine();
                
                //Kan bruges i testprogram til at teste hvilke noder der passeres :) ^

            }
            return pathList;
        }

        public List<Vector2> ReturnPos(List<Node<string>> list)
        {
            List<Vector2> result = new List<Vector2>();

            foreach (Node<string> node in list)
            {
                result.Add(node.NodePosition);
            }

            return result; ;
        }

        private static Node<T> BFS<T>(Node<T> start, Node<T> goal)
        {
            Queue<Edge<T>> edgesStack = new Queue<Edge<T>>();
            edgesStack.Enqueue(new Edge<T>(start, start));

            while (edgesStack.Count > 0)
            {
                Edge<T> edge = edgesStack.Dequeue();
                
                if (!edge.To.Discovered)
                {
                    // Er edgen discovered og hvem er dens parent?:
                    edge.To.Discovered = true;
                    edge.To.Parent = edge.From;
                }
                if (edge.To == goal)
                {
                    return edge.To;
                }

                foreach (Edge<T> e in edge.To.EdgesList)
                {
                    if (!e.To.Discovered)
                    {
                        edgesStack.Enqueue(e);
                    }
                }
            }
            return null;

        }

        ///metoden som returnerer listen som fjenderne skal gå.
        private static List<Node<T>> TrackPath<T>(Node<T> node, Node<T> start)
        {
            List<Node<T>> pathList = new List<Node<T>>();

            while (!node.Equals(start))
            {
                pathList.Add(node);
                node = node.Parent;
            }

            pathList.Add(start);

            //Da det bliver lagt på fra toppen, vender vi listen, så den starter med A, og ikke I:
            pathList.Reverse();

            return pathList;
        }


    }
}
