﻿using FilmsUCWpf.Presenter;
using FilmsUCWpf.PresenterInterfaces;
using FilmsUCWpf.ViewInterfaces;
using InfoMenusWpf;
using System;
using System.Collections;
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
using TablesLibrary.Interpreter;
using TL_Objects;

namespace FilmsUCWpf.View
{
    /// <summary>
    /// Логика взаимодействия для BookUpdateControl.xaml
    /// </summary>
    public partial class BookUpdateControl : UserControl, IBookUpdateView, IUpdateControl
    {
        private BookUpdatePresenter presenter;
        public BookUpdateControl(Book book, IMenu<Book> menu, TableCollection table)
        {
            InitializeComponent();
            presenter = new BookUpdatePresenter(book, this, menu, table);
        }

        private void btn_comment_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_sources_Click(object sender, RoutedEventArgs e)
        {
            presenter.OpenSources();
        }

        private void watched_Click(object sender, RoutedEventArgs e)
        {
            if (fullReadDate.IsEmpty)
            {
                fullReadDate.Date = DateTime.Today;
            }

            if (countOfReadings.Text == "")
            {
                countOfReadings.Text = "1";
            }
        }

        public void UpdateElement()
        {
            presenter.UpdateElement();
        }

        public string ID { set => id.Text = value; }
        string IBookUpdateView.Name { get => name.Text; set => name.Text = value; }
        public IList Genres { get => genre.Items; }
        public BookGenre Genre { get => (BookGenre)genre.SelectedItem; set => genre.SelectedItem = value; }
        public string RealiseYear { get => realiseYear.Text; set => realiseYear.Text = value; }
        public bool Readed { get => (bool)readed.IsChecked; set => readed.IsChecked = value; }
        public string Author { get => author.Text; set => author.Text = value; }
        public DateTime FullReadDate { get => fullReadDate.Date; set => fullReadDate.Date = value; }
        public IList Marks => mark.Items;
        public string Mark { get => mark.Text; set => mark.Text = value; }
        public string CountOfReadings { get => countOfReadings.Text; set => countOfReadings.Text = value; }
        public string Bookmark { get => bookmark.Text; set => bookmark.Text = value; }
    }
}
