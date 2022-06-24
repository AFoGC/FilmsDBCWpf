﻿using FilmsUCWpf.ModelBinder;
using FilmsUCWpf.Presenter;
using FilmsUCWpf.PresenterInterfaces;
using FilmsUCWpf.ViewInterfaces;
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

namespace FilmsUCWpf.View
{
    /// <summary>
    /// Логика взаимодействия для BookInCategorySimpleControl.xaml
    /// </summary>
    public partial class BookInCategorySimpleControl : UserControl, IView
    {
		BookPresenter presenter;
		public BookInCategorySimpleControl()
		{
			InitializeComponent();
		}

		private void id_Click(object sender, RoutedEventArgs e)
		{
			presenter.SetSelectedElement();
		}

		private void id_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			presenter.SetSelectedElement();
		}

		private void btn_moreInfo_Click(object sender, RoutedEventArgs e)
		{
			presenter.OpenInfoMenu();
		}

		public bool SetPresenter(IBasePresenter presenter)
		{
			if (this.presenter == null)
			{
				this.presenter = (BookPresenter)presenter;
				DataContext = new BookSimpleBinder(this.presenter.Model);
				return true;
			}
			else
			{
				return false;
			}
		}

		public void SetVisualDefault()
		{
			SolidColorBrush myBrush = BrushColors.DefaultColor;
			this.id.Background = myBrush;
			this.name.Background = myBrush;
			this.genre.Background = myBrush;
			this.realiseYear.Background = myBrush;
		}

		public void SetVisualSelected()
		{
			SolidColorBrush myBrush = BrushColors.SelectColor;
			this.id.Background = myBrush;
			this.name.Background = myBrush;
			this.genre.Background = myBrush;
			this.realiseYear.Background = myBrush;
		}

		public void SetVisualFinded()
		{
			SolidColorBrush myBrush = BrushColors.FindColor;
			this.id.Background = myBrush;
			this.name.Background = myBrush;
			this.genre.Background = myBrush;
			this.realiseYear.Background = myBrush;
		}

		public void SelfRemove()
		{
			Panel panel = (Panel)this.Parent;
			if (panel != null)
				panel.Children.Remove(this);
		}

		private void btn_update_Click(object sender, RoutedEventArgs e)
		{
			presenter.OpenUpdateMenu();
		}

		private void MenuItem_Click(object sender, RoutedEventArgs e)
		{
			presenter.DeleteThis();
		}

		private void addToPriority_Click(object sender, RoutedEventArgs e)
		{
			presenter.AddToPriority();
		}

        private void Up_Click(object sender, RoutedEventArgs e)
        {
			presenter.UpFranshiseListID();
        }

        private void Down_Click(object sender, RoutedEventArgs e)
        {
			presenter.DownFranshiseListID();
        }
    }
}