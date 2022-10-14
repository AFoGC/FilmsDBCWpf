using FilmsUCWpf.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TL_Objects;
using TL_Objects.Interfaces;

namespace FilmsUCWpf.ViewModel
{
    public class FilmCategoryViewModel : BaseViewModel<Category>, IHasGenre, IFilter, IFinded
    {
        private readonly IMenuViewModel<Film> menu;
        public FilmCategoryViewModel(Category model, IMenuViewModel<Film> menu) : base(model)
        {
            this.menu = menu;


        }

        public bool Filter(IGenre[] selectedGenres, bool isReadedChecked, bool isUnReadedChecked)
        {
            throw new NotImplementedException();
        }

        public bool HasSelectedGenre(IGenre[] selectedGenres)
        {
            throw new NotImplementedException();
        }

        public bool SetFinded(string search)
        {
            throw new NotImplementedException();
        }

        public String ID
        {
            get => Model.ID.ToString();
            set { }
        }
        public String Name
        {
            get => Model.Name;
            set => Model.Name = value;
        }
        public String HideName
        {
            get => Model.HideName;
            set => Model.HideName = value;
        }
        public String Mark
        {
            get => Model.FormatedMark.ToString();
            set => Model.FormatedMark.SetMarkFromString(value);
        }
        public List<String> Marks => Model.FormatedMark.GetComboItems();
    }
}
