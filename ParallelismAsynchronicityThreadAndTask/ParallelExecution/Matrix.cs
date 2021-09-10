using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelismAsynchronicityThreadAndTask.ParallelExecution
{
    class Matrix
    {
        static int m = 535, n = 528, p = 528, q = 531;
        //static int m = 5, n = 3, p = 3, q = 4;
        static int[,] A = new int[m, n];
        static int[,] B = new int[p, q];
        static int[,] C = new int[m, q];
       public static void InitializeMatrices()
        {
            Random r = new Random();

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    A[i, j] = r.Next(0, 5);
                }
            }

            for (int i = 0; i < p; i++)
            {
                for (int j = 0; j < q; j++)
                {
                    B[i, j] = r.Next(0, 5);
                }
            }
        }
        public static void ArrayMultiplierIJK()
        {
            var stopWarchIJK = Stopwatch.StartNew();
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < q; j++)
                {
                    for (int k = 0; k < p; k++)
                    {
                        C[i, j] = A[i, k] * B[k, j];
                    }
                }
            }
            Console.WriteLine($"Execution in I-J-K order finished after {stopWarchIJK.Elapsed} seconds");

        }

        public static void ArrayMultiplierIKJ()
        {
            var stopWarchIKJ = Stopwatch.StartNew();
            for (int i = 0; i < m; i++)  //[i,k,j] order works better than [i,j,k]
            {
                for (int k = 0; k < p; k++)
                {
                    for (int j = 0; j < q; j++)
                    {
                        C[i, j] = A[i, k] * B[k, j];
                    }
                }
            }
            Console.WriteLine($"Execution in I-K-J order finished after {stopWarchIKJ.Elapsed} seconds");
        }

        public static void ArrayParallelMultiplierIJK()
        {

            var stopWarcParallelhIJK = Stopwatch.StartNew();
            Parallel.For(0, m, i =>  //The best performance is Parallel [i]
            {
                for (int j = 0; j < q; j++)
                {
                    for (int k = 0; k < p; k++)
                    {
                        C[i, j] = A[i, k] * B[k, j];
                    }
                }
            });

            Console.WriteLine($"Parallel Execution in I-J-K order finished after {stopWarcParallelhIJK.Elapsed} seconds");
        }
        public static void ArrayParallelMultiplierIKJ()
        {

            var stopWarcParallelhIKJ = Stopwatch.StartNew();
            Parallel.For(0, m, i =>  //The best performance is Parallel [i]
            {
                for (int k = 0; k < p; k++)
                {
                    for (int j = 0; j < q; j++)
                    {
                        C[i, j] = A[i, k] * B[k, j];
                    }
                }
            });
            Console.WriteLine($"Parallel Execution in I-K-J order finished after {stopWarcParallelhIKJ.Elapsed} seconds");
        }

        public static void ArrayMultiplicationResult()
        {
            Console.WriteLine("Result");
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < q; j++)
                {
                    Console.Write($"C[{i}{j}]={C[i, j]} \t");
                }
                Console.WriteLine();
            }
        }
    }
}
