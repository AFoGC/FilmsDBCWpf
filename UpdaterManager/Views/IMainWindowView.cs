using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdaterManager.Views
{
    public interface IMainWindowView
    {
        String FilePath { get; set; }
        String UpdateInfo { get; set; }
    }
}
