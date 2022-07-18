using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BO_Launcher
{
    [Serializable]
    public class ProgramBO
    {
        public ProgramBO()
        {
            UpdateInfo = String.Empty;
        }

        public Int64 ID { get; set; }
        public String UpdateInfo { get; set; }
        public DateTime SubmitDate { get; set; }
        public String Version { get; set; }
        public Byte[] ProgramFile { get; set; }
        public Byte[] ZipFile { get; set; }
    }
}
