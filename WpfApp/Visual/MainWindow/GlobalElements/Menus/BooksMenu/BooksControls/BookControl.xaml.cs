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
        public BookControl(Book book)
        {
            InitializeComponent();
            this.Info = book;
            book.PropertyChanged += Book_PropertyChanged;
            RefreshData();
        }

        public BookControl(BookSimpleControl simpleControl) : this(simpleControl.Info) { }

        private void Book_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            RefreshData();
        }

        public override void RefreshData()
        {
            Book defBook = MainInfo.Tables.BooksTable.DefaultCell;

            this.id.Text = Info.ID.ToString();
            this.name.Text = Info.Name;
            this.genre.Text = Book.FormatToString(Info.BookGenre, defBook.BookGenre);
            this.realiseYear.Text = Book.FormatToString(Info.PublicationYear, defBook.PublicationYear);
            this.readed.IsChecked = Info.Readed;
            this.author.Text = Info.Author;
            this.fullReadDate.Text = Book.FormatToString(Info.FullReadDate, defBook.FullReadDate);
            this.mark.Text = VisualHelper.markToText(Book.FormatToString(Info.Mark, defBook.Mark));
            this.countOfReadings.Text = Book.FormatToString(Info.CountOfReadings, defBook.CountOfReadings);
            this.bookmark.Text = Info.Bookmark;
            this.RefreshSourceLabel();
        }

        public void RefreshSourceLabel()
        {
            if (Info.Sources.Count == 0)
            {
                btn_copyUrl.Content = "no url";
            }
            else
            {
                if (Info.Sources[0].Name == "")
                {
                    btn_copyUrl.Content = "copy url";
                }
                else
                {
                    btn_copyUrl.Content = "url: " + Info.Sources[0].Name;
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
            if (Info.Sources.Count != 0)
            {
                Clipboard.SetText(Info.Sources[0].SourceUrl);
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
            SetSelectedElement();
        }
    }
}
