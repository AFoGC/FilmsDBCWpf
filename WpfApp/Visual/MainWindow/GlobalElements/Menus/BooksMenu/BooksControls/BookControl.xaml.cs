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
using WpfApp.Visual.MainWindow.GlobalElements.Menus.BooksMenu.UpdateControls;
using WpfApp.Visual.StaticVisualClasses;

namespace WpfApp.Visual.MainWindow.GlobalElements.Menus.BooksMenu.BooksControls
{
    /// <summary>
    /// Логика взаимодействия для BookControl.xaml
    /// </summary>
    public partial class BookControl : ABookElementControl
    {
        BookSimpleControl simpleControl = null;
        public BookControl(Book book)
        {
            InitializeComponent();
            this.bookInfo = book;
            RefreshData();
        }

        public BookControl(BookSimpleControl simpleControl)
        {
            InitializeComponent();
            this.bookInfo = simpleControl.bookInfo;
            this.simpleControl = simpleControl;
            RefreshData();
        }


        public override void RefreshData()
        {
            Book defBook = MainInfo.Tables.BooksTable.DefaultCell;

            this.id.Text = bookInfo.ID.ToString();
            this.name.Text = bookInfo.Name;
            this.genre.Text = Book.FormatToString(bookInfo.BookGenre, defBook.BookGenre);
            this.realiseYear.Text = Book.FormatToString(bookInfo.PublicationYear, defBook.PublicationYear);
            this.readed.IsChecked = bookInfo.Readed;
            this.fullReadDate.Text = Book.FormatToString(bookInfo.FullReadDate, defBook.FullReadDate);
            this.mark.Text = VisualHelper.markToText(Book.FormatToString(bookInfo.Mark, defBook.Mark));
            this.countOfReadings.Text = Book.FormatToString(bookInfo.CountOfReadings, defBook.CountOfReadings);
            this.bookmark.Text = bookInfo.Bookmark;
            this.RefreshSourceLabel();

            if (simpleControl != null)
            {
                simpleControl.RefreshData();
            }
        }

        public void RefreshSourceLabel()
        {
            if (bookInfo.Sources.Count == 0)
            {
                btn_copyUrl.Content = "no url";
            }
            else
            {
                if (bookInfo.Sources[0].Name == "")
                {
                    btn_copyUrl.Content = "copy url";
                }
                else
                {
                    btn_copyUrl.Content = "url: " + bookInfo.Sources[0].Name;
                }
            }
        }

        public override Control ToUpdateControl()
        {
            return new BookUpdateControl(this);
        }

        internal override void setVisualSelected()
        {
            SolidColorBrush myBrush = new SolidColorBrush(Color.FromRgb(0, 220, 0));
            this.id.Background = myBrush;
        }

        internal override void setVisualFinded()
        {
            SolidColorBrush myBrush = new SolidColorBrush(Color.FromRgb(0, 0, 220));
            this.id.Background = myBrush;
        }

        public override void SetVisualDefault()
        {
            SolidColorBrush myBrush = new SolidColorBrush(Color.FromRgb(53, 53, 53));
            this.id.Background = myBrush;
        }

        private void btn_copyUrl_Click(object sender, RoutedEventArgs e)
        {
            if (bookInfo.Sources.Count != 0)
            {
                Clipboard.SetText(bookInfo.Sources[0].SourceUrl);
            }
        }

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            MainInfo.MainWindow.BooksMenu.UpdateFormVisualizer.OpenUpdateControl(this);
        }

        private void watched_Click(object sender, RoutedEventArgs e)
        {
            readed.IsChecked = !readed.IsChecked;
        }

        private void id_GotFocus(object sender, RoutedEventArgs e)
        {
            SetSelectedElement(MainInfo.MainWindow.BooksMenu.ControlInBuffer);
        }
    }
}
