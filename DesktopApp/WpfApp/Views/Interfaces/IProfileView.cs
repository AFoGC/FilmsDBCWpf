using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Models;

namespace WpfApp.Views.Interfaces
{
    public interface IProfileView
    {
        void SetVisualSelected();
        void SetVisualDefault();
        void SetSelected(Profile usedProfile);
    }
}
