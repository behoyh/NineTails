using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineTails
{
    public class Vertex
    {

        public int p { get; set; }

        public Vertex(int p)
        {
            this.p = p;
        }
    }

    public interface Graph
    {
        int getSize();

        List<Vertex> getVertices();

        int getIndex(Vertex vertex);

        List<int> getNeighbors(int index);

        int getDegree(int vertex);

        void printEdges();

        void clear();

        void addVertex(Vertex vertex);

        void addEdge(int u, int v);


        AbstractGraph.Tree DepthFirstSearch(int v);
        AbstractGraph.Tree BreathFirstSearch(int x);
    }

    public abstract class AbstractGraph
    {
        protected static List<Vertex> verticers = new List<Vertex>();
        protected List<List<int>> neighbors = new List<List<int>>();


        protected AbstractGraph()
        {

        }
        protected AbstractGraph(int[][] edges, Vertex[] vertices)
        {
            for (int i = 0; i < vertices.Length -1; i++)
            {
                verticers.Add(vertices[i]);
            }
            createAdjacencyLists(edges, vertices.Count());
        }

        protected AbstractGraph(List<Edge> edges, List<Vertex> vertices)
        {
            for (int i = 0; i < vertices.Count(); i++)
            {
                verticers.Add(vertices[i]);
            }
            createAdjacencyLists(edges, vertices.Count());
        }


        protected AbstractGraph(List<Edge> edges, int numberOfVertices)
        {
            for (int i = 0; i < numberOfVertices; i++)
            {
                verticers.Add(new NineTails.Vertex(i));
            }
            createAdjacencyLists(edges, verticers.Count());
        }

        protected AbstractGraph(int[][] edges, int numberOfVertices)
        {
            for (int i = 0; i < numberOfVertices; i++)
            {
               verticers.Add(verticers[i]);
            }
            createAdjacencyLists(edges, verticers.Count());
        }

        private void createAdjacencyLists(int[][] edges, int numberOfVertrices)
        {
            for (int i = 0; i < numberOfVertrices; i++)
            {
                neighbors.Add(new List<int>());
            }

            for (int j = 0; j < edges.Length; j++)
            {
                int u = edges[j][0];
                int v = edges[j][1];
                neighbors[u].Add(v);
            }
        }

        private void createAdjacencyLists(List<Edge> edges, int numberOfVertrices)
        {
            for (int i = 0; i <= numberOfVertrices; i++)
            {
                neighbors.Add(new List<int>());
            }

            foreach (Edge e in edges)
            {
                neighbors[e.u].Add(e.v);
            }
        }

        public int getSize()
        {
            return verticers.Count();
        }

        public List<Vertex> getVertices()
        {
            return verticers;
        }

        public Vertex getVertex(int vertex)
        {
            return verticers[vertex];
        }

        public int getIndex(Vertex vertex)
        {
            return verticers.IndexOf(vertex);
        }

        public List<int> getNeighbors(int index)
        {
            return neighbors[index];
        }

        public int getDegree(int vector)
        {
            return neighbors[vector].Count();
        }

        public void printEdges()
        {
            for (int u = 0; u < neighbors.Count(); u++)
            {
                Console.Write(getVertex(u));
                for (int j = 0; j < neighbors[u].Count; j++)
                {
                    Console.Write("(" + u + ", " + neighbors[u][j] + ") ");
                }
                Console.WriteLine();
            }
        }

        public void clear()
        {
            verticers.Clear();
            neighbors.Clear();
        }

        public void addEdge(int u, int v)
        {
            neighbors[u].Add(v);
            neighbors[v].Add(u);
        }

        public class Edge
        {
            public int u { get; set; }
            public int v { get; set; }
        }

        public void addVertex(Vertex vertex)
        {
            verticers.Add(vertex);
            neighbors.Add(new List<int>());
        }



        public Tree BreathFirstSearch(int v)
        {
            List<int> searchOrder = new List<int>();

            int[] parent = new int[verticers.Count()];

            for (int i = 0; i < parent.Length -1 ; i++)
            {
                parent[i] = -1;
            }

            LinkedList<int> queue = new LinkedList<int>();
            bool[] isVisited = new bool[verticers.Count()];
            queue.AddLast(v);
            isVisited[v] = true;

            while (queue.Count != 0)
            {
                int u = queue.Last();
                queue.RemoveLast();

                    searchOrder.Add(u);

                    foreach (int w in neighbors[u])
                    {
                        if (!isVisited[w])
                        {
                            queue.AddLast(w);
                            parent[w] = u;

                            isVisited[w] = true;
                        }
                    }
            }

            return new Tree(v, parent, searchOrder);
        }


        public Tree DepthFirstSearch(int v)
        {
            List<int> searchOrder = new List<int>();
            int[] parent = new int[verticers.Count()];

            for (int i = 0; i < parent.Length -1; i++)
            {
                parent[i] = -1;
            }

            bool[] isVisited = new bool[verticers.Count];

            DepthFirstSearch(v, parent, searchOrder, isVisited);

            return new Tree(v, parent, searchOrder);
        }

        private void DepthFirstSearch(int v, int[] parent, List<int> searchOrder, bool[] isVisited)
        {
            searchOrder.Add(v);
            isVisited[v] = true;

            foreach (int i in neighbors[v])
            {
                if (!isVisited[i])
                {
                    parent[i] = v;
                    DepthFirstSearch(i, parent, searchOrder, isVisited);
                }
            }
        }

        public class Tree : AbstractGraph
        {
            public int root { get; private set; }
            private int[] parent;
            private List<int> searchOrder;

            public Tree(int root, int[] parent, List<int> searchOrder)
            {
                this.root = root;
                this.parent = parent;
                this.searchOrder = searchOrder;
            }
            public int GetParent(int v)
            {
                return parent[v];
            }

            public List<Vertex> GetPath(int index)
            {
                List<Vertex> path = new List<Vertex>();

                do
                {
                    path.Add(verticers[index]);
                    index = parent[index];
                }
                while (index != -1);

                return path;
            }
            public void printPath(int index)
            {
                List<Vertex> path = GetPath(index);

                Console.Write($"Path from {verticers[root]} to {verticers[index]}");

                for (int i = path.Count(); i >= 0; i--)
                {
                    Console.Write(path[i] + "- ");
                }
            }
            public void PrintTree()
            {
                Console.Write($"Root is: {verticers[root]}");
                Console.Write($"Edges: ");
                for (int i = 0; i < parent.Length -1; i++)
                {
                    if (parent[i] != -1)
                    {
                        Console.Write($"({verticers[parent[i]]}, {verticers[i]}");
                    }
                }
                Console.WriteLine();
            }
        }

    }
    public class UnwightedGraph : AbstractGraph
    {
        public UnwightedGraph() { }

        public UnwightedGraph(int[][] edges, NineTails.Vertex[] vertices) : base(edges, vertices)
        { }

        public UnwightedGraph(List<Edge> edges, int numberOfVertices) : base(edges, numberOfVertices)
        { }

        public UnwightedGraph(List<Edge> edges, List<NineTails.Vertex> vertices) : base(edges, vertices)
        { }

        public UnwightedGraph(int[][] edges, int numberOfVertices) : base(edges, numberOfVertices)
        { }
    }
}
