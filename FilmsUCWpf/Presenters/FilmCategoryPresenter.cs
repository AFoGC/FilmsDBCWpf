using FilmsUCWpf.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TL_Objects;

namespace FilmsUCWpf.Presenters
{
    public class FilmCategoryPresenter : BasePresenter<Category>
    {
        protected IMenu<Film> menu;
        protected List<SimpleFilmPresenter> presenters;
        
        public new ICategoryView<Category> View
        {
            get { return (ICategoryView<Category>)base.View; }
        }

        public FilmCategoryPresenter(Category category, ICategoryView<Category> view, IMenu<Film> menu) : base(category, view)
        {
            this.menu = menu;
            presenters = new List<SimpleFilmPresenter>();
        }

        private void AddSimpleControl(Film film)
        {
            //View.CategoryCollection.Add();
        }

        public override bool HasCheckedProperty(bool isWached)
        {
            /*
            foreach (SimpleControl control in cat_panel.Children)
            {
                if (control.HasCheckedProperty(isWached))
                {
                    return true;
                }
            }

            return false;
            */
            return false;
        }

        public override bool SetFindedElement(string search)
        {
            throw new NotImplementedException();
        }
    }
}
