using BO_Films;
using ProfilesConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.MVP.Models
{
    public interface IUserModel
    {
        event EventHandler UserChanged;
        UserBO LoggedInUser { get; set; }
        ProgramSettings Settings { get; }
    }
}
