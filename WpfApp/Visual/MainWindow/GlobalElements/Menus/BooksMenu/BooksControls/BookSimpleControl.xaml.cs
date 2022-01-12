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
using WpfApp.Visual.MainWindow.GlobalElements.Menus.BooksMenu.UpdateControls;

namespace WpfApp.Visual.MainWindow.GlobalElements.Menus.BooksMenu.BooksControls
{
    /// <summary>
    /// Логика взаимодействия для BookSimpleControl.xaml
    /// </summary>
    public partial class BookSimpleControl : ABookElementControl, ISimpleControl
    {
        public BookSimpleControl(Book book)
        {
            InitializeComponent();
            this.bookInfo = book;
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
        }

        public override void SetVisualDefault()
        {
            SolidColorBrush myBrush = new SolidColorBrush(Color.FromRgb(53, 53, 53));

            this.id.Background = myBrush;
        }

        internal override void setVisualFinded()
        {
            SolidColorBrush myBrush = new SolidColorBrush(Color.FromRgb(0, 0, 220));

            this.id.Background = myBrush;
        }

        internal override void setVisualSelected()
        {
            SolidColorBrush myBrush = new SolidColorBrush(Color.FromRgb(0, 220, 0));

            this.id.Background = myBrush;
        }

        public override Control ToUpdateControl()
        {
            return new BookUpdateControl(new BookControl(this));
        }

        public Control ToMoreInfo()
        {
            return new BookControl(this);
        }

        private void watched_Click(object sender, RoutedEventArgs e)
        {
            readed.IsChecked = !readed.IsChecked;
        }

        private void btn_moreInfo_Click(object sender, RoutedEventArgs e)
        {
            MainInfo.MainWindow.BooksMenu.MoreInfoVisualizer.OpenMoreInfoForm(this);
        }

        private void id_GotFocus(object sender, RoutedEventArgs e)
        {
            SetSelectedElement(MainInfo.MainWindow.BooksMenu.ControlInBuffer);
        }
    }
}
