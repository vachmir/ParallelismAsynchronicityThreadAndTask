using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelismAsynchronicityThreadAndTask.ThreadsUsage
{
    class ThreadRun
    {
        private object threadLock = new object();
        
        public static void ThreadStartDelegate()
        {
            Printer printer = new Printer();

            Console.WriteLine("Do you want [1] or [2] threads? ");
            string threadCount = Console.ReadLine();

            Thread primaryThread = Thread.CurrentThread;
            primaryThread.Name = "Primary";

            Console.WriteLine($"{Thread.CurrentThread.Name} is executing in Main()"); //Display Thread info 

            switch (threadCount)
            {
                case "2":
                    Thread backgrounfThread = new Thread(new ThreadStart(printer.PrintNumbers));
                    backgrounfThread.Name = "Secondary";
                    backgrounfThread.Start();
                    break;
                case "1":
                    printer.PrintNumbers();
                    break;
                default:
                    Console.WriteLine("I do not know what you want. You get 1 thread");
                    goto case "1";
            }
            Console.WriteLine("This is on the primary thread");
        }
        public static void ParameterizedThreadStartDelegate()
        {
            Printer printer = new Printer();
            Console.WriteLine($"ID of thread in Main(): {Thread.CurrentThread.ManagedThreadId}");
            AddParams ap = new AddParams(10, 10); // Make an AddParams object to pass to the secondary thread.
            Thread t = new Thread(new ParameterizedThreadStart(Add));
            t.Start(ap);

            Thread.Sleep(50);     // Force a wait to let other thread finish.

            Console.WriteLine("***** Background Threads *****\n");

            Thread bgroundThread = new Thread(new ThreadStart(printer.PrintNumbers));

            bgroundThread.IsBackground = true;    // This is now a background thread.
            bgroundThread.Start();
        }
        public static void ForegroundAndBackgroundThreads()
        {
            Console.WriteLine("Background Threads");
            Printer printer2 = new Printer();
            Thread backgroundThread = new Thread(new ThreadStart(printer2.PrintNumbers));
            backgroundThread.IsBackground = true;
            backgroundThread.Start();
        }
        public static void RunThreadFormThreadPool()
        {
            Printer printer = new Printer();
            WaitCallback workItem = new WaitCallback(printer.PrintTheNumbers);
            for (int i = 0; i < 10; i++)
            {
                ThreadPool.QueueUserWorkItem(workItem);
            }
            Console.WriteLine("All tasks queued");
        }
        public static void ExtractExecutingThread()
        {
            Thread currThread = Thread.CurrentThread;   //Get thjread currently executing this method      
        }
        public static void ExtractAppDomainHostingThread()
        {
            AppDomain ad = Thread.GetDomain();  //Obtain AppDomain hosting the current thread
        }
        public static void ExtractCurrentThreadExecutionContext()
        {
            ExecutionContext executionContext = Thread.CurrentThread.ExecutionContext; //Obtain execution Context under which the current thread is operating
        }
        public static void ThreadMembers()
        {
            Thread primaryThread = Thread.CurrentThread;
            primaryThread.Name = "The Primary Thread";

            Console.WriteLine($"ID of current thread: {primaryThread.ManagedThreadId}");
            Console.WriteLine($"Thread Name: {primaryThread.Name}");
            Console.WriteLine($"Has thread started?: {primaryThread.IsAlive}");
            Console.WriteLine($"Priority Level: {primaryThread.Priority}");
            Console.WriteLine($"Thread State: {primaryThread.ThreadState}");

        }
        private static void Add(object data)
        {
            if (data is AddParams ap)
            {
                Console.WriteLine($"ID of thread in Add(): {Thread.CurrentThread.ManagedThreadId}");
                Console.WriteLine($"{ap.a} + {ap.b} is {ap.a + ap.b}");
            }
        }   
        

    }
}
