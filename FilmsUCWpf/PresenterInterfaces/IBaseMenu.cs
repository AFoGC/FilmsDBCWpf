using InfoMenusWpf.MoreInfo;
using InfoMenusWpf.UpdateInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmsUCWpf.PresenterInterfaces
{
    public interface IBaseMenu
    {
        MoreInfoFormVisualizer MoreInfoFormVisualizer { get; }
        UpdateFormVisualizer UpdateFormVisualizer { get; }
    }
}
