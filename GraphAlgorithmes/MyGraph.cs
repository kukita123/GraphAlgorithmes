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

        //array (hash table) to implement the nodes:
        private Dictionary<string, Node> nodes = new Dictionary<string, Node>();

        //array (hash table) of Lists with connected nodes to implement the edges:
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
            var fromNode = nodes[from]; //the node in the nodes hash map

            if (fromNode == null)
                throw new Exception("Illegal Argument");

            var toNode = nodes[to];
            if (toNode == null)
                throw new Exception("Illegal Argument");

            adjacencyList[fromNode].Add(toNode);
            // adjacencyList[toNode].Add(fromNode); //to create unoriented graph
        }

        public void Print()
        {
            foreach (var item in adjacencyList.Keys)
            {
                // we iterate the adjacencyLis and we take a list with all nodes, connected with the current node (item)
                var listToPrint = adjacencyList[item];
                if (listToPrint.Count != 0)
                    Console.WriteLine(item + " is connected to "+ string.Join(", ", listToPrint));
            }
        }

        public void RemoveNode(string label)
        {
            var node = nodes[label];
            if (node == null)
                return;

            // first we go to the adjacencyList and for the every entry in it,
            // to remove the node from it
            foreach (var item in adjacencyList.Keys)            
                adjacencyList[item].Remove(node);

            //next we need to remove tne node from the adjacencyList and from the nodes
            adjacencyList.Remove(node);
            nodes.Remove(label);            
        }

        public void RemoveEdge(string from, string to)
        {
            var fromNode = nodes[from]; //the node in the nodes hash map
            var toNode = nodes[to];
            if (fromNode == null || toNode == null)
                return;

            adjacencyList[fromNode].Remove(toNode);
        }

        public void TraverseDepthFirstRecursively(string root)
        {
            var node = nodes[root];
            if (node == null)
                return;

            TraverseDepthFirstRecursively(nodes[root], new HashSet<Node>());
        }

        private void TraverseDepthFirstRecursively(Node root, HashSet<Node> visited)
        {
            Console.WriteLine(root);
            visited.Add(root);  //we mark this node as visited

            //we should recursively visit all the neighbours of this root
            foreach (var node in adjacencyList[root])
            {
                if (!visited.Contains(node))
                    TraverseDepthFirstRecursively(node, visited);
            }            
        }

        public void TraverseDepthFirstIteratively(string root)
        {
            //using stack:
            //push(root)
            //while(stack is not empty)
            //   current = pop()
            //   visit(current)
            //   push each unvisited nighbour onto the stack

            var node = nodes[root];
            if (node == null)
                return;

            HashSet<Node> visited = new HashSet<Node>();

            Stack<Node> stack = new Stack<Node>();
            stack.Push(node);

            while (stack.Count != 0)
            {
                var current = stack.Pop();

                if (visited.Contains(current))
                    continue;

                Console.WriteLine(current);
                visited.Add(current);

                foreach (var neighbour in adjacencyList[current])
                {
                    if (!visited.Contains(neighbour))
                        stack.Push(neighbour);
                }
            }

        }

        public void TrverseBreadthFirst(string root)
        {
            var node = nodes[root];
            if (node == null)
                return;

            HashSet<Node> visited = new HashSet<Node>();

            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(node);

            while (queue.Count() != 0)
            {
                var current = queue.Dequeue();
                if (visited.Contains(current))
                    continue;

                Console.WriteLine(current);
                visited.Add(current);

                foreach (var neighbour in adjacencyList[current])
                {
                    if (!visited.Contains(neighbour))
                        queue.Enqueue(neighbour);
                }
            }
        }
    }
}
