using CustomButtons;
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
using WpfApp.Views;
using WpfApp.ViewsInterface;

namespace WpfApp.Views
{
    /// <summary>
    /// Логика взаимодействия для NavbarControl.xaml
    /// </summary>
    public partial class NavbarControl : UserControl
    {
        private List<PressButton> pressButtons = new List<PressButton>();
        public IChangeMenu Window { get; set; }
        public NavbarControl()
        {
            InitializeComponent();
            pressButtons.Add(films);
            pressButtons.Add(books);
            pressButtons.Add(settings);
            changeSelectedButton(films);

            //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
        }

        private void films_Click(object sender, RoutedEventArgs e)
        {
            changeSelectedButton((PressButton)sender);
            Window.ChangePriorityMenu<FilmsMenuView>();
        }

        private void books_Click(object sender, RoutedEventArgs e)
        {
            changeSelectedButton((PressButton)sender);
            Window.ChangePriorityMenu<BooksMenuView>();
        }

        private void settings_Click(object sender, RoutedEventArgs e)
        {
            changeSelectedButton((PressButton)sender);
            Window.ChangePriorityMenu<SettingsMenuView>();
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
