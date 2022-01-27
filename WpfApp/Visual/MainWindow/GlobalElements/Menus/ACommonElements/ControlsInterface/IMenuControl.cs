using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Visual.MainWindow.GlobalElements.Menus.ACommonElements.InfoMenus.MoreInfo;
using WpfApp.Visual.MainWindow.GlobalElements.Menus.ACommonElements.InfoMenus.UpdateInfo;

namespace WpfApp.Visual.MainWindow.GlobalElements.Menus.ACommonElements.ControlsInterface
{
    public interface IMenuControl
    {
        MoreInfoFormVisualizer MoreInfoFormVisualizer { get; }
        UpdateFormVisualizer UpdateFormVisualizer { get; }
    }
}
