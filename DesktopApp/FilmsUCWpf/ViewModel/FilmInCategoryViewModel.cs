using FilmsUCWpf.ViewModel.Interfaces;
using System;
using TablesLibrary.Interpreter;
using TL_Objects;
using TL_Tables;

namespace FilmsUCWpf.ViewModel
{
    public class FilmInCategoryViewModel : FilmViewModel
    {
        private CategoriesTable categories;
        public FilmInCategoryViewModel(Film model, IMenuViewModel<Film> menu) : base(model, menu)
        {
            TableCollection tableCollection = model.ParentTable.TableCollection;
            categories = (CategoriesTable)tableCollection.GetTable<Category>();
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
                if (Model.FranshiseId != 0)
                {
                    Category category = categories.GetCategoryByFilm(Model);
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