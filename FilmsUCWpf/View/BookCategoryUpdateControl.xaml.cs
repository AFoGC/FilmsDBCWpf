using FilmsUCWpf.Presenter;
using FilmsUCWpf.PresenterInterfaces;
using FilmsUCWpf.ViewInterfaces;
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

namespace FilmsUCWpf.View
{
    /// <summary>
    /// Логика взаимодействия для BookCategoryUpdateControl.xaml
    /// </summary>
    public partial class BookCategoryUpdateControl : UserControl, IBookCategoryUpdateView
    {
        private BookCategoryUpdatePresenter presenter;
        public BookCategoryUpdateControl(BookCategory model, IMenu<Book> menu)
        {
            InitializeComponent();
            presenter = new BookCategoryUpdatePresenter(model, this, menu);
        }

        private void btn_AddSelected_Click(object sender, RoutedEventArgs e)
        {
            presenter.AddSelected();
        }

        private void btn_RemoveSec_Click(object sender, RoutedEventArgs e)
        {
            presenter.RemoveSelected();
        }

        public string ID { set => id.Text = value; }
        string IBookCategoryUpdateView.Name { get => name.Text; set => name.Text = value; }
    }
}
