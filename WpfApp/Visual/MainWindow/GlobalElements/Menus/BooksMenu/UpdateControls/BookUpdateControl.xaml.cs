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
using WpfApp.StaticFilmClasses;
using WpfApp.Visual.MainWindow.GlobalElements.Menus.ACommonElements.ControlsInterface;
using WpfApp.Visual.MainWindow.GlobalElements.Menus.BooksMenu.BooksControls;
using WpfApp.Visual.StaticVisualClasses;

namespace WpfApp.Visual.MainWindow.GlobalElements.Menus.BooksMenu.UpdateControls
{
    /// <summary>
    /// Логика взаимодействия для BookUpdateControl.xaml
    /// </summary>
    public partial class BookUpdateControl : UserControl, IUpdateControl
    {
        private BookControl bookControl = null;
        private Book book = null;

        public BookUpdateControl(BookControl bookControl)
        {
            InitializeComponent();
            this.bookControl = bookControl;
            this.book = bookControl.BookInfo;

            foreach (var item in MainInfo.Tables.BookGenresTable)
            {
                genre.Items.Add(item);
            }
            foreach (var item in FilmMethods.GetAllMarks().ToArray())
            {
                mark.Items.Add(item);
            }

            refresh();
        }

        private void refresh()
        {
            Book defBook = MainInfo.Tables.BooksTable.DefaultCell;

            id.Text = book.ID.ToString();
            name.Text = book.Name;
            genre.SelectedItem = book.BookGenre;
            realiseYear.Text = Book.FormatToString(book.PublicationYear, defBook.PublicationYear);
            readed.IsChecked = book.Readed;
            author.Text = book.Author;
            fullReadDate.Date = book.FullReadDate;
            mark.Text = VisualHelper.markToText(Book.FormatToString(book.Mark, defBook.Mark));
            countOfReadings.Text = Book.FormatToString(book.CountOfReadings, defBook.CountOfReadings);
            bookmark.Text = book.Bookmark;
        }

        public void UpdateElement()
        {
            book.Name = name.Text;
            book.BookGenre = (BookGenre)genre.SelectedItem;
            book.PublicationYear = VisualHelper.TextToInt32(realiseYear.Text);
            book.Readed = (bool)readed.IsChecked;
            book.Author = author.Text;
            book.FullReadDate = fullReadDate.Date;
            book.Mark = VisualHelper.TextToMark(mark.Text);
            book.CountOfReadings = VisualHelper.TextToInt32(countOfReadings.Text);
            book.Bookmark = bookmark.Text;

            bookControl.RefreshData();
        }

        private void watched_Click(object sender, RoutedEventArgs e)
        {
            if (fullReadDate.IsEmpty)
            {
                fullReadDate.Date = DateTime.Today;
            }

            if (countOfReadings.Text == "")
            {
                countOfReadings.Text = "1";
            }
        }

        private void btn_comment_Click(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void btn_sources_Click(object sender, RoutedEventArgs e)
        {
            MainInfo.MainWindow.BooksMenu.UpdateVisualizer.SourcesVisualizer.OpenSourceControl(this.book.Sources);
        }
    }
}
