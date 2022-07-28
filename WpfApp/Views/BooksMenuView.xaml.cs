using FilmsUCWpf.View.Interfaces;
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
using TablesLibrary.Interpreter.TableCell;
using TL_Objects.CellDataClasses;
using WpfApp.Models;
using WpfApp.Presenters;
using WpfApp.Views.Interfaces;

namespace WpfApp.Views
{
    /// <summary>
    /// Логика взаимодействия для BooksMenuView.xaml
    /// </summary>
    public partial class BooksMenuView : UserControl, IBaseMenuView
    {
        private BooksMenuPresenter presenter;
        public IList MenuControls => controlsPanel.Children;
        public IList GenresControls => genres_panel.Children;
        private readonly MoreInfoControl infoControl;
        private readonly UpdateControl updateControl;
        private readonly SourcesControl sourcesControl;

        public BooksMenuView(MainWindowModel windowModel)
        {
            InitializeComponent();
            presenter = new BooksMenuPresenter(new BooksMenuModel(windowModel), this);

            infoControl = new MoreInfoControl(this);
            updateControl = new UpdateControl(this);
            sourcesControl = new SourcesControl();
        }

        public void OpenMoreInfo(IView uiElement)
        {
            CloseAllInfos();
            infoControl.Open(uiElement);
            MainInfoPanel.Children.Add(infoControl);
        }
        public void OpenUpdateInfo(IUpdateControl uiElement)
        {
            CloseAllInfos();
            updateControl.Open(uiElement);
            MainInfoPanel.Children.Add(updateControl);
        }
        public void UpdateInUpdateInfo()
        {
            updateControl.Update();
        }
        public void OpenSourcesInfo(TLCollection<Source> sources)
        {
            sourcesControl.Open(sources);
            SourceInfoPanel.Children.Clear();
            SourceInfoPanel.Children.Add(sourcesControl);
        }
        public void CloseAllInfos()
        {
            MainInfoPanel.Children.Clear();
            SourceInfoPanel.Children.Clear();
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

        private void btn_filter_Click(object sender, RoutedEventArgs e)
        {
            presenter.Filter((bool)readed.IsChecked, (bool)unreaded.IsChecked);
        }

        private void btn_showCategories_Click(object sender, RoutedEventArgs e)
        {
            bringToFront(sortPanel, sort_category);
            presenter.LoadCategories();
        }

        private void btn_showPriority_Click(object sender, RoutedEventArgs e)
        {
            bringToFront(sortPanel, sort_priority);
            presenter.LoadPriorityTable();
        }

        private void btn_showBooks_Click(object sender, RoutedEventArgs e)
        {
            bringToFront(sortPanel, sort_books);
            presenter.LoadBooks();
        }

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            presenter.SearchByName(textbox_search.Text);
        }

        private void btn_addCategory_Click(object sender, RoutedEventArgs e)
        {
            presenter.AddCategory();
        }

        private void btn_addBook_Click(object sender, RoutedEventArgs e)
        {
            presenter.AddBook();
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
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

        private void author_sort(object sender, RoutedEventArgs e)
        {
            presenter.SortByAuthor();
        }

        private void genre_sort(object sender, RoutedEventArgs e)
        {
            presenter.SortByGenre();
        }

        private void year_sort(object sender, RoutedEventArgs e)
        {
            presenter.SortByYear();
        }

        private void readed_sort(object sender, RoutedEventArgs e)
        {
            presenter.SortByReaded();
        }

        private void date_sort(object sender, RoutedEventArgs e)
        {
            presenter.SortByDate();
        }

        private void bookmark_sort(object sender, RoutedEventArgs e)
        {
            presenter.SortByBookmark();
        }

        private void countOfReadings_sort(object sender, RoutedEventArgs e)
        {
            presenter.SortByCoR();
        }
    }
}
