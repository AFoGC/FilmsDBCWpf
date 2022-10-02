using FilmsUCWpf.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TablesLibrary.Interpreter;
using TL_Objects;
using TL_Tables;

namespace FilmsUCWpf.ViewModel
{
    public class BookInCategoryViewModel : BookViewModel
    {
        private readonly BookCategoriesTable bookCategories;
        public BookInCategoryViewModel(Book model, IMenuViewModel<Book> menu) : base(model, menu)
        {
            TableCollection tableCollection = model.ParentTable.TableCollection;
            bookCategories = (BookCategoriesTable)tableCollection.GetTable<BookCategory>();
        }

        public override String Name
        {
            get
            {
                if (Model.FranshiseId != 0)
                {
                    BookCategory category = bookCategories.GetCategoryByBook(Model);
                    if (category != null)
                    {
                        if (category.HideName != String.Empty)
                        {
                            return Model.Name.Replace(category.HideName, String.Empty);
                        }

                        if (category.Name != String.Empty)
                        {
                            return Model.Name.Replace(category.Name, String.Empty);
                        }
                    }
                }
                return Model.Name;
            }
            set { }
        }
    }
}
