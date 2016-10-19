using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineTails
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the nine coin values as [H]'s and [T]'s");

           

            Console.WriteLine();
            string input = Console.ReadLine();

            char[] initalNode = input.ToCharArray();

            while (initalNode.Count() < 9)
            {
                Console.Write("Enter the nine coin values as [H]'s and [T]'s");

                Console.WriteLine();

                initalNode = Console.ReadLine().ToCharArray();
            }


            NineTailModel model = new NineTailModel();

            List<Vertex> path = model.getShortestPath(NineTailModel.GetIndex(initalNode));

            Console.WriteLine("Steps to flip coins:");

            for (int i = 0; i < path.Count(); i++)
            {
                NineTailModel.printNode(NineTailModel.GetNode(path[i].p));
            }

            Console.ReadLine();
        }
    }
}
