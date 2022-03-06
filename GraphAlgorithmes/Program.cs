using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphAlgorithmes
{
    class Program
    {
        static void Main(string[] args)
        {
            MyGraph graph = new MyGraph();
            graph.AddNode("A");
            graph.AddNode("B");
            graph.AddNode("C");
            graph.AddNode("D");
            graph.AddEdge("A", "B");
            graph.AddEdge("A", "C");
            graph.AddEdge("C", "D");
            graph.Print();

            Console.WriteLine();
            graph.RemoveEdge("A", "C");
            graph.Print();

            Console.WriteLine();
            graph.RemoveNode("D");
            graph.Print();

            Console.ReadKey();
        }
    }
}
