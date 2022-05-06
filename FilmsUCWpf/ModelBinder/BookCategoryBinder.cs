using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TL_Objects;

namespace FilmsUCWpf.ModelBinder
{
    public class BookCategoryBinder : BaseBinder<BookCategory>
    {
        public BookCategoryBinder(BookCategory category) : base(category) { }

        private static BookCategory defCat = new BookCategory();
        public String ID { get => Model.ID.ToString(); set { } }
        public String Name { get => Model.Name; set { } }
        public String Mark { get => Helper.MarkToText(BookCategory.FormatToString(Model.Mark, defCat.Mark)); set { } }
    }
}
