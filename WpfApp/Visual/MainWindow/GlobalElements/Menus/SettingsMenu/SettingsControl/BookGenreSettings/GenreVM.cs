using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TL_Objects;
using WpfApp.Visual.MainWindow.GlobalElements.Menus.SettingsMenu.SettingsControl.Commands;

namespace WpfApp.Visual.MainWindow.GlobalElements.Menus.SettingsMenu.SettingsControl.BookGenreSettings
{
    public class GenreVM : INotifyPropertyChanged
    {
        public BookGenre BookGenre { get; private set; }
        public ICommand DeleteGenre { get; private set; }

        public GenreVM(BookGenre genre)
        {
            BookGenre = genre;
            BookGenre.PropertyChanged += FilmGenre_PropertyChanged;
            DeleteGenre = new ActionCommand(DelGenre);
        }

        private void DelGenre()
        {
            MainInfo.Tables.BookGenresTable.Remove(BookGenre);
        }


        public String Name
        {
            get { return BookGenre.Name; }
            set
            {
                BookGenre.Name = value;
                foreach (BookGenre genre in MainInfo.Tables.GenresTable)
                {
                    if (genre.ID == BookGenre.ID)
                    {
                        genre.Name = BookGenre.Name;
                    }
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
