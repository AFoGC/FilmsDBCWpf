using FilmsDBCWpf.Visual.Buttons;
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

namespace FilmsDBCWpf.Visual.MainWindow.GlobalElements
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
            //MainInfo.MainForm.MainControl.BringToFront();
        }

        private void books_Click(object sender, RoutedEventArgs e)
        {
            changeSelectedButton((PressButton)sender);
            //MainInfo.MainForm.BooksControl.BringToFront();
        }

        private void settings_Click(object sender, RoutedEventArgs e)
        {
            changeSelectedButton((PressButton)sender);
            //MainInfo.MainForm.SettingsControl.RefreshControl();
            //MainInfo.MainForm.SettingsControl.BringToFront();
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
    }
}
