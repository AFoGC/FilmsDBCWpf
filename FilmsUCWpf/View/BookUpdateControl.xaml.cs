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
    public partial class BookUpdateControl : UserControl, IBookUpdateView, IUpdateControl
    {
        private BookUpdatePresenter presenter;
        public BookUpdateControl(Book book, IMenuPresenter<Book> menu, TableCollection table)
        {
            InitializeComponent();
            presenter = new BookUpdatePresenter(book, this, menu, table);
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
            if (readed.IsChecked == false)
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
            readed.IsChecked = !readed.IsChecked;
        }

        private void readed_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (readed.IsChecked == false)
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
        }

        private void TextInputIsNumber(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"\D");
            e.Handled = regex.IsMatch(e.Text);
        }

        public void UpdateElement()
        {
            try
            {
                presenter.UpdateElement();
            }
            catch
            {
                MessageBox.Show("Invalid data type entered", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public string ID { set => id.Text = value; }
        string IBookUpdateView.Name { get => name.Text; set => name.Text = value; }
        public IList Genres { get => genre.Items; }
        public BookGenre Genre { get => (BookGenre)genre.SelectedItem; set => genre.SelectedItem = value; }
        public string RealiseYear { get => realiseYear.Text; set => realiseYear.Text = value; }
        public bool Readed { get => (bool)readed.IsChecked; set => readed.IsChecked = value; }
        public string Author { get => author.Text; set => author.Text = value; }
        public DateTime FullReadDate { get => fullReadDate.Date; set => fullReadDate.Date = value; }
        public IList Marks => mark.Items;
        public string Mark { get => mark.Text; set => mark.Text = value; }
        public string CountOfReadings { get => countOfReadings.Text; set => countOfReadings.Text = value; }
        public string Bookmark { get => bookmark.Text; set => bookmark.Text = value; }
    }
}
