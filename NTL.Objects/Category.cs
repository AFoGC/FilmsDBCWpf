using NewTablesLibrary;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using TL_Objects.CellDataClasses;
using TL_Objects.Interfaces;

namespace TL_Objects
{
    [Cell("Category")]
    public class Category : Cell, ICategory<Film>
    {
        [SaveField("name")]
        private string _name;

        [SaveField("hideName")]
        private string _hideName;

        [SaveField("mark")]
        private Mark _mark;

        [SaveField("priority")]
        private int _priority;

        private OneToManyCollection<Category, Film> _films;

        public Category()
        {
            _name = String.Empty;
            _hideName = String.Empty;
            _mark = new Mark();
            _priority = 0;
            _films = new OneToManyCollection<Category, Film>(this);

            _mark.PropertyChanged += Mark_PropertyChanged;
        }

        protected override void OnRemoving()
        {
            _films.Clear();

            base.OnRemoving();
        }

        private void Mark_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Mark));
            OnPropertyChanged(nameof(FormatedMark));
        }

        
        public bool ChangeFilmPositionBy(Film film, int i)
        {
            return false;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        public string HideName
        {
            get { return _hideName; }
            set 
            { 
                _hideName = value;
                //TODO сделать оповещение фильмов о том чтобы они обновили свой HideName
                /*
                foreach (Film film in CategoryElements)
                {
                    film.OnPropertyChanged(nameof(film.Name));
                }
                */
                OnPropertyChanged(nameof(HideName)); 
            }
        }

        public int Mark
        {
            get { return _mark.RawMark; }
            set { _mark.RawMark = value; OnPropertyChanged(nameof(Mark)); }
        }

        public Mark FormatedMark
        {
            get => _mark;
        }

        public int Priority
        {
            get { return _priority; }
            set { _priority = value; OnPropertyChanged(nameof(Priority)); }
        }

        public OneToManyCollection<Category, Film> CategoryElements
        {
            get { return _films; }
        }

        ICollection<Film> ICategory<Film>.CategoryElements
        {
            get => CategoryElements;
        }
    }
}
