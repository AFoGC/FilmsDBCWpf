using FilmsUCWpf.Presenter;
using FilmsUCWpf.PresenterInterfaces;
using InfoMenusWpf.MoreInfo;
using InfoMenusWpf.UpdateInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TL_Objects;

namespace WpfApp.MVP.Models
{
    public class FilmsMenuModel
    {
        public enum MenuCondition
        {
            Category = 1,
            Film = 2,
            Serie = 3,
            PriorityFilm = 4
        }

        public MenuCondition ControlsCondition { get; set; }
        public MoreInfoFormVisualizer MoreInfoFormVisualizer { get; set; }
        public UpdateFormVisualizer UpdateFormVisualizer { get; set; }
        public List<IBasePresenter> BasePresenters { get; private set; } 
        private FilmPresenter selectedElement = null;
        public FilmPresenter SelectedElement
        {
            get { return selectedElement; }
            set
            {
                if (selectedElement != null) selectedElement.SetVisualDefault();
                selectedElement = value;
            }
        }

        public FilmsMenuModel()
        {
            BasePresenters = new List<IBasePresenter>();
            ControlsCondition = MenuCondition.Category;
        }
    }
}
