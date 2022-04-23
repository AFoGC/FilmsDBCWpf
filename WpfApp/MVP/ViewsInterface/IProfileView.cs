using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Config;

namespace WpfApp.MVP.ViewsInterface
{
    public interface IProfileView
    {
        void SetVisualSelected();
        void SetVisualDefault();
        void SetSelected(Profile usedProfile);
    }
}
