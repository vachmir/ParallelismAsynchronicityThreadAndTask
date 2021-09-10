using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace ParallelismAsynchronicityThreadAndTask.ProcessesAndThreads
{
    class ProcessAndThread
    {

        static readonly object threadLock = new object();
        public static void AllRunningProcesses()
        {
            var runningProcs = from proc in Process.GetProcesses(".") orderby proc.Id select proc;
            foreach (var p in runningProcs)
            {
                string info = $"PID: {p.Id}, Name: {p.ProcessName}";
                Console.WriteLine(info);
            }
            Console.WriteLine($"Thre are {runningProcs.Count()} Processes");
        }
        public static void GetSpecificProcessById()
        {
            Process theProc = null;
            try
            {
                theProc = Process.GetProcessById(0);
                Console.WriteLine(theProc);
            }
            catch (ArgumentException arEx)
            {
                Console.WriteLine(arEx.Message); ;
            }

        }
        public static void EnumThreadForPid(int pID)
        {
            Process theProc = null;
            try
            {
                theProc = Process.GetProcessById(pID);
            }
            catch (ArgumentException arEx)
            {
                Console.WriteLine(arEx.Message);
                return;
            }
            Console.WriteLine($"Threads used by {theProc.ProcessName}");
            ProcessThreadCollection theThreads = theProc.Threads;

            foreach (ProcessThread pt in theThreads)
            {
                string info = $"The=read ID: {pt.Id}, Start Time: {pt.StartTime.ToShortTimeString()}, Priority: {pt.PriorityLevel}, Wait Reason: {pt.WaitReason}";
                Console.WriteLine(info);
            }

        }
        public static void EnumModsForPid(int pID)
        {
            Process theProc = null;
            try
            {
                theProc = Process.GetProcessById(pID);
            }
            catch (ArgumentException arEx)
            {
                Console.WriteLine(arEx.Message);
                return;
            }


            Console.WriteLine($"Loaded Modules {theProc.ProcessName}");
            ProcessModuleCollection theMods = theProc.Modules; //Investigating a Process’s Module Set

            foreach (ProcessModule pm in theMods)
            {
                string info = $"Module Name: {pm.ModuleName}, {pm.FileName}";
                Console.WriteLine(info);
            }
        }
        public static void StartAndKillProcess()
        {
            Process proc = null;
            try     //Starting a process
            {
                proc = Process.Start(@"C:\Program Files\Google\Chrome\Application\chrome.exe", "fb.com");

            }
            catch (InvalidOperationException invalidOP)
            {
                Console.WriteLine(invalidOP.Message);
            }

            Console.WriteLine($"Press enter to kill {proc.ProcessName}");
            Console.ReadLine();

            try
            {
                foreach (var p in Process.GetProcessesByName("Taskmgr"))
                {
                    p.Kill(true);
                }
            }
            catch (InvalidOperationException invlidOp)
            {
                Console.WriteLine(invlidOp.Message);
            }


            try  // Maximized window
            {
                ProcessStartInfo startInfo = new ProcessStartInfo("chrome");
                startInfo.UseShellExecute = true;
                proc = Process.Start(startInfo);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static void UseApplicationVerbs()
        {
            int i = 0;
            ProcessStartInfo si = new ProcessStartInfo(@"C:\Users\vchgn\Desktop\Text.txt");//Have file at this location
            foreach (var verb in si.Verb)
            {
                Console.WriteLine($"{i++}.{verb}");
            }
            si.WindowStyle = ProcessWindowStyle.Maximized;
            si.Verb = "Edit";
            si.UseShellExecute = true;
            Process.Start(si);
        }
        public static void ListAllAssembliesInAppDomain()
        {
            AppDomain defaultAD = AppDomain.CurrentDomain;
            var loadedAssemblies = defaultAD.GetAssemblies().OrderBy(x => x.GetName().Name);
            Console.WriteLine($"***** Here are the assemblies loaded in { defaultAD.FriendlyName} *****\n");
            foreach (Assembly a in loadedAssemblies)
            {
                Console.WriteLine($"-> Name, Version: {a.GetName().Name}:{a.GetName().Version}");
            }
        }

        public static void ThreadLocking()
        {

            lock (threadLock)
            {
                Console.WriteLine($"-> {Thread.CurrentThread.Name} is printing numbers");
                Console.Write("Numbers are: ");
                for (int i = 0; i < 15; i++)
                {
                    Thread.Sleep(10);
                    Console.Write(i);
                }
                Console.WriteLine();
            }

        }
    }
}
