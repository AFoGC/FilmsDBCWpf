using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TL_Objects;

namespace FilmsUCWpf.ModelBinder
{
    public class FilmCategoryBinder : BaseBinder<Category>
    {
        public FilmCategoryBinder(Category category) : base(category) { }

        private static Category defCat = new Category();
        public String ID { get => Model.ID.ToString(); set { } }
        public String Name { get => Model.Name; set { } }
        public String Mark { get => Model.FormatedMark.ToString(); set { } }
    }
}
