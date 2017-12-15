using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graph_Fun
{
    class Program
    {
        static void Main(string[] args)
        {
            var graph = new Graph();
            var startBFS = 3;
            var startDFS = 2;

            Console.WriteLine("### BFS ###");
            Console.WriteLine(String.Format("Start: {0}", startBFS));
            for (var i = 0; i < graph.Count; ++i)
            {
                graph.BFS(i).ForEach(x => Console.Write(x));
                Console.WriteLine();
            }
            Console.WriteLine();

            Console.WriteLine("### DFS ###");
            Console.WriteLine(String.Format("Start: {0}", startDFS));
            for (var i = 0; i < graph.Count; ++i)
            {
                graph.DFS(i).ForEach(x => Console.Write(x));
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
