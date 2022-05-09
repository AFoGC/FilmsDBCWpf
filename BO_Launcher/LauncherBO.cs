using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BO_Launcher
{
    public class LauncherBO
    {
        public LauncherBO()
        {
            Version = String.Empty;
        }

        public Int64 ID { get; set; }
        public byte[] LauncherFile { get; set; }
        public DateTime SubmitDate { get; set; }
        public String Version { get; set; }
    }
}
