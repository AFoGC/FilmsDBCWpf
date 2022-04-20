using BO_Films;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Config;

namespace WpfApp.MVP.Models
{
    public interface IUserModel
    {
        event EventHandler UserChanged;
        UserBO LoggedInUser { get; set; }
        ProgramSettings Settings { get; }
    }
}
