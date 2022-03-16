﻿using FilmsUCWpf.Presenters;
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

namespace FilmsUCWpf.Views
{
    /// <summary>
    /// Логика взаимодействия для FilmControl.xaml
    /// </summary>
    public partial class FilmControl : UserControl, IView<Film>
    {
        public FilmPresenter Presenter { get; protected set; }
        public Film Info => Presenter.Model;
        public FilmControl(FilmPresenter presenter)
        {
            InitializeComponent();
            Presenter = presenter;
            Presenter.View = this;
        }

        private void id_GotFocus(object sender, RoutedEventArgs e)
        {
            Presenter.SetSelectedElement();
        }

        private bool commentIsOpen = false;
        private void btn_comment_Click(object sender, RoutedEventArgs e)
        {
            comment.Text = Info.Comment;

            if (commentIsOpen) { this.grid.Height -= 15; }
            else { this.grid.Height += 15; }

            commentIsOpen = !commentIsOpen;
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
            Presenter.OpenUpdateMenu();
        }

        public void SetVisualDefault()
        {
            SolidColorBrush myBrush = new SolidColorBrush(Color.FromRgb(53, 53, 53));
            this.id.Background = myBrush;
        }

        public void SetVisualSelected()
        {
            SolidColorBrush myBrush = new SolidColorBrush(Color.FromRgb(0, 220, 0));
            this.id.Background = myBrush;
        }

        public void SetVisualFinded()
        {
            SolidColorBrush myBrush = new SolidColorBrush(Color.FromRgb(0, 0, 220));
            this.id.Background = myBrush;
        }

        public void SelfRemove()
        {
            Panel panel = (Panel)this.Parent;
            panel.Children.Remove(this);
        }
    }
}