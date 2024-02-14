namespace PathDejkstraAlgo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyWeightedGraph graph = new MyWeightedGraph();

            graph.AddNode("A");
            graph.AddNode("B");
            graph.AddNode("C");
            graph.AddNode("D");
            graph.AddNode("E");

            graph.AddEdge("A", "B", 2);
            graph.AddEdge("A", "C", 4);
            graph.AddEdge("B", "D", 7);
            graph.AddEdge("C", "D", 1);
            graph.AddEdge("C", "E", 3);
            graph.AddEdge("D", "E", 5);

            Console.WriteLine("Graph:");
            graph.Print();

            string startNode = "A";
            string endNode = "E";

            var result = graph.Dijkstra(startNode, endNode);

            Console.WriteLine($"\nShortest path from {startNode} to {endNode}:");
            Console.WriteLine($"Weight: {result.weight}");
            Console.WriteLine($"Path: {string.Join(" -> ", result.path)}");
        }
    }

}