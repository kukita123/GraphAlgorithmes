namespace Dijkstra_Algo_Lists
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyWeightedGraph graph1 = new MyWeightedGraph();
            graph1.AddNode("A");
            graph1.AddNode("B");
            graph1.AddNode("C");
            graph1.AddNode("D");
            graph1.AddNode("E");
            graph1.AddEdge("A", "B", 2);
            graph1.AddEdge("A", "C", 1);
            graph1.AddEdge("A", "D", 4);
            graph1.AddEdge("D", "E", 3);
            graph1.AddEdge("D", "C", 2);
            graph1.AddEdge("B", "E", 2);
            graph1.AddEdge("B", "C", 2);
            graph1.AddEdge("B", "D", 2);

            graph1.Print();
            Console.ReadLine();

            graph1.PrintDijkstra("A");
            Console.WriteLine();
        }
    }

    public class MyWeightedGraph
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

        // Dictionary with weight for each edge
        private Dictionary<Node, Dictionary<Node, int>> adjacencyList = new Dictionary<Node, Dictionary<Node, int>>();
        public void AddNode(string label)
        {
            var node = new Node(label);

            if(!nodes.ContainsKey(label))
                nodes.Add(label, node);

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
            // for unoriented graph add:
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

        // <returns> Dictionary -> minimal distances from the start label to each other</returns>
        public Dictionary<Node, int> Dijkstra(string startLabel)
        {
            var startNode = nodes[startLabel];
            if (startNode == null)
                throw new Exception("Illegal Argument");


            // Data structures for distances and previous nodes
            // Инициализация на структури за отчитане на разстояния и предходни върхове
            var distances = new Dictionary<Node, int>();
            var previousNodes = new Dictionary<Node, Node>();
            var unvisitedNodes = new List<Node>();

            // Initialising start distances to all nodes to MaxValue, initialising start values for previous nodes to null -> empty list
            // Инициализация на разстоянията до всички върхове с безкрайно разстояние, а предходните върхове с null
            foreach (var node in nodes.Values)
            {
                distances[node] = int.MaxValue;
                previousNodes[node] = null;
                unvisitedNodes.Add(node);
            }

            // Initialising distanse to start node equals 0
            // Разстоянието до стартовия връх е 0
            distances[startNode] = 0;

            while (unvisitedNodes.Count > 0)
            {
                var currentNode = GetNodeWithMinDistance(unvisitedNodes, distances);
                unvisitedNodes.Remove(currentNode);

                foreach (var neighbor in adjacencyList[currentNode])
                {
                    var alternativeRoute = distances[currentNode] + neighbor.Value;

                    if (alternativeRoute < distances[neighbor.Key])
                    {
                        distances[neighbor.Key] = alternativeRoute;
                        previousNodes[neighbor.Key] = currentNode;
                    }
                }
            }

            return distances;
        }

        // Method to choose the current minimal distance node
        private Node GetNodeWithMinDistance(List<Node> unvisitedNodes, Dictionary<Node, int> distances)
        {
            Node minNode = null;
            foreach (var node in unvisitedNodes)
            {
                if (minNode == null || distances[node] < distances[minNode])
                {
                    minNode = node;
                }
            }
            return minNode;
        }
        
        //Method to print the Dijkstra result
        public void PrintDijkstra(string startLabel)
        {
            Dictionary<Node, int> listRezults = new Dictionary<Node, int>();
            listRezults = Dijkstra(startLabel);

            foreach (var item in listRezults)
            {
                Console.WriteLine($"Minimal distanse from {startLabel} to {item.Key} is {item.Value}");
            }
        }
    }
}