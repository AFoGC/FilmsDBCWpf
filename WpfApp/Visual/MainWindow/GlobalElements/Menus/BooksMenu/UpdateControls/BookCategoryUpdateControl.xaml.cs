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
using WpfApp.Visual.MainWindow.GlobalElements.Menus.BooksMenu.BooksControls;

namespace WpfApp.Visual.MainWindow.GlobalElements.Menus.BooksMenu.UpdateControls
{
    /// <summary>
    /// Логика взаимодействия для BookCategoryUpdateControl.xaml
    /// </summary>
    public partial class BookCategoryUpdateControl : UserControl
    {
        private BookCategoryControl categoryControl = null;
        private BookCategory category = null;

        public BookCategoryUpdateControl(BookCategoryControl categoryControl)
        {
            InitializeComponent();
            this.categoryControl = categoryControl;
            this.category = categoryControl.CategoryInfo;
            refresh();
        }

        public void UpdateElement()
        {
            category.Name = this.name.Text;
            categoryControl.RefreshData();
        }

        private void refresh()
        {
            this.id.Text = category.ID.ToString();
            this.name.Text = category.Name;
        }

        private void btn_AddSelected_Click(object sender, RoutedEventArgs e)
        {
            if (MainInfo.MainWindow.FilmsMenu.ControlInBuffer != null)
            {
                if (MainInfo.MainWindow.FilmsMenu.ControlInBuffer.GetType() == typeof(BookSimpleControl))
                {
                    BookSimpleControl simpleControl = (BookSimpleControl)MainInfo.MainWindow.BooksMenu.ControlInBuffer;
                    Book book = simpleControl.BookInfo;
                    if (book.FranshiseId == 0)
                    {
                        book.FranshiseId = category.ID;
                        category.Books.Add(book);
                        categoryControl.AddSimpleCotrol(book);
                        //MainInfo.MainWindow.BooksMenu.controlsPanel.Children.Remove(simpleControl);
                        MainInfo.MainWindow.BooksMenu.ControlInBuffer = null;
                    }
                }
            }
        }

        private void btn_RemoveSec_Click(object sender, RoutedEventArgs e)
        {
            if (MainInfo.MainWindow.FilmsMenu.ControlInBuffer != null)
            {
                if (MainInfo.MainWindow.FilmsMenu.ControlInBuffer.GetType() == typeof(BookSimpleControl))
                {
                    BookSimpleControl simpleControl = (BookSimpleControl)MainInfo.MainWindow.BooksMenu.ControlInBuffer;
                    Book book = simpleControl.BookInfo;

                    if (categoryControl.RemoveFilmFromCategory(simpleControl))
                    {

                        //if (MainInfo.MainWindow.BooksMenu.ControlsCondition == BooksMenuControl.MenuCondition.Category)
                        {
                            int i = 0;
                            foreach (UserControl userControl in MainInfo.MainWindow.FilmsMenu.controlsPanel.Children)
                            {
                                if (userControl.GetType() == typeof(BookSimpleControl))
                                {
                                    BookSimpleControl sControl = (BookSimpleControl)userControl;
                                    if (sControl.BookInfo.ID > book.ID)
                                    {
                                        MainInfo.MainWindow.FilmsMenu.controlsPanel.Children.Insert(i, simpleControl);
                                        break;
                                    }
                                }
                                ++i;
                            }
                        }
                    }
                }
            }
        }
    }
}
