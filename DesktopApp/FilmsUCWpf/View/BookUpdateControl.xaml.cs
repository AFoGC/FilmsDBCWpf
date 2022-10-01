using FilmsUCWpf.ModelBinder;
using FilmsUCWpf.Presenter;
using FilmsUCWpf.Presenter.Interfaces;
using FilmsUCWpf.View.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using TablesLibrary.Interpreter;
using TL_Objects;

namespace FilmsUCWpf.View
{
    /// <summary>
    /// Логика взаимодействия для BookUpdateControl.xaml
    /// </summary>
    public partial class BookUpdateControl : UserControl
    {
        private BookUpdatePresenter presenter;
        private readonly Book book;
        public BookUpdateControl(Book book, IMenuPresenter<Book> menu, TableCollection table)
        {
            InitializeComponent();
            this.book = book;
            presenter = new BookUpdatePresenter(book, menu);
            DataContext = new BookBinder(book);
        }

        private void btn_comment_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_sources_Click(object sender, RoutedEventArgs e)
        {
            presenter.OpenSources();
        }

        private void watched_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (book.Readed == false)
            {
                if (fullReadDate.IsEmpty)
                {
                    fullReadDate.Date = DateTime.Today;
                }

                if (countOfReadings.Text == String.Empty)
                {
                    countOfReadings.Text = "1";
                }
            }
            readed.IsChecked = !readed.IsChecked;
        }

        private void readed_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (book.Readed == false)
            {
                if (fullReadDate.IsEmpty)
                {
                    book.FullReadDate = DateTime.Today;
                }

                if (countOfReadings.Text == String.Empty)
                {
                    book.CountOfReadings = 1;
                }
            }
        }

        private void TextInputIsNumber(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"\D");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
