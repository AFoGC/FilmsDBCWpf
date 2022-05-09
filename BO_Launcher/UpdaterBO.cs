using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO_Launcher
{
    public class UpdaterBO
    {
        public UpdaterBO()
        {
            Version = String.Empty;
        }

        public Int64 ID { get; set; }
        public byte[] UpdaterFile { get; set; }
        public DateTime SubmitDate { get; set; }
        public String Version { get; set; }
    }
}
