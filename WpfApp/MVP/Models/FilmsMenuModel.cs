using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfApp.Visual.MainWindow.GlobalElements.Menus.ACommonElements.ControlsInterface;
using WpfApp.Visual.MainWindow.GlobalElements.Menus.ACommonElements.InfoMenus.MoreInfo;
using WpfApp.Visual.MainWindow.GlobalElements.Menus.ACommonElements.InfoMenus.UpdateInfo;

namespace WpfApp.MVP.Models
{
    public class FilmsMenuModel
    {
        public MenuCondition ControlsCondition { get; set; }
        public MoreInfoFormVisualizer MoreInfoFormVisualizer { get; private set; }
        public UpdateFormVisualizer UpdateFormVisualizer { get; private set; }
        public List<UserControl> TableControls { get; private set; } 
        private IBaseControls controlInBuffer = null;
        public IBaseControls ControlInBuffer
        {
            get { return controlInBuffer; }
            set
            {
                if (controlInBuffer != null) controlInBuffer.SetVisualDefault();
                controlInBuffer = value;
            }
        }

        public FilmsMenuModel()
        {
            TableControls = new List<UserControl>();
        }

        public enum MenuCondition
        {
            Category = 1,
            Film = 2,
            Serie = 3,
            PriorityFilm = 4
        }
    }
}
