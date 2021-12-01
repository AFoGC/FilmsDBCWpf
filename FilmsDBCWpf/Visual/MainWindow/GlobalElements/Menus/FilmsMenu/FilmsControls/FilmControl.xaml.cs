using FilmsDBCWpf.Visual.MainWindow.GlobalElements.Menus.FilmsMenu.UpdateControls;
using FilmsDBCWpf.Visual.StaticVisualClasses;
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

namespace FilmsDBCWpf.Visual.MainWindow.GlobalElements.Menus.FilmsMenu.FilmsControls
{
    /// <summary>
    /// Логика взаимодействия для FilmControl.xaml
    /// </summary>
    public partial class FilmControl : AElementControl
    {
        public SimpleControl simpleControl = null;

        public FilmControl(Film film)
        {
            InitializeComponent();
            this.filmInfo = film;

            RefreshData();
        }

        public FilmControl(SimpleControl simpleControl)
        {
            InitializeComponent();
            this.filmInfo = simpleControl.FilmInfo;
            this.simpleControl = simpleControl;

            RefreshData();
        }

        public override void RefreshData()
        {
            Film defFilm = MainInfo.Tables.FilmsTable.DefaultCell;

            this.id.Text = filmInfo.ID.ToString();
            this.name.Text = filmInfo.Name;
            this.genre.Text = filmInfo.Genre.Name;
            this.realiseYear.Text = Film.FormatToString(filmInfo.RealiseYear, defFilm.RealiseYear);
            this.watched.IsChecked = filmInfo.Watched;
            this.watchDate.Text = Film.FormatToString(filmInfo.DateOfWatch, defFilm.DateOfWatch);
            this.mark.Text = VisualHelper.markToText(Film.FormatToString(filmInfo.Mark, defFilm.Mark));
            this.countOfviews.Text = Film.FormatToString(filmInfo.CountOfViews, defFilm.CountOfViews);
            this.comment.Text = filmInfo.Comment;
            this.RefreshSourceLabel();

            if (simpleControl != null)
            {
                simpleControl.RefreshData();
            }
        }

        public void RefreshSourceLabel()
        {
            if (filmInfo.Sources.Count == 0)
            {
                btn_copyUrl.Content = "no url";
            }
            else
            {
                if (filmInfo.Sources[0].Name != "")
                {
                    btn_copyUrl.Content = "url: " + filmInfo.Sources[0].Name;
                }
                else
                {
                    btn_copyUrl.Content = "copy url";
                }
            }
        }

        private bool commentIsOpen = false;
        private void btn_comment_Click(object sender, RoutedEventArgs e)
        {
            comment.Text = filmInfo.Comment;

            if (commentIsOpen) { this.grid.Height -= 15; }
            else { this.grid.Height += 15; }
            //this.Height -= 15;

            commentIsOpen = !commentIsOpen;
        }

        private void btn_copyUrl_Click(object sender, RoutedEventArgs e)
        {
            if (filmInfo.Sources.Count != 0)
            {
                Clipboard.SetText(filmInfo.Sources[0].SourceUrl);
            }
        }

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            MainInfo.MainWindow.FilmsMenu.UpdateVisualizer.OpenUpdateControl(this, MainInfo.MainWindow.FilmsMenu.MoreInfoVisualizer);
        }

        private void watched_Click(object sender, RoutedEventArgs e)
        {
            watched.IsChecked = !watched.IsChecked;
        }

        private void id_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SetSelectedElement(MainInfo.MainWindow.FilmsMenu.ControlInBuffer);
        }

        public override void SetVisualDefault()
        {
            SolidColorBrush myBrush = new SolidColorBrush(Color.FromRgb(53, 53, 53));
            this.id.Background = myBrush;
        }

        internal override void setVisualFinded()
        {
            SolidColorBrush myBrush = new SolidColorBrush(Color.FromRgb(0, 0, 220));
            this.id.Background = myBrush;
        }

        internal override void setVisualSelected()
        {
            SolidColorBrush myBrush = new SolidColorBrush(Color.FromRgb(0, 220, 0));
            this.id.Background = myBrush;
        }

        public override Control ToUpdateControl()
        {
            if (filmInfo.Genre.IsSerialGenre)
            {
                return new SerieUpdateControl(this);
            }
            else
            {
                return new FilmUpdateControl(this);
            }
        }
    }
}
