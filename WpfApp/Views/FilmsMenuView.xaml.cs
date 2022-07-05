using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using WpfApp.Models;
using WpfApp.Presenters;
using WpfApp.ViewsInterface;

namespace WpfApp.Views
{
    /// <summary>
    /// Логика взаимодействия для FilmsMenuView.xaml
    /// </summary>
    public partial class FilmsMenuView : UserControl, IBaseMenuView
    {
        private FilmsMenuPresenter presenter;

        public IList MenuControls => controlsPanel.Children;

        public IList GenresControls => genres_panel.Children;

        public Canvas InfoCanvas => infoCanvas;

        public FilmsMenuView(MainWindowModel windowModel)
        {
            InitializeComponent();
            presenter = new FilmsMenuPresenter(new FilmsMenuModel(windowModel), this);
        }

        private void bringToFront(Grid grid, UIElement element)
        {
            foreach (UIElement item in grid.Children)
            {
                Grid.SetZIndex(item, Grid.GetZIndex(item) - 1);
            }
            Grid.SetZIndex(element, 0);
        }

        private void btn_saveTable_Click(object sender, RoutedEventArgs e)
        {
            presenter.SaveTables();
        }
        private void btn_showCategories_Click(object sender, RoutedEventArgs e)
        {
            bringToFront(sortPanel, sort_category);
            presenter.LoadCategories();
        }
        private void btn_showFilms_Click(object sender, RoutedEventArgs e)
        {
            bringToFront(sortPanel, sort_films);
            presenter.LoadFilmTable();
        }
        private void btn_showSeries_Click(object sender, RoutedEventArgs e)
        {
            bringToFront(sortPanel, sort_series);
            presenter.LoadSerieTable();
        }
        private void btn_showPriority_Click(object sender, RoutedEventArgs e)
        {
            bringToFront(sortPanel, sort_priority);
            presenter.LoadPriorityTable();
        }
        private void btn_addCategory_Click(object sender, RoutedEventArgs e)
        {
            presenter.AddCategory();
        }
        private void btn_addFilm_Click(object sender, RoutedEventArgs e)
        {
            presenter.AddFilm();
        }
        private void btn_filter_Click(object sender, RoutedEventArgs e)
        {
            presenter.Filter(watchedRequestControl.IsWatched, watchedRequestControl.IsUnwatched);
        }
        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            presenter.SearchByName(textbox_search.Text);
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Enter))
            {
                presenter.UpdateVisualizerIfOpen();
            }

            if (e.Key == Key.S && (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
            {
                presenter.UpdateVisualizerIfOpen();
            }
        }

        private void id_sort(object sender, RoutedEventArgs e)
        {
            presenter.SortByID();
        }

        private void name_sort(object sender, RoutedEventArgs e)
        {
            presenter.SortByName();
        }

        private void mark_sort(object sender, RoutedEventArgs e)
        {
            presenter.SortByMark();
        }

        private void genre_sort(object sender, RoutedEventArgs e)
        {
            presenter.SortByGenre();
        }

        private void year_sort(object sender, RoutedEventArgs e)
        {
            presenter.SortByYear();
        }

        private void watched_sort(object sender, RoutedEventArgs e)
        {
            presenter.SortByWatched();
        }

        private void start_date_sort(object sender, RoutedEventArgs e)
        {
            presenter.SortByStartDate();
        }

        private void watched_series_sort(object sender, RoutedEventArgs e)
        {
            presenter.SortByWatchedSeries();
        }

        private void date_sort(object sender, RoutedEventArgs e)
        {
            presenter.SortByDate();
        }

        private void total_series_sort(object sender, RoutedEventArgs e)
        {
            presenter.SortByTotalSeries();
        }

        private void countOfViews_sort(object sender, RoutedEventArgs e)
        {
            presenter.SortByCoV();
        }
    }
}
