using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
