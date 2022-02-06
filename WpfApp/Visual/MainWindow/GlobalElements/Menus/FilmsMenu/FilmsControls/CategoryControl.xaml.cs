using WpfApp.Visual.MainWindow.GlobalElements.Menus.ACommonElements.ControlsInterface;
using WpfApp.Visual.MainWindow.GlobalElements.Menus.FilmsMenu.UpdateControls;
using WpfApp.Visual.StaticVisualClasses;
using System;
using System.Collections.Generic;
using System.Text;
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

namespace WpfApp.Visual.MainWindow.GlobalElements.Menus.FilmsMenu.FilmsControls
{
    /// <summary>
    /// Логика взаимодействия для CategoryControl.xaml
    /// </summary>
    public partial class CategoryControl : UserControl, IControls<Category, Genre>
    {
        public Category Info { get; private set; }

        public CategoryControl(Category category)
        {
            InitializeComponent();
            this.Info = category;
            category.PropertyChanged += Category_PropertyChanged;
            category.CellRemoved += Category_CellRemoved;

            foreach (Film film in category.Films)
            {
                film.CellRemoved += Film_CellRemoved;
            }

            RefreshData();
        }

        private void Film_CellRemoved(object sender, EventArgs e)
        {
            Film film = (Film)sender;
            Info.Films.Remove(film);
        }

        private void Category_CellRemoved(object sender, EventArgs e)
        {
            Panel panel = (Panel)this.Parent;
            panel.Children.Remove(this);
        }

        private void Category_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
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

            foreach (Film film in Info.Films)
            {
                this.Height += 15;
                cat_panel.Children.Add(new SimpleControl(film));
            }

            SimpleControl simpleControl = null;
            for (int i = 0; i < cat_panel.Children.Count; i++)
            {
                simpleControl = (SimpleControl)cat_panel.Children[i];
                if (simpleControl.Info.FranshiseListIndex != i)
                {
                    cat_panel.Children.Remove(simpleControl);
                    cat_panel.Children.Insert(simpleControl.Info.FranshiseListIndex, simpleControl);
                    i = 0;
                }
            }

        }

        public void AddSimpleCotrol(Film film)
        {
            this.Height += 15;
            this.cat_panel.Children.Add(new SimpleControl(film));
            film.FranshiseListIndex = Convert.ToSByte(cat_panel.Children.Count - 1);
        }

        public bool RemoveFilmFromCategory(SimpleControl simpleControl)
        {
            if (simpleControl.Info.FranshiseId == this.Info.ID)
            {
                cat_panel.Children.Remove(simpleControl);

                this.Height -= 15;

                simpleControl.Info.FranshiseId = 0;
                simpleControl.Info.FranshiseListIndex = 0;

                foreach (Film film in Info.Films)
                {
                    if (simpleControl.Info.FranshiseListIndex < film.FranshiseListIndex)
                    {
                        --film.FranshiseListIndex;
                    }
                }

                return Info.Films.Remove(simpleControl.Info);
            }
            else
            {
                return false;
            }
        }

        public bool HasSelectedGenre(Genre[] selectedGenres)
        {
            foreach (SimpleControl control in cat_panel.Children)
            {
                if (control.HasSelectedGenre(selectedGenres))
                {
                    return true;
                }
            }

            return false;
        }

        public bool HasCheckedProperty(bool isWached)
        {
            foreach (SimpleControl control in cat_panel.Children)
            {
                if (control.HasCheckedProperty(isWached))
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

            foreach (IControls<Film, Genre> control in cat_panel.Children)
            {
                control.SetFindedElement(search);
            }

            return export;
        }

        public void SetVisualDefault()
        {
            SolidColorBrush myBrush = new SolidColorBrush(Color.FromRgb(53, 53, 53));
            this.id.Background = myBrush;

            foreach (IControls<Film, Genre> control in cat_panel.Children)
            {
                control.SetVisualDefault();
            }
        }

        public Control ToUpdateControl()
        {
            return new CategoryUpdateControl(this);
        }

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            MainInfo.MainWindow.FilmsMenu.UpdateFormVisualizer.OpenUpdateControl(this);
        }
    }
}
