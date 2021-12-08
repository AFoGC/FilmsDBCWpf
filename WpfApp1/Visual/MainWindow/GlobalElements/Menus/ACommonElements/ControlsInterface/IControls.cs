using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace WpfApp.Visual.MainWindow.GlobalElements.Menus.ACommonElements.ControlsInterface
{
    public interface IControls
    {
        bool SetFindedElement(String search);
        void SetVisualDefault();
        void RefreshData();
        Control ToUpdateControl();
    }
}
