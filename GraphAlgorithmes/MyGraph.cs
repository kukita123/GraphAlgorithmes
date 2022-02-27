using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphAlgorithmes
{
    class MyGraph
    {
        private class Node
        {
            public string Label { get; set; }

            public Node(string label)
            {
                this.Label = label;
            }

            override public string ToString()
            {
                return Label;
            }
        }

        private Dictionary<string, Node> nodes = new Dictionary<string, Node>();
        //array of Lists to implement the edges:
        private Dictionary<Node, List<Node>> adjacencyList = new Dictionary<Node, List<Node>>();

        public void AddNode(string label)
        {
            var node = new Node(label);

            if (!nodes.ContainsKey(label))
                nodes.Add(label, node);

            if (!adjacencyList.ContainsKey(node))
                adjacencyList.Add(node, new List<Node>());

            return;
        }

        public void AddEdge(string from, string to)
        {
            var fromNode = nodes[from];
            if (fromNode == null)
                throw new Exception("Illegal Argument");

            var toNode = nodes[to];
            if (toNode == null)
                throw new Exception("Illegal Argument");

            adjacencyList[fromNode].Add(toNode);
            adjacencyList[toNode].Add(fromNode); //to create unoriented graph
        }

        public void Print()
        {
            foreach (var item in adjacencyList.Keys)
            {
                var listToPrint = adjacencyList[item];
                if (listToPrint.Count != 0)
                    Console.WriteLine(item + " is connected to "+ string.Join(", ", listToPrint));
            }
        }
    }
}
