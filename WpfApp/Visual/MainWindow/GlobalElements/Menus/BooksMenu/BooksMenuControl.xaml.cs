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
using WpfApp.Visual.MainWindow.GlobalElements.Menus.ACommonElements.ControlsInterface;
using WpfApp.Visual.MainWindow.GlobalElements.Menus.ACommonElements.InfoMenus.MoreInfo;
using WpfApp.Visual.MainWindow.GlobalElements.Menus.ACommonElements.InfoMenus.UpdateInfo;
using WpfApp.Visual.MainWindow.GlobalElements.Menus.BooksMenu.BooksControls;

namespace WpfApp.Visual.MainWindow.GlobalElements.Menus.BooksMenu
{
    /// <summary>
    /// Логика взаимодействия для BooksMenuControl.xaml
    /// </summary>
    public partial class BooksMenuControl : UserControl
    {
        public enum MenuCondition
        {
            Category = 1,
            Book = 2,
            PriorityBook = 3
        }
        public BooksMenuControl()
        {
            InitializeComponent();
            moreInfoVisualizer = new MoreInfoFormVisualizer(infoCanvas);
            updateVisualizer = new UpdateFormVisualizer(infoCanvas);
            MainInfo.TableCollection.TableLoad += new EventHandler(this.TableCollection_TablesLoad);
        }

        private void TableCollection_TablesLoad(object sender, EventArgs e)
        {
            LoadGenres();
            LoadCategories();
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

        private ABookElementControl controlInBuffer = null;
        public ABookElementControl ControlInBuffer
        {
            get { return controlInBuffer; }
            set { controlInBuffer = value; }
        }

        private List<UserControl> tableControls = new List<UserControl>();
        private void clearControls()
        {
            controlsPanel.Children.Clear();
            tableControls.Clear();
        }
        public void LoadGenres()
        {
            genres_panel.Children.Clear();
            foreach (BookGenre genre in MainInfo.Tables.BookGenresTable)
            {
                //genres_panel.Children.Add(new GenrePressButtonControl(genre));
            }
        }

        public void LoadCategories()
        {
            clearControls();
            controlInBuffer = null;

            foreach (BookCategory category in MainInfo.Tables.BookCategoriesTable)
            {
                tableControls.Add(new BookCategoryControl(category));
            }

            foreach (Book book in MainInfo.Tables.BooksTable)
            {
                if (book.FranshiseId == 0)
                    tableControls.Add(new BookSimpleControl(book));
            }

            //controlsPanel.Children.AddRange(tableControls.ToArray());
            foreach (var item in tableControls)
            {
                controlsPanel.Children.Add(item);
            }
        }

        private void btn_addBook_Click(object sender, RoutedEventArgs e)
        {
            MainInfo.Tables.BooksTable.AddElement();
            Book book = MainInfo.Tables.BooksTable.GetLastElement;
            IControls control = new BookSimpleControl(book);

            tableControls.Add((UserControl)control);
            controlsPanel.Children.Add((UserControl)control);
        }

        private void btn_addCategory_Click(object sender, RoutedEventArgs e)
        {
            //if (controlsCondition == MenuCondition.Category)
            {
                MainInfo.Tables.BookCategoriesTable.AddElement();
                BookCategoryControl categoryControl = new BookCategoryControl(MainInfo.Tables.BookCategoriesTable.GetLastElement);
                controlsPanel.Children.Insert(MainInfo.Tables.BookCategoriesTable.Count - 1, categoryControl);
            }
        }

        private void btn_filter_Click(object sender, RoutedEventArgs e)
        {
            /*
            controlsPanel.Children.Clear();
            BookGenre[] genres = getSelectedGenres();

            foreach (IBooksControls control in tableControls)
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
            */
        }

        private BookGenre[] getSelectedGenres()
        {
            throw new NotImplementedException();
            /*
            List<BookGenre> genres = new List<BookGenre>();
            foreach (BookGenrePressButton requestControl in flowLayoutPanel_requestsGenres.Controls)
            {
                if (requestControl.Included)
                {
                    genres.Add(requestControl.Genre);
                }
            }
            return genres.ToArray();
            */
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
