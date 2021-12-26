using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO_Films
{
    public class SaveBO
    {
        public Int64 Id { get; set; }
        public Byte[] FileSave { get; set; }
        public Int64 ProfileId { get; set; }


        public ProfileBO ProfileBO { get; set; }
    }
}
