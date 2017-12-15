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
            var graph = new Graph(loadFromFile: true);
            
            graph.DisplayMatrix();
            Console.WriteLine();

            for (var target = 0; target < graph.Count; ++target)
            {
                Console.WriteLine(String.Format("Target: {0}", target));

                Console.WriteLine(String.Format("### BFS Shortest Path to {0} ###", target));
                graph.ShortestPathBFS(target)
                    .ForEach(x => Console.Write(x));
                Console.WriteLine();

                Console.WriteLine(String.Format("### DFS Shortest Path to {0} ###", target));
                graph.ShortestPathDFS(target)
                    .ForEach(x => Console.Write(x));
                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}
