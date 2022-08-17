using FilmsUCWpf.ModelBinder;
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
    public partial class BookCategoryUpdateControl : UserControl, IUpdateControl
    {
        private BookCategoryUpdatePresenter presenter;
        public BookCategoryUpdateControl(BookCategory model, IMenuModel<Book> menu, TableCollection tableCollection)
        {
            InitializeComponent();
            presenter = new BookCategoryUpdatePresenter(model, menu, tableCollection);
            DataContext = new BookCategoryBinder(model);
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
            
        }

        private void btn_DeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            presenter.DeleteThisCategory();
        }
    }
}
