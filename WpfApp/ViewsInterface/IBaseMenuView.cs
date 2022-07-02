using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp.ViewsInterface
{
    public interface IBaseMenuView
    {
        IList MenuControls { get; }
        IList GenresControls { get; }
        Canvas InfoCanvas { get; }
    }
}
