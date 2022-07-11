using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Views.Interfaces
{
    public interface IProfileSettingsContainerView
    {
        IList ProfileControls { get; }
        double Height { get; set; }
        double DefaultHeight { get; }
        string AddProfileText { get; set; }
    }
}
