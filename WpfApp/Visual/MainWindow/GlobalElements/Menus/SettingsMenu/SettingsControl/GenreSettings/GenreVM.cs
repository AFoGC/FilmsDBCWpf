using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TL_Objects;
using WpfApp.Visual.MainWindow.GlobalElements.Menus.SettingsMenu.SettingsControl.Commands;

namespace WpfApp.Visual.MainWindow.GlobalElements.Menus.SettingsMenu.SettingsControl.GenreSettings
{
    public class GenreVM : INotifyPropertyChanged
    {
        public Genre FilmGenre { get; private set; }
        public ICommand DeleteGenre { get; private set; }
        //public ICommand ChangeSerial { get; private set; }

        public GenreVM(Genre genre)
        {
            FilmGenre = genre;
            FilmGenre.PropertyChanged += FilmGenre_PropertyChanged;
            DeleteGenre = new GenreCommand(DelGenre, genre);
        }

        private void DelGenre()
        {
            MainInfo.Tables.GenresTable.Remove(FilmGenre);
        }


        public String Name
        { 
            get { return FilmGenre.Name; }
            set
            {
                FilmGenre.Name = value;
                foreach (Genre genre in MainInfo.Tables.GenresTable)
                {
                    if (genre.ID == FilmGenre.ID)
                    {
                        genre.Name = FilmGenre.Name;
                    }
                }
            }
        }

        public Boolean IsSerialGenre
        {
            get { return FilmGenre.IsSerialGenre; }
            set
            {
                if (!MainInfo.Tables.FilmsTable.GenreHasFilm(FilmGenre))
                {
                    FilmGenre.IsSerialGenre = value;
                }
            }
        }

        private void FilmGenre_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, e);
        }

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, e);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
