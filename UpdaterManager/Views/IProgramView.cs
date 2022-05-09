using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdaterManager.Views
{
    public interface IProgramView
    {
        String UpdateInfo { get; }
        String ProgramPath { get; }
        String VersionInfo { set; }
    }
}
