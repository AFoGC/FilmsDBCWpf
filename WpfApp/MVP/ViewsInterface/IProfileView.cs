using ProfilesConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.MVP.ViewsInterface
{
    public interface IProfileView
    {
        void SetVisualSelected();
        void SetVisualDefault();
        void SetSelected(Profile usedProfile);
    }
}
