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
using WpfApp.Models;
using WpfApp.Presenters;
using WpfApp.Services;
using WpfApp.Views.Interfaces;

namespace WpfApp.Views
{
    /// <summary>
    /// Логика взаимодействия для ProflieView.xaml
    /// </summary>
    public partial class ProflieView : UserControl, IProfileView
    {
        private readonly ProfilePresenter presenter;
        public ProflieView(Profile profile, ProgramSettings settings, ProfileContainerPresenter parentPresenter)
        {
            InitializeComponent();
            presenter = new ProfilePresenter(profile, this, settings, parentPresenter);
            ProfileNameTextBox.Text = profile.Name;
        }

        public void SetSelected(Profile usedProfile) => presenter.SetSelected(usedProfile);

        public void SetVisualDefault()
        {
            this.ProfileNameTextBox.Background = new SolidColorBrush(Color.FromRgb(53, 53, 53));
            this.ProfileNameTextBox.BorderBrush = new SolidColorBrush(Color.FromRgb(122, 122, 122));
        }

        public void SetVisualSelected()
        {
            this.ProfileNameTextBox.Background = new SolidColorBrush(Color.FromRgb(53, 53, 83));
            this.ProfileNameTextBox.BorderBrush = new SolidColorBrush(Color.FromRgb(122, 122, 182));
        }

        private void ChangeProfileButton_Click(object sender, RoutedEventArgs e)
        {
            presenter.ChangeProfile();
        }

        private void DeleteProfileButton_Click(object sender, RoutedEventArgs e)
        {
            presenter.DeleteThisProfile();
        }

        private void RenameProfileButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
