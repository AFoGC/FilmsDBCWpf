using NewTablesLibrary;
using System;
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
            TablesCollection tableCollection = model.ParentTable.ParentCollection;
            bookCategories = tableCollection.GetTableByTableType<BookCategoriesTable>();
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
                if (Model.Category?.ID != 0)
                {
                    BookCategory category = Model.Category;
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
