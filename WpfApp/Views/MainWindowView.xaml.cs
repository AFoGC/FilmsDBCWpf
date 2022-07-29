using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp.Models;
using WpfApp.Presenters;
using WpfApp.Views.Interfaces;

namespace WpfApp.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindowView.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        private MainWindowPresenter presenter;

        private readonly SettingsMenuView settingsMenu;
        private readonly BooksMenuView booksMenu;
        private readonly FilmsMenuView filmsMenu;

        public MainWindowView()
        {
            InitializeComponent();

            MainWindowModel model = new MainWindowModel();
            presenter = new MainWindowPresenter(model);

            settingsMenu = new SettingsMenuView(model);
            booksMenu = new BooksMenuView(model);
            filmsMenu = new FilmsMenuView(model);

            films_Click(films, new RoutedEventArgs());

            model.InfoUnsaved = false;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            presenter.WindowClosed();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (presenter.InfoUnsaved)
            {
                ExitWindow window = new ExitWindow();
                window.ShowDialog();
                if (window.Save)
                {
                    presenter.SaveTables();
                }

                e.Cancel = !window.CloseProg;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.S && (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
            {
                presenter.SaveTables();
            }
        }

        private void changeSelectedButton(object sender)
        {
            ToggleButton pressButton = (ToggleButton)sender;

            foreach (ToggleButton button in Navbar.Children)
            {
                if (pressButton == button)
                {
                    pressButton.IsChecked = true;
                }
                else
                {
                    button.IsChecked = false;
                }
            }
        }

        private void films_Click(object sender, RoutedEventArgs e)
        {
            changeSelectedButton(sender);
            menus.Navigate(filmsMenu);
        }

        private void books_Click(object sender, RoutedEventArgs e)
        {
            changeSelectedButton(sender);
            menus.Navigate(booksMenu);
        }

        private void settings_Click(object sender, RoutedEventArgs e)
        {
            changeSelectedButton(sender);
            menus.Navigate(settingsMenu);
        }
    }
}
