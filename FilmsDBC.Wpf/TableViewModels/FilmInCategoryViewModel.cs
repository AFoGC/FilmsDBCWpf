using NewTablesLibrary;
using System;
using TL_Objects;
using TL_Tables;
using WpfApp.TableViewModels.Interfaces;

namespace WpfApp.TableViewModels
{
    public class FilmInCategoryViewModel : FilmViewModel
    {
        private CategoriesTable categories;
        public FilmInCategoryViewModel(Film model, IMenuViewModel<Film> menu) : base(model, menu)
        {
            TablesCollection tableCollection = model.ParentTable.ParentCollection;
            categories = tableCollection.GetTableByTableType<CategoriesTable>();
            PropertyChanged += NamePropertyChanged;
        }

        private void NamePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Name))
                OnPropertyChanged(nameof(ShortName));
        }

        public string ShortName
        {
            get
            {
                if (Model.Category?.ID != 0)
                {
                    Category category = Model.Category;
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