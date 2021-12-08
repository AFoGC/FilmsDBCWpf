using WpfApp.Visual.MainWindow.GlobalElements.Menus.ACommonElements.InfoMenus.MoreInfo;
using WpfApp.Visual.MainWindow.GlobalElements.Menus.ACommonElements.InfoMenus.UpdateInfo;
using WpfApp.Visual.MainWindow.GlobalElements.Menus.FilmsMenu.FilmsControls;
using System;
using System.Collections.Generic;
using System.Text;
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
using TL_Tables;

namespace WpfApp.Visual.MainWindow.GlobalElements.Menus.FilmsMenu
{
    /// <summary>
    /// Логика взаимодействия для FilmsMenuControl.xaml
    /// </summary>
    public partial class FilmsMenuControl : UserControl
    {
        public enum MenuCondition
        {
            Category = 1,
            Film = 2,
            Serie = 3,
            PriorityFilm = 4
        }

        private MenuCondition controlsCondition = MenuCondition.Category;
        public MenuCondition ControlsCondition { get { return controlsCondition; } }

        public FilmsMenuControl()
        {
            InitializeComponent();
            moreInfoVisualizer = new MoreInfoFormVisualizer(infoCanvas);
            updateVisualizer = new UpdateFormVisualizer(infoCanvas);
            MainInfo.TableCollection.TableLoad += new EventHandler(this.TableCollection_TablesLoad);
        }

        private void TableCollection_TablesLoad(object sender, EventArgs e)
        {
            loadCategories();
            loadGenres();
        }

        private MoreInfoFormVisualizer moreInfoVisualizer;
        public MoreInfoFormVisualizer MoreInfoVisualizer
        {
            get { return moreInfoVisualizer; }
        }
        private UpdateFormVisualizer updateVisualizer;
        public UpdateFormVisualizer UpdateVisualizer
        {
            get { return updateVisualizer; }
        }

        private List<UserControl> tableControls = new List<UserControl>();
        private AElementControl controlInBuffer = null;
        public AElementControl ControlInBuffer
        {
            get { return controlInBuffer; }
            set { controlInBuffer = value; }
        }

        public void loadGenres()
        {
            genres_panel.Children.Clear();
            foreach (Genre genre in MainInfo.Tables.GenresTable)
            {
                //genres_panel.Children.Add(new GenrePressButton(genre));
            }
        }

        private void clearControls()
        {
            controlsPanel.Children.Clear();
            tableControls.Clear();
        }

        private void lockNotSerialGenreButtons()
        {
            /*
            foreach (GenrePressButton button in flowLayoutPanel_requestsGenres.Controls)
            {
                if (!button.Genre.IsSerialGenre)
                {
                    button.Included = false;
                    button.ClickLocked = true;
                }
            }
            */
        }
        private void unLockGenreButtons()
        {
            /*
            foreach (GenrePressButton button in flowLayoutPanel_requestsGenres.Controls)
            {
                button.ClickLocked = false;
                button.Included = true;
            }
            */
        }

        public void loadFilmTable()
        {
            clearControls();
            controlsCondition = MenuCondition.Film;
            controlInBuffer = null;

            foreach (Film film in MainInfo.Tables.FilmsTable)
            {
                tableControls.Add(new FilmControl(film));
            }

            foreach (UserControl control in tableControls)
            {
                controlsPanel.Children.Add(control);
            }

            unLockGenreButtons();
        }

        public void loadSerieTable()
        {
            clearControls();
            controlsCondition = MenuCondition.Serie;
            controlInBuffer = null;

            foreach (Film film in MainInfo.Tables.FilmsTable)
            {
                if (film.Genre.IsSerialGenre)
                {
                    tableControls.Add(new SerieControl(film));
                }
            }
            foreach (UserControl control in tableControls)
            {
                controlsPanel.Children.Add(control);
            }

            lockNotSerialGenreButtons();
        }

        public void loadCategories()
        {
            clearControls();
            controlsCondition = MenuCondition.Category;
            controlInBuffer = null;

            foreach (Category category in MainInfo.Tables.CategoriesTable)
            {
                tableControls.Add(new CategoryControl(category));
            }

            foreach (Film film in MainInfo.Tables.FilmsTable)
            {
                if (film.FranshiseId == 0)
                {
                    tableControls.Add(new SimpleControl(film));
                }
            }

            foreach (UserControl control in tableControls)
            {
                controlsPanel.Children.Add(control);
            }

            unLockGenreButtons();
        }

        public void loadPriorityTable()
        {
            clearControls();
            controlsCondition = MenuCondition.PriorityFilm;
            controlInBuffer = null;

            PriorityFilmsTable priTable = MainInfo.Tables.PriorityFilmsTable;

            PriorityFilm film;
            for (int i = 0; i < MainInfo.Tables.PriorityFilmsTable.Count; i++)
            {
                film = priTable[i];

                if (film.Film.Watched)
                {
                    MainInfo.Tables.PriorityFilmsTable.Remove(film);
                    --i;
                }
                else
                {
                    tableControls.Add(new SimpleControl(film.Film));
                }
            }

            foreach (UserControl control in tableControls)
            {
                controlsPanel.Children.Add(control);
            }
            unLockGenreButtons();
        }

        private Genre[] getSelectedGenres()
        {
            /*
            List<Genre> genres = new List<Genre>();
            foreach (GenrePressButton requestControl in genres_panel.Children)
            {
                if (requestControl.Included)
                {
                    genres.Add(requestControl.Genre);
                }
            }
            return genres.ToArray();
            */
            throw new Exception();
        }
    }
}
