using BL_Launcher;
using BO_Launcher;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LauncherUpdater
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Start Launcher Update");
                string launcherPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\LauncherFDBC.exe";

                LauncherBO launcherBO = GetExeFromServer();
                DeleteExe(launcherPath);
                WriteExe(launcherPath, launcherBO);
                StartLauncher(launcherPath);
            }
            catch(Exception e)
            {
                Console.WriteLine();
                Console.WriteLine(e);
                Console.ReadKey();
            }
            
        }

        static LauncherBO GetExeFromServer()
        {
            Console.WriteLine("Start last update load");
            LauncherBO launcherBO = LauncherBL.GetLastUpdate();
            Console.WriteLine("End last update load");
            return launcherBO;
        }

        static void StartLauncher(string launcherPath)
        {
            Console.WriteLine("Start launcher");
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = launcherPath
            };
            Process.Start(startInfo);
        }

        static void WriteExe(string launcherPath, LauncherBO launcherBO)
        {
            Console.WriteLine($"Save new version ID: {launcherBO.ID}");
            File.WriteAllBytes(launcherPath, launcherBO.LauncherFile);
        }

        static void DeleteExe(string launcherPath)
        {
            if (File.Exists(launcherPath))
            {
                Console.WriteLine("Delete old version...");
                bool cont = true;
                while (cont)
                {
                    try
                    {
                        File.Delete(launcherPath);
                        cont = false;
                    }
                    catch
                    {

                    }
                }
                Console.WriteLine("Deletion completed...");
            }
        }
    }
}
