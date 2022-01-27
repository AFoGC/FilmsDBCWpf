using InfoMenus.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TL_Objects;

namespace TablesControlsWPF.FilmControls
{
    /// <summary>
    /// Логика взаимодействия для FilmControl.xaml
    /// </summary>
    public partial class FilmControl : UserControl, IControls, INotifyPropertyChanged
    {
        public Film Film { get; private set; }
        private static readonly Film defFilm;
        private IMenuControl<Film> menuControl;

        static FilmControl()
        {
            defFilm = new Film();
        }

        public FilmControl(Film film, IMenuControl<Film> menuControl)
        {
            Film = film;
            this.menuControl = menuControl;
            DataContext = this;
            Film.PropertyChanged += Film_PropertyChanged;
            InitializeComponent();
        }

        private void id_GotFocus(object sender, RoutedEventArgs e)
        {
            SolidColorBrush myBrush = new SolidColorBrush(Color.FromRgb(0, 0, 220));
            this.id.Background = myBrush;
            menuControl.ElementInBuffer = Film;
        }

        private void btn_comment_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_copyUrl_Click(object sender, RoutedEventArgs e)
        {
            if (Film.Sources.Count != 0)
            {
                Clipboard.SetText(Film.Sources[0].SourceUrl);
            }
        }

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            menuControl.UpdateInfoVisualizer.OpenUpdateControl(this);
        }

        public bool SetFindedElement(string search)
        {
            if (this.name.Text.ToLowerInvariant().Contains(search))
            {
                SolidColorBrush myBrush = new SolidColorBrush(Color.FromRgb(0, 0, 220));
                this.id.Background = myBrush;
                return true;
            }

            return false;
        }

        public void SetVisualDefault()
        {
            SolidColorBrush myBrush = new SolidColorBrush(Color.FromRgb(53, 53, 53));
            this.id.Background = myBrush;
        }

        public Control ToUpdateControl()
        {
            throw new NotImplementedException();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Film_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e);
        }

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, e);
        }

        public int ID
        {
            get { return Film.ID; } set { }
        }
        public string FilmName
        {
            get { return Film.Name; } set { }
        }
        public Genre Genre
        {
            get { return Film.Genre; } set { }
        }
        public String RealiseYear
        {
            get { return Film.FormatToString(Film.RealiseYear, defFilm.RealiseYear); } set { }
        }
        public bool Watched { get { return Film.Watched; } set { } }
        //public String Mark { get { return Film.FormatToString(Film.Mark, defFilm.RealiseYear); } }
    }
}
