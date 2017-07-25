using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01Edmonds_Karp
{
    class EdmondsKarp
    {
        //      (B)
        //     / | \
        //  (A)  |  (D)
        //     \ | /
        //      (C)
        private static int[][] graph;

        private static int[] parents;

        static void Main(string[] args)
        {
            graph = new int[][]
                        {
                            new int[] { 0, 1000, 1000, 0 },
                            new int[] { 0, 0, 1, 1000 },
                            new int[] { 0, 0, 0, 1000 },
                            new int[] { 0, 0, 0, 0 },
                        };

            parents = new int[graph.Length];
            Console.WriteLine(EdmondsKarpMaxFlow());
        }

        private static bool BreadthFirstSearch(int start, int end)
        {
            bool[] visited = new bool[graph.Length];
            visited[start] = true;
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                for (int i = 0; i < graph[node].Length; i++)
                {
                    var capacity = graph[node][i];

                    if (!visited[i] && capacity > 0)
                    {
                        queue.Enqueue(i);
                        visited[i] = true;
                        parents[i] = node;
                    }
                }
            }

            return visited[end];
        }

        private static int EdmondsKarpMaxFlow()
        {
            //reset parents
            for (int i = 0; i < graph.Length; i++)
            {
                parents[i] = -1;
            }

            int maxFlow = 0;
            int start = 0;
            int end = graph.Length - 1;

            while (BreadthFirstSearch(start, end))
            {
                int pathFlow = int.MaxValue;
                int currentNode = end;
                while (currentNode != start)
                {
                    int previousNode = parents[currentNode];
                    pathFlow = Math.Min(pathFlow, graph[previousNode][currentNode]);
                    currentNode = previousNode;
                }

                maxFlow += pathFlow;

                //update flow values
                currentNode = end;
                while (currentNode != start)
                {
                    int previousNode = parents[currentNode];
                    graph[previousNode][currentNode] -= pathFlow;
                    graph[currentNode][previousNode] += pathFlow;
                    currentNode = previousNode;
                }
            }

            return maxFlow;
        }
    }
}
