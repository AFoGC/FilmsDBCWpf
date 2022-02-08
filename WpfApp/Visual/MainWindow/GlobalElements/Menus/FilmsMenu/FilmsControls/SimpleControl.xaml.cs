using WpfApp.Visual.MainWindow.GlobalElements.Menus.ACommonElements.ControlsInterface;
using WpfApp.Visual.MainWindow.GlobalElements.Menus.FilmsMenu.UpdateControls;
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
	/// Логика взаимодействия для SimpleControl.xaml
	/// </summary>
	public partial class SimpleControl : AElementControl, ISimpleControl
	{
		public SimpleControl(Film film)
		{
			InitializeComponent();
			this.Info = film;
			film.PropertyChanged += Film_PropertyChanged;
			film.CellRemoved += Film_CellRemoved;
			film.Genre.PropertyChanged += Film_PropertyChanged;

			RefreshData();
		}

		private void Film_CellRemoved(object sender, EventArgs e)
		{
			Panel panel = (Panel)this.Parent;
			if(panel != null)
				panel.Children.Remove(this);
		}

		private void Film_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			RefreshData();
		}

		public override void RefreshData()
		{
			Film defFilm = MainInfo.Tables.FilmsTable.DefaultCell;

			id.Text = Info.ID.ToString();
			name.Text = Info.Name;
			genre.Text = Info.Genre.Name;
			realiseYear.Text = Film.FormatToString(Info.RealiseYear, defFilm.RealiseYear);
			watched.IsChecked = Info.Watched;
		}

		public override void SetVisualDefault()
		{
			SolidColorBrush myBrush = new SolidColorBrush(Color.FromRgb(53,53,53));

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

		public Control ToMoreInfo()
		{
			if (Info.Genre.IsSerialGenre)
			{
				return new SerieControl(this);
			}
			else
			{
				return new FilmControl(this);
			}
		}

		public override Control ToUpdateControl()
		{
			if (Info.Genre.IsSerialGenre)
			{
				return new SerieUpdateControl(new SerieControl(this));
			}
			else
			{
				return new FilmUpdateControl(new FilmControl(this));
			}
		}

		private void watched_Checked(object sender, RoutedEventArgs e)
		{
			watched.IsChecked = !watched.IsChecked;
		}

		private void watched_Click(object sender, RoutedEventArgs e)
		{
			watched.IsChecked = !watched.IsChecked;
		}

		private void btn_moreInfo_Click(object sender, RoutedEventArgs e)
		{
			MainInfo.MainWindow.FilmsMenu.MoreInfoFormVisualizer.OpenMoreInfoForm(this);
		}

		private void id_GotFocus(object sender, RoutedEventArgs e)
		{
			SetSelectedElement();
		}
	}
}
