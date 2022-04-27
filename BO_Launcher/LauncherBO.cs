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
        public Int64 ID { get; set; }

        [XmlIgnore]
        public byte[] LauncherFile { get; set; }
    }
}
