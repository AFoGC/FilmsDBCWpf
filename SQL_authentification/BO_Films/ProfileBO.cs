using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO_Films
{
    public class ProfileBO
    {
        public Int64 Id { get; set; }
        public String Name { get; set; }
        public Byte[] Lastsave { get; set; }
        public Int64 UserId { get; set; }


        //public UserBO UserBO { get; set; }
    }
}
