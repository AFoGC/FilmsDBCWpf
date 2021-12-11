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
    public partial class CategoryControl : UserControl, IFilmsControl
    {
        private Category categoryInfo = null;
        public Category CategoryInfo
        {
            get { return categoryInfo; }
        }

        public CategoryControl(Category category)
        {
            InitializeComponent();
            this.categoryInfo = category;

            RefreshData();
        }

        public void RefreshData()
        {
            id.Text = categoryInfo.ID.ToString();
            name.Text = categoryInfo.Name;
            mark.Text = VisualHelper.markToText(Category.FormatToString(categoryInfo.Mark, -1));

            categoryFilms();
        }

        private void categoryFilms()
        {
            this.cat_panel.Children.Clear();
            this.Height = 20;

            foreach (Film film in categoryInfo.Films)
            {
                this.Height += 15;
                cat_panel.Children.Add(new SimpleControl(film));
            }

            SimpleControl simpleControl = null;
            for (int i = 0; i < cat_panel.Children.Count; i++)
            {
                simpleControl = (SimpleControl)cat_panel.Children[i];
                if (simpleControl.FilmInfo.FranshiseListIndex != i)
                {
                    cat_panel.Children.Remove(simpleControl);
                    cat_panel.Children.Insert(simpleControl.FilmInfo.FranshiseListIndex, simpleControl);
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
            if (simpleControl.FilmInfo.FranshiseId == this.categoryInfo.ID)
            {
                cat_panel.Children.Remove(simpleControl);

                this.Height -= 15;

                simpleControl.FilmInfo.FranshiseId = 0;
                simpleControl.FilmInfo.FranshiseListIndex = 0;

                foreach (Film film in categoryInfo.Films)
                {
                    if (simpleControl.FilmInfo.FranshiseListIndex < film.FranshiseListIndex)
                    {
                        --film.FranshiseListIndex;
                    }
                }

                return categoryInfo.Films.Remove(simpleControl.FilmInfo);
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

        public bool HasWatchedProperty(bool isWached)
        {
            foreach (SimpleControl control in cat_panel.Children)
            {
                if (control.HasWatchedProperty(isWached))
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
            return new CategoryUpdateControl(this);
        }

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            MainInfo.MainWindow.FilmsMenu.UpdateVisualizer.OpenUpdateControl(this, MainInfo.MainWindow.FilmsMenu.MoreInfoVisualizer);
        }
    }
}
