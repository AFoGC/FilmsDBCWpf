using FilmsUCWpf.Presenter;
using FilmsUCWpf.Presenter.Interfaces;
using FilmsUCWpf.View.Interfaces;
using System;
using System.Collections;
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
using TablesLibrary.Interpreter;
using TL_Objects;

namespace FilmsUCWpf.View
{
    /// <summary>
    /// Логика взаимодействия для BookCategoryUpdateControl.xaml
    /// </summary>
    public partial class BookCategoryUpdateControl : UserControl, IBookCategoryUpdateView, IUpdateControl
    {
        private BookCategoryUpdatePresenter presenter;
        public BookCategoryUpdateControl(BookCategory model, IMenuModel<Book> menu, TableCollection tableCollection)
        {
            InitializeComponent();
            presenter = new BookCategoryUpdatePresenter(model, this, menu, tableCollection);
        }

        private void btn_AddSelected_Click(object sender, RoutedEventArgs e)
        {
            presenter.AddSelected();
        }

        private void btn_RemoveSec_Click(object sender, RoutedEventArgs e)
        {
            presenter.RemoveSelected();
        }

        public void UpdateElement()
        {
            presenter.UpdateElement();
        }

        public string ID { set => id.Text = value; }
        string IBookCategoryUpdateView.Name { get => name.Text; set => name.Text = value; }
        IList IBookCategoryUpdateView.Marks { get => mark.Items; }
        string IBookCategoryUpdateView.Mark { get => mark.Text; set => mark.Text = value; }
        string IBookCategoryUpdateView.HideName { get => name.Text; set => name.Text = value; }

        private void btn_DeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            presenter.DeleteThisCategory();
        }
    }
}
