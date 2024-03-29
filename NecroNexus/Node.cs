﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace NecroNexus
{
    public class Node<T>
    {

        public T Data { get; set; } 

        // EDGE LIST
        public List<Edge<T>> EdgesList { get; set; } = new List<Edge<T>>();

        public bool Discovered { get; set; } = false;

        public Vector2 NodePosition { get; set; }

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
        public void AddEdge(Node<T> other, bool wallBuilt)
        {
            EdgesList.Add(new Edge<T>(this, other, wallBuilt));

        }



        
    }
}
