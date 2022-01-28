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
    /// Логика взаимодействия для SerieControl.xaml
    /// </summary>
    public partial class SerieControl : AElementControl
    {
		public SimpleControl simpleControl = null;

		private Serie serieInfo = null;
		public Serie SerieInfo
		{
			get { return serieInfo; }
		}

		public SerieControl(Film film)
		{
			InitializeComponent();
			this.Info = film;
			this.serieInfo = film.Serie;

			RefreshData();
		}

		public SerieControl(Serie serie)
		{
			InitializeComponent();
			this.Info = serie.Film;
			this.serieInfo = serie;

			RefreshData();
		}

		public SerieControl(SimpleControl simpleControl)
		{
			InitializeComponent();
			this.Info = simpleControl.Info;
			this.serieInfo = simpleControl.Info.Serie;
			this.simpleControl = simpleControl;

			RefreshData();
		}

		public override void RefreshData()
        {
			Film defFilm = MainInfo.Tables.FilmsTable.DefaultCell;

			this.id.Text = Info.ID.ToString();
			this.name.Text = Info.Name;
			this.genre.Text = Info.Genre.Name;
			this.realiseYear.Text = Film.FormatToString(Info.RealiseYear, defFilm.RealiseYear);
			this.watched.IsChecked = Info.Watched;
			this.watchDate.Text = Film.FormatToString(Info.DateOfWatch, defFilm.DateOfWatch);
			this.mark.Text = VisualHelper.markToText(Film.FormatToString(Info.Mark, defFilm.Mark));
			this.countOfviews.Text = Film.FormatToString(Info.CountOfViews, defFilm.CountOfViews);

			Serie defSerie = MainInfo.Tables.SeriesTable.DefaultCell;

			this.startWatchDate.Text = Serie.FormatToString(serieInfo.StartWatchDate, defSerie.StartWatchDate);
			this.countOfWatchedSeries.Text = Serie.FormatToString(serieInfo.CountOfWatchedSeries, defSerie.CountOfWatchedSeries);
			this.totalSeries.Text = Serie.FormatToString(serieInfo.TotalSeries, defSerie.TotalSeries);
			this.refreshSourceLabel();

			if (simpleControl != null)
			{
				simpleControl.RefreshData();
			}
		}

		public void refreshSourceLabel()
		{
			if (Info.Sources.Count == 0)
			{
				btn_copyUrl.Content = "no url";
			}
			else
			{
				if (Info.Sources[0].Name != "")
				{
					btn_copyUrl.Content = "url: " + Info.Sources[0].Name;
				}
				else
				{
					btn_copyUrl.Content = "copy url";
				}
			}
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
			return new SerieUpdateControl(this);
		}

        private void btn_copyUrl_Click(object sender, RoutedEventArgs e)
        {
			if (Info.Sources.Count != 0)
			{
				Clipboard.SetText(Info.Sources[0].SourceUrl);
			}
		}

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
			MainInfo.MainWindow.FilmsMenu.UpdateFormVisualizer.OpenUpdateControl(this);
		}

		private bool commentIsOpen = false;
		private void btn_comment_Click(object sender, RoutedEventArgs e)
		{
			comment.Text = Info.Comment;

			if (commentIsOpen) { this.grid.Height -= 15; }
			else { this.grid.Height += 15; }

			commentIsOpen = !commentIsOpen;
		}

        private void watched_Click(object sender, RoutedEventArgs e)
        {
			watched.IsChecked = !watched.IsChecked;
        }

        private void id_GotFocus(object sender, RoutedEventArgs e)
        {
			SetSelectedElement();
		}
    }
}
