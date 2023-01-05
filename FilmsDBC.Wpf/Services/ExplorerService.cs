using System.Diagnostics;
using WpfApp.Services.Interfaces;

namespace WpfApp.Services
{
    public class ExplorerService : IExplorerService
    {
        public void OpenExplorer(string path)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                Arguments = path,
                FileName = "explorer.exe"
            };
            Process.Start(startInfo);
        }
    }
}
