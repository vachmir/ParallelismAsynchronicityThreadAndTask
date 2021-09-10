using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelismAsynchronicityThreadAndTask.AsyncAndAwait
{
    class Asynchronicity
    {
        public static string DoWork()
        {
            Thread.Sleep(8000);
            return "Job done";
        }

        public static async Task<string> DoWorkAsync() //This allows GUI to remain responsive
        {
            Console.WriteLine("From DoWork Async method");
            return await Task.Run(() =>
            {
                Thread.Sleep(8000);
                return "Job done";
            });
        }

        public static async Task MethodReturnungTaskOfVoidAsync()
        {
            await Task.Run(() =>
            {
                Thread.Sleep(4000);
            });
            Console.WriteLine("Void Method Completed");
        }

        public static async void MethodReturningVoidAsync() //Fire and Forget void async method
        {
            await Task.Run(() => {
                Thread.Sleep(4_000);
            });
            global::System.Console.WriteLine("Fire andforget void method completed");
        }

        public static async Task MultipleAwaits()   //Async method with multiple methods
        {
            await Task.Run(() => { Thread.Sleep(2000); });
            Console.WriteLine("Done with first task!");

            await Task.Run(() => { Thread.Sleep(2000); });
            Console.WriteLine("Done with second task!");

            await Task.Run(() => { Thread.Sleep(2000); });
            Console.WriteLine("Done with first task!");
        }

        public static async Task MultipleAwaitsAllTohether()
        {
            // Console.WriteLine(  "As");
            var task1 = Task.Run(() =>
            {
                Thread.Sleep(2000);
                Console.WriteLine("Done with first task!");
            });

            var task2 = Task.Run(() =>
            {
                Thread.Sleep(2000);
                Console.WriteLine("Done with second task!");
            });

            var task3 = Task.Run(() =>
            {
                Thread.Sleep(2000);
                Console.WriteLine("Done with third task!");
            });

            await Task.WhenAll(task1, task2, task3);
            Console.WriteLine("as");
        }

        public static async Task<string> MethodWithTryCatchAsync()
        {
            await DoWorkAsync();
            await MultipleAwaits();
            try
            {
                Console.WriteLine("Try and catch block");
                await DoWorkAsync();
                return "Try block in Async";
                //await DoWorkAsync();    //Unreachable codes
                //await MultipleAwaits();
            }
            catch (Exception)
            {
                await DoWorkAsync();
                await MultipleAwaits();
                throw;
            }
            finally
            {
                DoWork();
                await DoWorkAsync();
                await MultipleAwaits();
            }
        }

    }
}
