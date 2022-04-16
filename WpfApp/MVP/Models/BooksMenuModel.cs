using FilmsUCWpf.Presenter;
using FilmsUCWpf.PresenterInterfaces;
using InfoMenusWpf.MoreInfo;
using InfoMenusWpf.UpdateInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.MVP.Models
{
    public class BooksMenuModel
    {
        public enum MenuCondition
        {
            Category = 1,
            Book = 2,
            PriorityBook = 3
        }

        public MenuCondition ControlsCondition { get; set; }
        public MoreInfoFormVisualizer MoreInfoFormVisualizer { get; set; }
        public UpdateFormVisualizer UpdateFormVisualizer { get; set; }
        public List<IBasePresenter> BasePresenters { get; private set; }
        private BookPresenter selectedElement = null;
        public BookPresenter SelectedElement
        {
            get { return selectedElement; }
            set
            {
                if (selectedElement != null) selectedElement.SetVisualDefault();
                selectedElement = value;
            }
        }

        public BooksMenuModel()
        {
            BasePresenters = new List<IBasePresenter>();
            ControlsCondition = MenuCondition.Category;
        }
    }
}
