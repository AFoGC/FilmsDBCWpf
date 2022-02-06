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
    public partial class BookCategoryControl : UserControl, IControls<BookCategory, BookGenre>
    {
        public BookCategory Info { get; private set; }

        public BookCategoryControl(BookCategory category)
        {
            InitializeComponent();
            this.Info = category;
            this.Info.PropertyChanged += Info_PropertyChanged;
            this.Info.CellRemoved += Info_CellRemoved;

            foreach (Book book in category.Books)
            {
                book.CellRemoved += Book_CellRemoved; ;
            }

            RefreshData();
        }

        private void Book_CellRemoved(object sender, EventArgs e)
        {
            Book book = (Book)sender;
            //Info.Books.Remove(book);
            //RemoveBookFromCategory(book);
        }

        private void Info_CellRemoved(object sender, EventArgs e)
        {
            foreach (BookSimpleControl control in cat_panel.Children)
            {
                RemoveBookFromCategory(control);
            }
            Panel panel = (Panel)this.Parent;
            panel.Children.Remove(this);
        }

        private void Info_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            RefreshData();
        }

        public void RefreshData()
        {
            id.Text = Info.ID.ToString();
            name.Text = Info.Name;
            mark.Text = VisualHelper.markToText(Category.FormatToString(Info.Mark, -1));

            categoryFilms();
        }

        private void categoryFilms()
        {
            this.cat_panel.Children.Clear();
            this.Height = 20;

            foreach (Book book in Info.Books)
            {
                this.Height += 15;
                cat_panel.Children.Add(new BookSimpleControl(book));
            }

            BookSimpleControl simpleControl = null;
            for (int i = 0; i < cat_panel.Children.Count; i++)
            {
                simpleControl = (BookSimpleControl)cat_panel.Children[i];
                if (simpleControl.Info.FranshiseListIndex != i)
                {
                    cat_panel.Children.Remove(simpleControl);
                    cat_panel.Children.Insert(simpleControl.Info.FranshiseListIndex, simpleControl);
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

        public bool RemoveBookFromCategory(Book import)
        {
            if (import.FranshiseId == this.Info.ID)
            {

                this.Height -= 15;

                import.FranshiseId = 0;
                import.FranshiseListIndex = 0;

                foreach (Book book in Info.Books)
                {
                    if (import.FranshiseListIndex < book.FranshiseListIndex)
                    {
                        --book.FranshiseListIndex;
                    }
                }

                return Info.Books.Remove(import);
            }
            else
            {
                return false;
            }
        }

        public bool RemoveBookFromCategory(BookSimpleControl simpleControl)
        {
            if (simpleControl.Info.FranshiseId == this.Info.ID)
            {
                cat_panel.Children.Remove(simpleControl);

                this.Height -= 15;

                simpleControl.Info.FranshiseId = 0;
                simpleControl.Info.FranshiseListIndex = 0;

                foreach (Book book in Info.Books)
                {
                    if (simpleControl.Info.FranshiseListIndex < book.FranshiseListIndex)
                    {
                        --book.FranshiseListIndex;
                    }
                }

                return Info.Books.Remove(simpleControl.Info);
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

        public bool HasCheckedProperty(bool isReaded)
        {
            foreach (BookSimpleControl control in cat_panel.Children)
            {
                if (control.HasCheckedProperty(isReaded))
                {
                    return true;
                }
            }

            return false;
        }

        public bool SetFindedElement(string search)
        {
            bool export = false;
            if (this.Info.Name.ToLowerInvariant().Contains(search))
            {
                SolidColorBrush myBrush = new SolidColorBrush(Color.FromRgb(0, 0, 220));
                this.id.Background = myBrush;
            }

            foreach (IControls<Book,BookGenre> control in cat_panel.Children)
            {
                control.SetFindedElement(search);
            }

            return export;
        }

        public void SetVisualDefault()
        {
            SolidColorBrush myBrush = new SolidColorBrush(Color.FromRgb(53, 53, 53));
            this.id.Background = myBrush;

            foreach (IControls<Book, BookGenre> control in cat_panel.Children)
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
