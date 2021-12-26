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
using WpfApp.Visual.MainWindow.GlobalElements.Menus.ACommonElements.MenuElements;
using WpfApp.Visual.MainWindow.GlobalElements.Menus.ACommonElements.ControlsInterface;

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
            moreInfoVisualizer.UpdateVisualizer = updateVisualizer;
            updateVisualizer.MoreVisualizer = moreInfoVisualizer;
            MainInfo.TableCollection.TableLoad += new EventHandler(this.TableCollection_TablesLoad);
        }

        private void TableCollection_TablesLoad(object sender, EventArgs e)
        {
            loadCategories();
            LoadGenres();
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

        public void LoadGenres()
        {
            genres_panel.Children.Clear();
            foreach (Genre genre in MainInfo.Tables.GenresTable)
            {
                genres_panel.Children.Add(new GenrePressButtonControl(genre));
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
            foreach (GenrePressButtonControl button in genres_panel.Children)
            {
                if (!button.Genre.IsSerialGenre)
                {
                    button.PressButton.Included = false;
                    button.PressButton.ClickLocked = true;
                }
            }
            */
        }
        private void unLockGenreButtons()
        {
            /*
            foreach (GenrePressButtonControl button in genres_panel.Children)
            {
                button.PressButton.ClickLocked = false;
                button.PressButton.Included = true;
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
            List<Genre> genres = new List<Genre>();
            foreach (GenrePressButtonControl requestControl in genres_panel.Children)
            {
                if (requestControl.PressButton.Included)
                {
                    genres.Add(requestControl.Genre);
                }
            }
            return genres.ToArray();
        }

        private void btn_saveTable_Click(object sender, RoutedEventArgs e)
        {
            MainInfo.TableCollection.SaveTables();
        }

        private void btn_showCategories_Click(object sender, RoutedEventArgs e)
        {
            loadCategories();
        }

        private void btn_showFilms_Click(object sender, RoutedEventArgs e)
        {
            loadFilmTable();
        }

        private void btn_showSeries_Click(object sender, RoutedEventArgs e)
        {
            loadSerieTable();
        }

        private void btn_showPriority_Click(object sender, RoutedEventArgs e)
        {
            loadPriorityTable();
        }

        private void btn_addCategory_Click(object sender, RoutedEventArgs e)
        {
            if (controlsCondition == MenuCondition.Category)
            {
                CategoriesTable categoryTable = MainInfo.Tables.CategoriesTable;
                categoryTable.AddElement();
                CategoryControl categoryControl = new CategoryControl((Category)categoryTable.GetLastElement);
                controlsPanel.Children.Insert(categoryTable.Count - 1, categoryControl);
            }
        }

        private void btn_addFilm_Click(object sender, RoutedEventArgs e)
        {
            MainInfo.Tables.FilmsTable.AddElement();
            Film film = MainInfo.Tables.FilmsTable.GetLastElement;
            IControls control = new SimpleControl(film);
            switch (controlsCondition)
            {
                case MenuCondition.Category:
                    break;
                case MenuCondition.Film:
                    control = new FilmControl(film);
                    break;
                case MenuCondition.Serie:
                    film.Genre = MainInfo.Tables.GenresTable.GetFirstSeriealGenre();
                    control = new SerieControl(film);
                    break;
                default:
                    return;
            }

            tableControls.Add((UserControl)control);
            controlsPanel.Children.Add((UserControl)control);
        }

        private void btn_AddToPriority_Click(object sender, RoutedEventArgs e)
        {
            if (controlInBuffer != null)
            {
                MainInfo.Tables.PriorityFilmsTable.AddElement();
                PriorityFilm priorityFilm = MainInfo.Tables.PriorityFilmsTable.GetLastElement;
                priorityFilm.Film = controlInBuffer.FilmInfo;
            }
        }

        private void btn_filter_Click(object sender, RoutedEventArgs e)
        {
            controlsPanel.Children.Clear();
            Genre[] genres = getSelectedGenres();

            foreach (IFilmsControl control in tableControls)
            {
                if (watchedRequestControl.IsAllIncluded)
                {
                    if (control.HasSelectedGenre(genres))
                    {
                        controlsPanel.Children.Add((UserControl)control);
                    }
                }
                else
                {
                    if (control.HasSelectedGenre(genres) && control.HasWatchedProperty(watchedRequestControl.IsWatched))
                    {
                        controlsPanel.Children.Add((UserControl)control);
                    }
                }
            }
        }

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            foreach (IControls control in controlsPanel.Children)
            {
                control.SetVisualDefault();
            }

            if (textbox_search.Text != "")
            {
                String searchStr = textbox_search.Text.ToLowerInvariant();

                foreach (IControls control in controlsPanel.Children)
                {
                    control.SetFindedElement(searchStr);
                }
            }
        }
    }
}
