using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Visual.MainWindow.GlobalElements.Menus.ACommonElements.ControlsInterface
{
    public interface IBaseControls
    {
        bool SetFindedElement(String search);
        void SetVisualDefault();
        void RefreshData();
        bool HasCheckedProperty(bool isReaded);
    }
}
