using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        [field: NonSerialized]
        public Byte[] ProgramFile { get; set; }
    }
}
