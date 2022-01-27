using WpfApp.Visual.MainWindow.GlobalElements.Menus.ACommonElements.ControlsInterface;
using WpfApp.Visual.MainWindow.GlobalElements.Menus.FilmsMenu.FilmsControls;
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
using WpfApp.StaticFilmClasses;
using WpfApp.Visual.StaticVisualClasses;

namespace WpfApp.Visual.MainWindow.GlobalElements.Menus.FilmsMenu.UpdateControls
{
    /// <summary>
    /// Логика взаимодействия для SerieUpdateControl.xaml
    /// </summary>
    public partial class SerieUpdateControl : UserControl, IUpdateControl
    {
        private AElementControl control = null;
        private Serie serie = null;
        public SerieUpdateControl(SerieControl serieControl)
        {
            InitializeComponent();

            this.control = serieControl;

            this.serie = serieControl.SerieInfo;


            foreach (var item in MainInfo.Tables.GenresTable)
            {
                genre.Items.Add(item);
            }
            foreach (var item in FilmMethods.GetAllMarks().ToArray())
            {
                mark.Items.Add(item);
            }

            refresh();
        }

        public SerieUpdateControl(FilmControl filmControl)
        {
            InitializeComponent();
            this.control = filmControl;

            Film film = filmControl.FilmInfo;

            foreach (Serie serie in MainInfo.Tables.SeriesTable)
            {
                if (serie.FilmId == film.ID)
                {
                    this.serie = serie;
                    goto cont;
                }
            }

            Serie ser = new Serie();
            ser.Film = film;

            MainInfo.Tables.SeriesTable.AddElement(ser);

        cont:
            foreach (var item in MainInfo.Tables.GenresTable)
            {
                genre.Items.Add(item);
            }
            foreach (var item in FilmMethods.GetAllMarks().ToArray())
            {
                mark.Items.Add(item);
            }

            refresh();
        }

        private void refresh()
        {
            Film defFilm = MainInfo.Tables.FilmsTable.DefaultCell;

            this.id.Text = serie.Film.ID.ToString();
            this.name.Text = serie.Film.Name;
            this.genre.SelectedItem = serie.Film.Genre;
            this.realiseYear.Text = Film.FormatToString(serie.Film.RealiseYear, defFilm.RealiseYear);
            this.watched.IsChecked = serie.Film.Watched;
            this.watchDate.Date = serie.Film.DateOfWatch;
            this.mark.Text = VisualHelper.markToText(Film.FormatToString(serie.Film.Mark, defFilm.Mark));
            this.countOfViews.Text = Film.FormatToString(serie.Film.CountOfViews, defFilm.CountOfViews);
            this.comment.Text = serie.Film.Comment;

            Serie defSerie = MainInfo.Tables.SeriesTable.DefaultCell;

            this.startWatchDate.Date = serie.StartWatchDate;
            this.countOfWatchedSeries.Text = Serie.FormatToString(serie.CountOfWatchedSeries, defSerie.CountOfWatchedSeries);
            this.totalSeries.Text = Serie.FormatToString(serie.TotalSeries, defSerie.TotalSeries);
        }

        public void UpdateElement()
        {
            serie.Film.Name = this.name.Text;
            serie.Film.Genre = (Genre)genre.SelectedItem;
            serie.Film.RealiseYear = VisualHelper.TextToInt32(this.realiseYear.Text);
            serie.Film.Watched = (bool)this.watched.IsChecked;
            serie.Film.DateOfWatch = this.watchDate.Date;
            serie.Film.Mark = VisualHelper.TextToMark(this.mark.Text);
            serie.Film.CountOfViews = VisualHelper.TextToInt32(this.countOfViews.Text);
            serie.Film.Comment = this.comment.Text;

            serie.StartWatchDate = this.startWatchDate.Date;
            serie.CountOfWatchedSeries = VisualHelper.TextToInt32(this.countOfWatchedSeries.Text);
            serie.TotalSeries = VisualHelper.TextToInt32(this.totalSeries.Text);

            control.RefreshData();
        }

        private void btn_sources_Click(object sender, RoutedEventArgs e)
        {
            MainInfo.MainWindow.FilmsMenu.UpdateFormVisualizer.SourcesVisualizer.OpenSourceControl(this.serie.Film.Sources);
        }

        private bool commentIsOpen = false;
        private void btn_comment_Click(object sender, RoutedEventArgs e)
        {
            if (commentIsOpen) { this.grid.Height -= 20; }
            else { this.grid.Height += 20; }

            commentIsOpen = !commentIsOpen;
        }

        private void watched_Click(object sender, RoutedEventArgs e)
        {
            if (watchDate.IsEmpty)
            {
                watchDate.Date = DateTime.Today;
            }


            int cows = 0;
            int ts = 0;
            bool emptyTotal = true;

            if (countOfWatchedSeries.Text != "")
            {
                cows = Convert.ToInt32(countOfWatchedSeries.Text);
            }

            if (totalSeries.Text != "")
            {
                ts = Convert.ToInt32(totalSeries.Text);
                emptyTotal = false;
            }

            if (cows < ts && emptyTotal == false)
            {
                countOfWatchedSeries.Text = totalSeries.Text;
            }


            if (countOfViews.Text == "")
            {
                countOfViews.Text = "1";
            }
        }
    }
}
