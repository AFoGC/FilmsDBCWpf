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
using System.Windows.Shapes;
using WpfApp.Models;
using WpfApp.Presenters;
using WpfApp.Views.Interfaces;

namespace WpfApp.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindowView.xaml
    /// </summary>
    public partial class MainWindowView : Window, IChangeMenu
    {
        private MainWindowPresenter presenter;

        private readonly SettingsMenuView settingsMenu;
        private readonly BooksMenuView booksMenu;
        private readonly FilmsMenuView filmsMenu;

        public MainWindowView()
        {
            InitializeComponent();
            navbar.Window = this;

            MainWindowModel model = new MainWindowModel();
            presenter = new MainWindowPresenter(model);

            settingsMenu = new SettingsMenuView(model);
            booksMenu = new BooksMenuView(model);
            filmsMenu = new FilmsMenuView(model);

            ChangeMenu(SelectedMenu.FilmsMenu);

            model.InfoUnsaved = false;
        }

        public void ChangeMenu(SelectedMenu menu)
        {
            switch (menu)
            {
                case SelectedMenu.FilmsMenu:
                    menus.Navigate(filmsMenu);
                    break;
                case SelectedMenu.BooksMenu:
                    menus.Navigate(booksMenu);
                    break;
                case SelectedMenu.SettingMenu:
                    menus.Navigate(settingsMenu);
                    break;
            }
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
    }
}
