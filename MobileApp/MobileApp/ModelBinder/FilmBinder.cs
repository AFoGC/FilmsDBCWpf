using System;
using System.Collections.Generic;
using System.Text;
using TL_Objects;

namespace MobileApp.ModelBinder
{
    public class FilmBinder : BaseBinder<Film>
    {
        public FilmBinder(Film film) : base(film)
        {
            film.Genre.PropertyChanged += Genre_PropertyChanged;
        }

        private void Genre_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged("Genre");
        }

        private static Film defFilm = new Film();
        public String ID { get => Model.ID.ToString(); set { } }
        public virtual String Name { get => Model.Name; set { } }
        public String Genre { get => Model.Genre.ToString(); set { } }
        public String RealiseYear { get => Film.FormatToString(Model.RealiseYear, defFilm.RealiseYear); set { } }
        public Boolean Watched { get => Model.Watched; set { } }
    }
}
