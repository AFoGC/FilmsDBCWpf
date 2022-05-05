using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TablesLibrary.Interpreter;
using TL_Objects;
using TL_Tables;

namespace FilmsUCWpf.ModelBinder
{
    public class FilmSimpleBinder : FilmBinder
    {
        private CategoriesTable categories;
        public FilmSimpleBinder(Film film) : base(film)
        {
            TableCollection tableCollection = film.ParentTable.TableCollection;
            categories = (CategoriesTable)tableCollection.GetTable<Category>();
        }

        public override String Name
        {
            get
            {
                if (Model.FranshiseId != 0)
                {
                    Category category = categories.GetCategoryByFilm(Model);
                    if (category != null)
                        if (category.Name != String.Empty)
                            return Model.Name.Replace(category.Name, String.Empty);
                }
                return Model.Name;
            }
            set { }
        }
    }
}
