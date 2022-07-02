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

        private void btn_saveTable_Click(object sender, RoutedEventArgs e)
        {
            presenter.SaveTables();
        }
        private void btn_showCategories_Click(object sender, RoutedEventArgs e)
        {
            presenter.LoadCategories();
        }
        private void btn_showFilms_Click(object sender, RoutedEventArgs e)
        {
            presenter.LoadFilmTable();
        }
        private void btn_showSeries_Click(object sender, RoutedEventArgs e)
        {
            presenter.LoadSerieTable();
        }
        private void btn_showPriority_Click(object sender, RoutedEventArgs e)
        {
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
    }
}
