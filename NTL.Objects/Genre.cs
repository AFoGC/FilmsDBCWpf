using NewTablesLibrary;
using TL_Objects.Interfaces;

namespace TL_Objects
{
    [Cell("Genre")]
    public class Genre : Cell, IGenre
    {
        [SaveField("name")]
        private string _name;

        [SaveField("isSerialGenre")]
        private bool _isSerialGenre;

        private readonly OneToManyCollection<Genre, Film> _films;

        public Genre()
        {
            _name = String.Empty;
            _isSerialGenre = false;

            _films = new OneToManyCollection<Genre, Film>(this);
        }

        protected override void OnRemoving()
        {
            _films.Clear();

            base.OnRemoving();
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        public bool IsSerialGenre
        {
            get { return _isSerialGenre; }
            set { _isSerialGenre = value; OnPropertyChanged(); }
        }

        public ICollection<Film> Films => _films;

        public override string ToString()
        {
            return this._name;
        }
    }
}
