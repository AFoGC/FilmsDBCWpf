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
using WpfApp.Visual.StaticVisualClasses;

namespace WpfApp.Visual.MainWindow.GlobalElements.Menus.BooksMenu.BooksControls
{
    /// <summary>
    /// Логика взаимодействия для BookCategoryControl.xaml
    /// </summary>
    public partial class BookCategoryControl : UserControl, IBooksControls
    {
        public BookCategory CategoryInfo { get; private set; }

        public BookCategoryControl(BookCategory category)
        {
            InitializeComponent();
            this.CategoryInfo = category;

            RefreshData();
        }

        public void RefreshData()
        {
            id.Text = CategoryInfo.ID.ToString();
            name.Text = CategoryInfo.Name;
            mark.Text = VisualHelper.markToText(Category.FormatToString(CategoryInfo.Mark, -1));

            categoryFilms();
        }

        private void categoryFilms()
        {
            this.cat_panel.Children.Clear();
            this.Height = 20;

            foreach (Book book in CategoryInfo.Books)
            {
                this.Height += 15;
                cat_panel.Children.Add(new BookSimpleControl(book));
            }

            BookSimpleControl simpleControl = null;
            for (int i = 0; i < cat_panel.Children.Count; i++)
            {
                simpleControl = (BookSimpleControl)cat_panel.Children[i];
                if (simpleControl.BookInfo.FranshiseListIndex != i)
                {
                    cat_panel.Children.Remove(simpleControl);
                    cat_panel.Children.Insert(simpleControl.BookInfo.FranshiseListIndex, simpleControl);
                    i = 0;
                }
            }

        }

        public void AddSimpleCotrol(Book book)
        {
            this.Height += 15;
            this.cat_panel.Children.Add(new BookSimpleControl(book));
            book.FranshiseListIndex = Convert.ToSByte(cat_panel.Children.Count - 1);
        }

        public bool RemoveBookFromCategory(BookSimpleControl simpleControl)
        {
            if (simpleControl.BookInfo.FranshiseId == this.CategoryInfo.ID)
            {
                cat_panel.Children.Remove(simpleControl);

                this.Height -= 15;

                simpleControl.BookInfo.FranshiseId = 0;
                simpleControl.BookInfo.FranshiseListIndex = 0;

                foreach (Book book in CategoryInfo.Books)
                {
                    if (simpleControl.BookInfo.FranshiseListIndex < book.FranshiseListIndex)
                    {
                        --book.FranshiseListIndex;
                    }
                }

                return CategoryInfo.Books.Remove(simpleControl.BookInfo);
            }
            else
            {
                return false;
            }
        }

        public bool HasSelectedGenre(BookGenre[] selectedGenres)
        {
            foreach (BookSimpleControl control in cat_panel.Children)
            {
                if (control.HasSelectedGenre(selectedGenres))
                {
                    return true;
                }
            }

            return false;
        }

        public bool HasReadedProperty(bool isReaded)
        {
            foreach (BookSimpleControl control in cat_panel.Children)
            {
                if (control.HasReadedProperty(isReaded))
                {
                    return true;
                }
            }

            return false;
        }

        public bool SetFindedElement(string search)
        {
            bool export = false;
            if (this.CategoryInfo.Name.ToLowerInvariant().Contains(search))
            {
                SolidColorBrush myBrush = new SolidColorBrush(Color.FromRgb(0, 0, 220));
                this.id.Background = myBrush;
            }

            foreach (IControls control in cat_panel.Children)
            {
                control.SetFindedElement(search);
            }

            return export;
        }

        public void SetVisualDefault()
        {
            SolidColorBrush myBrush = new SolidColorBrush(Color.FromRgb(53, 53, 53));
            this.id.Background = myBrush;

            foreach (IControls control in cat_panel.Children)
            {
                control.SetVisualDefault();
            }
        }

        public Control ToUpdateControl()
        {
            return new BookCategoryUpdateControl(this);
        }

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            MainInfo.MainWindow.BooksMenu.UpdateFormVisualizer.OpenUpdateControl(this);
        }
    }
}
