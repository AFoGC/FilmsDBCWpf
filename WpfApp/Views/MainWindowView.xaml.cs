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
        public MainWindowView()
        {
            InitializeComponent();
            navbar.Window = this;

            MainWindowModel model = new MainWindowModel();
            presenter = new MainWindowPresenter(model);

            menus.Children.Add(new SettingsMenuView(model));
            menus.Children.Add(new BooksMenuView(model));
            menus.Children.Add(new FilmsMenuView(model));

            presenter.WindowLoaded();
            model.InfoUnsaved = false;
        }

        public void ChangePriorityMenu<Element>() where Element : UIElement
        {
            foreach (UIElement el in menus.Children)
            {
                if (el.GetType() == typeof(Element))
                {
                    Grid.SetZIndex(el, menus.Children.Count);
                    el.Focus();
                }
                else
                {
                    Grid.SetZIndex(el, Grid.GetZIndex(el) - 1);
                }
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
