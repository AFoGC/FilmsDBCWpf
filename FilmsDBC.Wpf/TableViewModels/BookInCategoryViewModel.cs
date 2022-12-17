using System;
using TablesLibrary.Interpreter;
using TL_Objects;
using TL_Tables;
using WpfApp.TableViewModels.Interfaces;

namespace WpfApp.TableViewModels
{
    public class BookInCategoryViewModel : BookViewModel
    {
        private readonly BookCategoriesTable bookCategories;
        public BookInCategoryViewModel(Book model, IMenuViewModel<Book> menu) : base(model, menu)
        {
            TableCollection tableCollection = model.ParentTable.TableCollection;
            bookCategories = (BookCategoriesTable)tableCollection.GetTable<BookCategory>();
            PropertyChanged += NamePropertyChanged;
        }

        private void NamePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Name))
                OnPropertyChanged(nameof(ShortName));
        }

        public String ShortName
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
        }
    }
}
