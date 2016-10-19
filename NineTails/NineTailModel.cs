using NineTails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NineTails.AbstractGraph;

namespace NineTails
{
    public class NineTailModel
    {
        public readonly int NumberOfNodes = 512;

        public Tree tree;

        public NineTailModel()
        {
            List<Edge> edges = GetEdges();

            UnwightedGraph graph = new UnwightedGraph(edges, NumberOfNodes);

            tree = graph.BreathFirstSearch(511);
        }

        private List<Edge> GetEdges()
        {
            List<Edge> edges = new List<Edge>();

            for (int u = 0; u < NumberOfNodes; u++)
            {
                for (int k = 0; k < 9; k++)
                {
                    char[] node = GetNode(u);
                    if (node[k] == 'T')
                    {
                        int v = GetFlippedNode(node, k);
                        edges.Add(new Edge { u = u, v = v });
                    }
                }
            }
            return edges;
        }

        public static int GetFlippedNode(char[] node, int position)
        {
            int row = position / 3;
            int column = position % 3;

            FlipACell(node, row, column);
            FlipACell(node, row - 1, column);
            FlipACell(node, row + 1, column);
            FlipACell(node, row, column - 1);
            FlipACell(node, row, column + 1);

            return GetIndex(node);
        }

        public static void FlipACell(char[] node, int row, int column)
        {
            if (row >= 0 && row <= 2 && column >= 0 && column <= 2) 
            {
                if(node[row * 3 + column] == 'H')
                {
                    node[row * 3 + column] = 'T';
                }
                else
                {
                    node[row * 3 + column] = 'H';
                }                         
            }
        }
        public static int GetIndex(char[] node)
        {
            int result = 0;

            for (int i = 0; i < 9; i++)
            {
                if (node[i] == 'T')
                {
                    result = result * 2 + 1;
                }
                else
                {
                    result = result * 2 + 0;
                }
            }
            return result;
        }
        public static char[] GetNode(int index)
        {
            char[] result = new char[9];

            for (int i = 0; i < 9; i++)
            {
                int digit = index % 2;
                if (digit == 0)
                {
                    result[8 - i] = 'H';
                }
                else
                {
                    result[8 - 1] = 'T';
                }
                index = index / 2;
            }
            return result;
        }

        public List<Vertex> getShortestPath(int nodeIndex)
        {
            return tree.GetPath(nodeIndex);
        }

        public static void printNode(char[] node)
        {
            for (int i = 0; i < 9; i++)
            {
                if (i % 3 != 2)
                {
                    Console.Write(node[i]);
                }
                else
                {
                    Console.WriteLine(node[i]);
                }
            }
            Console.WriteLine();
        }
    }
}
