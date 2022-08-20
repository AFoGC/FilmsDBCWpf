using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO_Films
{
    public class UserHasRoleBO
    {
        public Int64 Id { get; set; }


        public Int64 UserId { get; set; }
        public UserBO UserBO { get; set; }

        public Int64 RoleId { get; set; }
        public RoleBO RoleBO { get; set; }
    }
}
