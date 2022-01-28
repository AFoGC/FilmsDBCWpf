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
using WpfApp.Visual.MainWindow.GlobalElements.Menus.BooksMenu.BooksControls;

namespace WpfApp.Visual.MainWindow.GlobalElements.Menus.BooksMenu.UpdateControls
{
    /// <summary>
    /// Логика взаимодействия для BookCategoryUpdateControl.xaml
    /// </summary>
    public partial class BookCategoryUpdateControl : UserControl, IUpdateControl
    {
        private BookCategoryControl categoryControl = null;
        private BookCategory category = null;

        public BookCategoryUpdateControl(BookCategoryControl categoryControl)
        {
            InitializeComponent();
            this.category = categoryControl.Info;
            this.categoryControl = categoryControl;
            refresh();
        }

        public void UpdateElement()
        {
            category.Name = this.name.Text;
        }

        private void refresh()
        {
            this.id.Text = category.ID.ToString();
            this.name.Text = category.Name;
        }

        private void btn_AddSelected_Click(object sender, RoutedEventArgs e)
        {
            if (MainInfo.MainWindow.BooksMenu.ControlInBuffer != null)
            {
                if (MainInfo.MainWindow.BooksMenu.ControlInBuffer.GetType() == typeof(BookSimpleControl))
                {
                    BookSimpleControl simpleControl = (BookSimpleControl)MainInfo.MainWindow.BooksMenu.ControlInBuffer;
                    Book book = simpleControl.Info;
                    if (book.FranshiseId == 0)
                    {
                        book.FranshiseId = category.ID;
                        category.Books.Add(book);
                        categoryControl.AddSimpleCotrol(book);
                        MainInfo.MainWindow.BooksMenu.controlsPanel.Children.Remove(simpleControl);
                        MainInfo.MainWindow.BooksMenu.TableControls.Remove(simpleControl);
                        MainInfo.MainWindow.BooksMenu.ControlInBuffer = null;
                    }
                }
            }
        }

        private void btn_RemoveSec_Click(object sender, RoutedEventArgs e)
        {
            if (MainInfo.MainWindow.BooksMenu.ControlInBuffer != null)
            {
                if (MainInfo.MainWindow.BooksMenu.ControlInBuffer.GetType() == typeof(BookSimpleControl))
                {
                    BookSimpleControl simpleControl = (BookSimpleControl)MainInfo.MainWindow.BooksMenu.ControlInBuffer;
                    Book book = simpleControl.Info;

                    if (categoryControl.RemoveBookFromCategory(simpleControl))
                    {

                        if (MainInfo.MainWindow.BooksMenu.ControlsCondition == BooksMenuControl.MenuCondition.Category)
                        {
                            int i = 0;
                            foreach (UserControl userControl in MainInfo.MainWindow.BooksMenu.controlsPanel.Children)
                            {
                                if (userControl.GetType() == typeof(BookSimpleControl))
                                {
                                    BookSimpleControl sControl = (BookSimpleControl)userControl;
                                    if (sControl.Info.ID > book.ID)
                                    {
                                        MainInfo.MainWindow.BooksMenu.controlsPanel.Children.Insert(i, simpleControl);
                                        MainInfo.MainWindow.BooksMenu.TableControls.Insert(i, simpleControl);
                                        return;
                                    }
                                }
                                ++i;
                            }
                            MainInfo.MainWindow.BooksMenu.controlsPanel.Children.Add(simpleControl);
                        }
                    }
                }
            }
        }
    }
}
