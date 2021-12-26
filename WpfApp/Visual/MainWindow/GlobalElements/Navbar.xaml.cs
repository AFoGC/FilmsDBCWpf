using WpfApp.Visual.Buttons;
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

namespace WpfApp.Visual.MainWindow.GlobalElements
{
    /// <summary>
    /// Логика взаимодействия для Navbar.xaml
    /// </summary>
    public partial class Navbar : UserControl
    {
        List<PressButton> pressButtons = new List<PressButton>();
        public Navbar()
        {
            InitializeComponent();
            pressButtons.Add(films);
            pressButtons.Add(books);
            pressButtons.Add(settings);
            changeSelectedButton(films);
            changeSelectedButton(books);
            changeSelectedButton(settings);
            changeSelectedButton(films);
        }

        private void films_Click(object sender, RoutedEventArgs e)
        {
            changeSelectedButton((PressButton)sender);
            ChangePriorityMenu(MainInfo.MainWindow.FilmsMenu);

        }

        private void books_Click(object sender, RoutedEventArgs e)
        {
            changeSelectedButton((PressButton)sender);
            ChangePriorityMenu(MainInfo.MainWindow.BooksMenu);
        }

        private void settings_Click(object sender, RoutedEventArgs e)
        {
            changeSelectedButton((PressButton)sender);
            ChangePriorityMenu(MainInfo.MainWindow.SettingsMenu);
        }

        private void changeSelectedButton(PressButton pressButton)
        {
            foreach (PressButton button in pressButtons)
            {
                if (pressButton == button)
                {
                    pressButton.Included = true;
                    pressButton.ClickLocked = true;
                }
                else
                {
                    button.ClickLocked = false;
                    button.Included = false;
                }
            }
        }

        private void ChangePriorityMenu(UIElement element)
        {
            foreach (UIElement el in MainInfo.MainWindow.Menus.Children)
            {
                Grid.SetZIndex(el, Grid.GetZIndex(el) - 1);
            }
            Grid.SetZIndex(element, MainInfo.MainWindow.Menus.Children.Count);
        }
    }
}
