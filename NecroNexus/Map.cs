using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    internal class Map
    {
        int nodeCount = -1;

        /*  Vi bygger graph1 instansen af Graph, som mapper ruten for bane nr 1. 
            Det er rammen som holder på vores noder og edges i denne bane.
            dvs at både noder og edges også bliver kreeret i denne metode.
            Heads up: Graph klassen er generisk.

            forloop'et i metoden skaber node: (0,1,2,...,7,8) (Validated)

            
        */
        public void Graph1()
        {
            //instans af graph:
            Graph<int> graph1 = new Graph<int>();

            //nye nodes bundet op på den nye instans:
            
            for (int i = 1; i <= 9; i++)
            {
                graph1.AddNode(nodeCount++);
            }



        }

        
       

    }
}
