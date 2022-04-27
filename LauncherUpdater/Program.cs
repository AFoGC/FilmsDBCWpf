using BL_Launcher;
using BO_Launcher;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace LauncherUpdater
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start Launcher Update");
            string launcherPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\LauncherFDBC.exe";
            Console.WriteLine("Start last update load");
            LauncherBO launcherBO = LauncherBL.GetLastUpdate();
            Console.WriteLine("End last update load");
            if (File.Exists(launcherPath))
            {
                Console.WriteLine("Delete old version");
                File.Delete(launcherPath);
            }
            Console.WriteLine("Save new version");
            File.WriteAllBytes(launcherPath, launcherBO.LauncherFile);

            Console.WriteLine("Start launcher");
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = launcherPath
            };
            Process.Start(startInfo);
            Console.ReadKey();
        }
    }
}
