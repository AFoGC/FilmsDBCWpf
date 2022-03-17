using FilmsUCWpf.PresenterInterfaces;
using FilmsUCWpf.ViewInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TL_Objects;

namespace FilmsUCWpf.Presenter
{
    public class FilmPresenter : BasePresenter<Film>
    {

        protected IMenu<Film> menu;
        public FilmPresenter(Film film, IBaseView view, IMenu<Film> menu) : base(film, view)
        {
            this.menu = menu;
        }

        public override bool HasCheckedProperty(bool isWatched)
        {
            return isWatched == Model.Watched;
        }

        public override bool SetFindedElement(string search)
        {
            if (this.Model.Name.ToLowerInvariant().Contains(search))
            {
                View.SetVisualFinded();
                return true;
            }

            return false;
        }

        public override void SetSelectedElement()
        {
            if (menu.ControlInBuffer != null)
            {
                menu.ControlInBuffer = this;
                View.SetVisualSelected();
            }
        }

        public override void SetVisualDefault()
        {
            View.SetVisualDefault();
        }

        public void OpenUpdateMenu()
        {
            //menu.UpdateFormVisualizer.OpenUpdateControl(new FilmUpdateControl());
        }

        public void OpenInfoMenu()
        {
            /*
            Control control = new Control();
            if (Model.Serie == null)
            {
                control = new FilmControl(Model, menu);
            }
            else
            {
                control = new SerieControl(Model, menu);
            }

            menu.MoreInfoFormVisualizer.OpenMoreInfoForm(control);
            */
        }

        public String ID { get => Model.ID.ToString(); set { } }
        public String Name { get => Model.Name; set { } }
        public String Genre { get => Model.Genre.ToString(); set { } }
        
    }
}
