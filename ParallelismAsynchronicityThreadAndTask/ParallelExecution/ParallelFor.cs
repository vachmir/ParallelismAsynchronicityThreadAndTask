using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelismAsynchronicityThreadAndTask.ParallelExecution
{
    class ParallelFor
    {
        static void ParallelLoopsInThread()
        {

            int length = 10;

            Console.WriteLine("Common For Loop");
            for (int index = 0; index < length; index++)
            {
                Console.WriteLine($"Value {index} is in N{Thread.CurrentThread.ManagedThreadId} Thread");
            }
            Console.WriteLine();

            Console.WriteLine("Parallel For Loop");
            Parallel.For(0, length, index =>
            {
                Console.WriteLine($"Value { index} is in N{ Thread.CurrentThread.ManagedThreadId} Thread ");
                Thread.Sleep(1000);
            });
            Console.ReadLine();
        }
    }
}
