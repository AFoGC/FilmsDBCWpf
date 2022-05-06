using FilmsUCWpf.Presenter;
using FilmsUCWpf.PresenterInterfaces;
using FilmsUCWpf.ViewInterfaces;
using InfoMenusWpf;
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
    /// Логика взаимодействия для FilmCategoryUpdateControl.xaml
    /// </summary>
    public partial class FilmCategoryUpdateControl : UserControl, IFilmCategoryUpdateView, IUpdateControl
    {
        private FilmCategoryUpdatePresenter presenter;
        public FilmCategoryUpdateControl(Category category, IMenu<Film> menu, TableCollection tableCollection)
        {
            InitializeComponent();
            presenter = new FilmCategoryUpdatePresenter(category, this, menu, tableCollection);
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
        string IFilmCategoryUpdateView.Name { set => name.Text = value; get => name.Text; }
        IList IFilmCategoryUpdateView.Marks { get => mark.Items; }
        string IFilmCategoryUpdateView.Mark { get => mark.Text; set => mark.Text = value; }
        string IFilmCategoryUpdateView.HideName { set => hideName.Text = value; get => hideName.Text; }

        private void btn_DeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            presenter.DeleteThisCategory();
        }
    }
}
