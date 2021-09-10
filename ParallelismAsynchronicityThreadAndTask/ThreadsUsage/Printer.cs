using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelismAsynchronicityThreadAndTask.ThreadsUsage
{
    class Printer
    { 
        private object threadLock = new object();
        public void PrintNumbers()
        {
            Console.WriteLine($"{Thread.CurrentThread.Name} is executing PrintNumbers()");
            for (int i = 0; i < 10; i++)
            {
                Console.Write($"{i}, ");
                Thread.Sleep(2000);
            }
            Console.WriteLine();
        }
        public void PrintTheNumbers(object state)
        {
            Printer task = (Printer)state;
            task.PrintNumbers();
        }
        public void PrintNumbersRandomWait()
        {
            Console.WriteLine($"{Thread.CurrentThread.Name} is executing PrintNUmbersRandomeWait()");

            for (int i = 0; i < 10; i++)
            {
                Random r = new Random();
                Thread.Sleep(1000 * r.Next(2)); //Thread waits for random time
                Console.Write($"{i}, ");
            }
            Console.WriteLine();
        }

        public void ConcurrentThreads()//     Issue of Concurrency
        {
            Printer printer = new Printer();
            Thread[] threads = new Thread[10];  //Make 10 threads that all point to the same method on the same object
            for (int i = 0; i < 10; i++)
            {
                threads[i] = new Thread(new ThreadStart(printer.PrintNumbersRandomWait));
                {
                    threads[i].Name = $"Worker thread ${i}";
                }
            }

            foreach (Thread t in threads)
            {
                t.Start();  //Start each thread
            }
            Console.WriteLine();
        }
      
        public void PrintNumbersRandomWaitLocked() //This method locks the threads
        {
            lock (threadLock)
            {
                Console.WriteLine($"{Thread.CurrentThread.Name} is executing PrintNUmbersRandomeWithLocked()");

                for (int i = 0; i < 10; i++)
                {
                    Random r = new Random();
                    Thread.Sleep(1000 * r.Next(2));
                    Console.WriteLine($"{i}, ");
                }
                Console.WriteLine();
            }
        }
        public void ConcurrentThreadsLocked() //Console and Randome are locked in PrintNumbersRandomWaitLocked() method
        {
            Printer printer = new Printer();
            Thread[] threads = new Thread[10];
            for (int i = 0; i < 10; i++)
            {
                threads[i] = new Thread(new ThreadStart(printer.PrintNumbersRandomWaitLocked));  //Calling method having lock keyword
                {
                    threads[i].Name = $"Worker thread ${i}";
                }
            }
            foreach (Thread t in threads)
            {
                t.Start();
            }
            Console.WriteLine();
        }

        public void PrintNumbersMonitor()
        {
            Monitor.Enter(threadLock);
            try
            {
                Console.WriteLine($"{Thread.CurrentThread.Name} is executing PrintNumbersMonitor");

                for (int i = 0; i < 10; i++)
                {
                    Random r = new Random();
                    Thread.Sleep(1000 * r.Next(2));
                    Console.WriteLine($"{i}, ");
                }
                Console.WriteLine();
            }
            finally
            {
                Monitor.Exit(threadLock);
            }
        }
        public void ConcurrentThreadsMonitor()
        {
            Printer printer = new Printer();
            Thread[] threads = new Thread[10];
            for (int i = 0; i < 10; i++)
            {
                threads[i] = new Thread(new ThreadStart(printer.ConcurrentThreadsMonitor));  //Calling method with Monitor()
                {
                    threads[i].Name = $"Worker thread ${i}";
                }
            }
            foreach (Thread t in threads)
            {
                t.Start();
            }
            Console.WriteLine();
        }
      
       
    }
}
