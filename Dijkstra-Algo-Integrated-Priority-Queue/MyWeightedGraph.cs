namespace PathDejkstraAlgo

{
    using System;
    using System.Collections.Generic;
    class MyWeightedGraph
    {
        public class Node
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
        // Dictionary с тежести за всяко ребро
        private Dictionary<Node, Dictionary<Node, int>> adjacencyList = new Dictionary<Node, Dictionary<Node, int>>();
        public void AddNode(string label)
        {
            var node = new Node(label);

            if (!adjacencyList.ContainsKey(node))
                adjacencyList.Add(node, new Dictionary<Node, int>());

            return;
        }
        public void AddEdge(string from, string to, int weight)
        {
            var fromNode = nodes[from];
            if (fromNode == null)
                throw new Exception("Illegal Argument");

            var toNode = nodes[to];
            if (toNode == null)
                throw new Exception("Illegal Argument");

            adjacencyList[fromNode][toNode] = weight;
            // Ако искате графа да бъде неориентиран, можете да добавите и следния ред:
            // adjacencyList[toNode][fromNode] = weight;
        }
        public void Print()
        {
            foreach (var entry in adjacencyList)
            {
                var fromNode = entry.Key;
                var edges = entry.Value;

                foreach (var edge in edges)
                {
                    var toNode = edge.Key;
                    var weight = edge.Value;

                    Console.WriteLine($"{fromNode} is connected to {toNode} with weight {weight}");
                }
            }
        }
        public void RemoveNode(string label)
        {
            var node = nodes[label];
            if (node == null)
                return;

            adjacencyList.Remove(node);

            // Променяме на обхождане на всички върхове и премахване на връзката с изтрития връх
            foreach (var entry in adjacencyList)
            {
                var edges = entry.Value;
                edges.Remove(node);
            }

            nodes.Remove(label);
        }
        public void RemoveEdge(string from, string to)
        {
            var fromNode = nodes[from];
            var toNode = nodes[to];

            if (fromNode == null || toNode == null)
                return;

            adjacencyList[fromNode].Remove(toNode);
            // Ако искате графа да бъде неориентиран, добавете и следния ред:
            // adjacencyList[toNode].Remove(fromNode);
        }
        public List<Node> Dijkstra(string start, string end)
        {
            var startNode = nodes[start];
            var endNode = nodes[end];

            if (startNode == null || endNode == null)
                throw new ArgumentException("Invalid start or end node label");

            var distances = new Dictionary<Node, int>();
            var predecessors = new Dictionary<Node, Node>();
            var priorityQueue = new PriorityQueue<Node>(Comparer<Node>.Create((x, y) => distances[x] - distances[y]));

            foreach (var node in nodes.Values)
            {
                distances[node] = int.MaxValue;
            }

            distances[startNode] = 0;
            priorityQueue.Enqueue(startNode);

            while (priorityQueue.Count > 0)
            {
                var current = priorityQueue.Dequeue();

                if (current == endNode)
                    break;

                foreach (var neighbor in adjacencyList[current])
                {
                    var newDistance = distances[current] + adjacencyList[current][neighbor.Key];

                    if (newDistance < distances[neighbor.Key])
                    {
                        distances[neighbor.Key] = newDistance;
                        predecessors[neighbor.Key] = current;
                        priorityQueue.Enqueue(neighbor.Key);
                    }
                }
            }

            var path = new List<Node>();
            var currentPredecessor = endNode;

            while (currentPredecessor != null)
            {
                path.Insert(0, currentPredecessor);
                currentPredecessor = predecessors[currentPredecessor];
            }

            return path;
        }
    }
}