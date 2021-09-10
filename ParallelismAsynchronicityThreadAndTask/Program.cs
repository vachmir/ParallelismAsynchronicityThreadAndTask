using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ParallelismAsynchronicityThreadAndTask.AsyncAndAwait;
using ParallelismAsynchronicityThreadAndTask.ProcessesAndThreads;
using ParallelismAsynchronicityThreadAndTask.ThreadsUsage;
using ParallelismAsynchronicityThreadAndTask.ParallelExecution;

namespace ParallelismAsynchronicityThreadAndTask
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            #region Asynchronicity with Async and Await keywords
            Console.WriteLine("Main method started");
            string messageAsync = await Asynchronicity.DoWorkAsync();
            string messageSync = Asynchronicity.DoWork();

            Console.WriteLine("Without Async and Await");
            Console.WriteLine(messageSync);

            Console.WriteLine("With Async and Await");
            Console.WriteLine(messageAsync);

            Console.WriteLine(Asynchronicity.DoWorkAsync().Result);
            Console.WriteLine(Asynchronicity.DoWorkAsync().GetAwaiter().GetResult());

            Asynchronicity.MethodReturnungTaskOfVoidAsync().Wait();

            Console.WriteLine();
            Asynchronicity.MethodReturningVoidAsync();
            Console.WriteLine(Asynchronicity.DoWorkAsync());

            var a = Asynchronicity.MethodWithTryCatchAsync().Result;
            Console.WriteLine(a);
            #endregion

            #region Processes and Threads
            //ProcessAndThread.AllRunningProcesses(); //Enumerating Running Processes
            //ProcessAndThread.GetSpecificProcessById();
            //string pID = Console.ReadLine();
            //int theProcID = int.Parse(pID);
            //ProcessAndThread.EnumThreadForPid(int.Parse(Console.ReadLine())); //Investigating a Process's Thread Set
            //ProcessAndThread.EnumModsForPid(int.Parse(Console.ReadLine()));  //Investigating a Process's Module Set
            //ProcessAndThread.StartAndKillProcess();//Start and Kill the Process
            //ProcessAndThread.UseApplicationVerbs();
            //ProcessAndThread.ListAllAssembliesInAppDomain();
            #endregion


            #region Thread usage
            Printer printer = new Printer();
            ThreadRun threadRun = new ThreadRun();
            #endregion


            #region Parallel Execution for Matrix
            Matrix.InitializeMatrices();
            Matrix.ArrayMultiplierIJK();
            Matrix.ArrayMultiplierIKJ();
            Matrix.ArrayParallelMultiplierIJK();
            Matrix.ArrayParallelMultiplierIKJ();
            Matrix.ArrayMultiplicationResult();
            #endregion
        }

        private static void CreateThreadUsingThreadClassWithoutParameter()
        {
            System.Threading.Thread thread;
            thread = new System.Threading.Thread(new System.Threading.ThreadStart(PrintNumber40Times));
            thread.Start();
        }
        private static void PrintNumber40Times()
        {

            for (int i = 0; i < 1000; i++)
            {

            }
            for (int i = 0; i < 40; i++)
            {
                Console.Write(1);
            }
            Console.WriteLine();

        }


        private static void CreateThreadUsingThreadClassWithParameter()
        {
            System.Threading.Thread thread;
            thread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(PrintNumberNTimes));
            thread.Start();
        }
        private static void PrintNumberNTimes(object times)
        {

            for (int i = 0; i < 1000; i++)
            {

            }
            int n = Convert.ToInt32(times);
            for (int i = 0; i < n; i++)
            {
                Console.Write(1);
            }
            Console.WriteLine();

        }


        private static void CreateThreadUsingThreadPool()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(PrintNumber40Times));
        }
        private static void PrintNumber40Times(object state)
        {

            for (int i = 0; i < 1000; i++)
            {

            }
            for (int i = 0; i < 40; i++)
            {
                Console.Write(1);
            }
            Console.WriteLine();

        }


    }
}
