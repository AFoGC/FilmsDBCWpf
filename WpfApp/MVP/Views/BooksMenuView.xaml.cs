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
using WpfApp.MVP.Models;
using WpfApp.MVP.Presenters;
using WpfApp.MVP.ViewsInterface;

namespace WpfApp.MVP.Views
{
    /// <summary>
    /// Логика взаимодействия для BooksMenuView.xaml
    /// </summary>
    public partial class BooksMenuView : UserControl, IBaseMenuView
    {
        private BooksMenuPresenter presenter;
        public IList MenuControls => controlsPanel.Children;
        public IList GenresControls => genres_panel.Children;
        public Canvas InfoCanvas => infoCanvas;

        public BooksMenuView(MainWindowModel windowModel)
        {
            InitializeComponent();
            watchedRequestControl.LeftText = "Readed";
            watchedRequestControl.RightText = "Unreaded";
            presenter = new BooksMenuPresenter(new Models.BooksMenuModel(), this, windowModel);
        }

        private void btn_saveTable_Click(object sender, RoutedEventArgs e)
        {
            presenter.SaveTables();
        }

        private void btn_filter_Click(object sender, RoutedEventArgs e)
        {
            presenter.Filter(watchedRequestControl.IsWatched, watchedRequestControl.IsUnwatched);
        }

        private void btn_showCategories_Click(object sender, RoutedEventArgs e)
        {
            new Thread(() =>
            {
                Dispatcher.Invoke(presenter.LoadCategories);
            }).Start();
        }

        private void btn_addCategory_Click(object sender, RoutedEventArgs e)
        {
            presenter.AddCategory();
        }

        private void btn_showBooks_Click(object sender, RoutedEventArgs e)
        {
            new Thread(() =>
            {
                Dispatcher.Invoke(presenter.LoadBooks);
            }).Start();
        }

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            presenter.SearchByName(textbox_search.Text);
        }

        private void btn_showPriority_Click(object sender, RoutedEventArgs e)
        {
            new Thread(() =>
            {
                Dispatcher.Invoke(presenter.LoadPriorityTable);
            }).Start();
        }

        private void btn_addBook_Click(object sender, RoutedEventArgs e)
        {
            presenter.AddBook();
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Enter) && presenter.UpdateFormVisualizer.IsOpen)
            {
                presenter.UpdateFormVisualizer.UpdateControl.Update();
            }

            if (e.Key == Key.S && (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
            {
                if (presenter.UpdateFormVisualizer.IsOpen)
                    presenter.UpdateFormVisualizer.UpdateControl.Update();
            }
        }
    }
}
