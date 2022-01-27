using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InfoMenus.Interfaces
{
    public interface IControls
    {
        bool SetFindedElement(String search);
        void SetVisualDefault();
        //void SetSelectedElement();
        Control ToUpdateControl();
    }
}
