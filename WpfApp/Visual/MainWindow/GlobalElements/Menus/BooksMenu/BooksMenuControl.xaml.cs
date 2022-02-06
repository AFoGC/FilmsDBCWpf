using System;
using System.Collections.Generic;
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
using TL_Tables;
using WpfApp.Visual.MainWindow.GlobalElements.Menus.ACommonElements.ControlsInterface;
using WpfApp.Visual.MainWindow.GlobalElements.Menus.ACommonElements.InfoMenus.MoreInfo;
using WpfApp.Visual.MainWindow.GlobalElements.Menus.ACommonElements.InfoMenus.UpdateInfo;
using WpfApp.Visual.MainWindow.GlobalElements.Menus.ACommonElements.MenuElements;
using WpfApp.Visual.MainWindow.GlobalElements.Menus.BooksMenu.BooksControls;

namespace WpfApp.Visual.MainWindow.GlobalElements.Menus.BooksMenu
{
    /// <summary>
    /// Логика взаимодействия для BooksMenuControl.xaml
    /// </summary>
    public partial class BooksMenuControl : UserControl, IMenuControl
    {
        public enum MenuCondition
        {
            Category = 1,
            Book = 2,
            PriorityBook = 3
        }

        public MenuCondition ControlsCondition { get; private set; }
        public BooksMenuControl()
        {
            InitializeComponent();
            MoreInfoFormVisualizer = new MoreInfoFormVisualizer(infoCanvas);
            UpdateFormVisualizer = new UpdateFormVisualizer(infoCanvas);
            MoreInfoFormVisualizer.UpdateVisualizer = UpdateFormVisualizer;
            UpdateFormVisualizer.MoreVisualizer = MoreInfoFormVisualizer;
            MainInfo.TableCollection.TableLoad += new EventHandler(this.TableCollection_TablesLoad);
        }

        private void TableCollection_TablesLoad(object sender, EventArgs e)
        {
            LoadGenres();
            LoadCategories();
        }

        public MoreInfoFormVisualizer MoreInfoFormVisualizer { get; private set; }
        public UpdateFormVisualizer UpdateFormVisualizer { get; private set; }

        private IBaseControls controlInBuffer = null;
        public IBaseControls ControlInBuffer
        {
            get { return controlInBuffer; }
            set
            {
                if (controlInBuffer != null) controlInBuffer.SetVisualDefault();
                controlInBuffer = value;
            }
        }

        public List<UserControl> TableControls { get; private set; } = new List<UserControl>();
        private void clearControls()
        {
            controlsPanel.Children.Clear();
            TableControls.Clear();
        }
        public void LoadGenres()
        {
            genres_panel.Children.Clear();
            foreach (BookGenre genre in MainInfo.Tables.BookGenresTable)
            {
                genres_panel.Children.Add(new BookGenrePressButtonControl(genre));
            }
        }

        public void LoadCategories()
        {
            clearControls();
            ControlInBuffer = null;
            ControlsCondition = MenuCondition.Category;

            foreach (BookCategory category in MainInfo.Tables.BookCategoriesTable)
            {
                TableControls.Add(new BookCategoryControl(category));
            }

            foreach (Book book in MainInfo.Tables.BooksTable)
            {
                if (book.FranshiseId == 0)
                    TableControls.Add(new BookSimpleControl(book));
            }

            foreach (var item in TableControls)
            {
                controlsPanel.Children.Add(item);
            }
        }

        public void LoadBooks()
        {
            clearControls();
            ControlInBuffer = null;
            ControlsCondition = MenuCondition.Book;

            foreach (Book book in MainInfo.Tables.BooksTable)
            {
                TableControls.Add(new BookControl(book));
            }

            foreach (var item in TableControls)
            {
                controlsPanel.Children.Add(item);
            }
        }

        public void LoadPriorityBooks()
        {
            clearControls();
            ControlInBuffer = null;
            ControlsCondition = MenuCondition.Book;

            PriorityBooksTable priorityTable = MainInfo.Tables.PriorityBooksTable;

            PriorityBook book;
            for (int i = 0; i < priorityTable.Count; i++)
            {
                book = priorityTable[i];

                if (book.Book.Readed)
                {
                    priorityTable.Remove(book);
                    --i;
                }
                else
                {
                    TableControls.Add(new BookSimpleControl(book.Book));
                }
            }

            foreach (UserControl control in TableControls)
            {
                controlsPanel.Children.Add(control);
            }
        }

        private void btn_addBook_Click(object sender, RoutedEventArgs e)
        {
            Book book = new Book();
            book.BookGenre = MainInfo.Tables.BookGenresTable[0];
            MainInfo.Tables.BooksTable.AddElement(book);
            IControls<Book, BookGenre> control;
            switch (ControlsCondition)
            {
                case MenuCondition.Category:
                    control = new BookSimpleControl(book);
                    TableControls.Add((UserControl)control);
                    controlsPanel.Children.Add((UserControl)control);
                    break;

                default:
                    break;
            }
        }

        private void btn_addCategory_Click(object sender, RoutedEventArgs e)
        {
            if (ControlsCondition == MenuCondition.Category)
            {
                MainInfo.Tables.BookCategoriesTable.AddElement();
                BookCategoryControl categoryControl = new BookCategoryControl(MainInfo.Tables.BookCategoriesTable.GetLastElement);
                controlsPanel.Children.Insert(MainInfo.Tables.BookCategoriesTable.Count - 1, categoryControl);
            }
        }

        private void btn_filter_Click(object sender, RoutedEventArgs e)
        {
            controlsPanel.Children.Clear();
            BookGenre[] genres = getSelectedGenres();

            foreach (IControls<Book, BookGenre> control in TableControls)
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
                    if (control.HasSelectedGenre(genres) && control.HasCheckedProperty(watchedRequestControl.IsWatched))
                    {
                        controlsPanel.Children.Add((UserControl)control);
                    }
                }
            }
        }

        private BookGenre[] getSelectedGenres()
        {
            List<BookGenre> genres = new List<BookGenre>();
            foreach (BookGenrePressButtonControl requestControl in genres_panel.Children)
            {
                if (requestControl.PressButton.Included)
                {
                    genres.Add(requestControl.Genre);
                }
            }
            return genres.ToArray();
        }

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            foreach (IControls<Book, BookGenre> control in controlsPanel.Children)
            {
                control.SetVisualDefault();
            }

            if (textbox_search.Text != "")
            {
                String searchStr = textbox_search.Text.ToLowerInvariant();

                foreach (IControls<Book, BookGenre> control in controlsPanel.Children)
                {
                    control.SetFindedElement(searchStr);
                }
            }
        }

        private void btn_showBooks_Click(object sender, RoutedEventArgs e)
        {
            LoadBooks();
        }

        private void btn_showCategories_Click(object sender, RoutedEventArgs e)
        {
            LoadCategories();
        }

        private void btn_AddToPriority_Click(object sender, RoutedEventArgs e)
        {
            if (ControlInBuffer != null)
            {
                MainInfo.Tables.PriorityBooksTable.AddElement();
                PriorityBook priorityBook = MainInfo.Tables.PriorityBooksTable.GetLastElement;
                priorityBook.Book = ((IControls<Book, BookGenre>)ControlInBuffer).Info;
            }
        }

        private void btn_showPriority_Click(object sender, RoutedEventArgs e)
        {
            LoadPriorityBooks();
        }

        private void btn_RemoveBook_Click(object sender, RoutedEventArgs e)
        {
            MainInfo.Tables.BooksTable.Remove(((IControls<Book,BookGenre>)ControlInBuffer).Info);
        }
    }
}
