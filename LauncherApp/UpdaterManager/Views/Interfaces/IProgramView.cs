using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdaterManager.Views.Interfaces
{
    public interface IProgramView
    {
        string UpdateInfo { get; }
        string ProgramPath { get; }
        string ZipPath { get; }
        string VersionInfo { set; }
    }
}
