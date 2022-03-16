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

            //MyGraph graphToTraverce = new MyGraph();
            //graphToTraverce.AddNode("A");
            //graphToTraverce.AddNode("B");
            //graphToTraverce.AddNode("C");
            //graphToTraverce.AddNode("D");
            //graphToTraverce.AddNode("E");
            //graphToTraverce.AddEdge("A", "B");
            //graphToTraverce.AddEdge("A", "E");
            //graphToTraverce.AddEdge("B", "B");
            //graphToTraverce.AddEdge("C", "B");
            //graphToTraverce.AddEdge("C", "A");
            //graphToTraverce.AddEdge("C", "D");
            //graphToTraverce.AddEdge("D", "E");
            //graphToTraverce.TraverseDepthFirstRecursively("C");
            //Console.WriteLine();
            //graphToTraverce.TraverseDepthFirstIteratively("C");
            //Console.WriteLine();
            //graphToTraverce.TrverseBreadthFirst("C");

            Console.ReadKey();
        }
    }
}
