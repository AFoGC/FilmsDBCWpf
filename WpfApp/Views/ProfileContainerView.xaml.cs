﻿using ProfilesConfig;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
using WpfApp.Presenters;
using WpfApp.Views.Interfaces;

namespace WpfApp.Views
{
    /// <summary>
    /// Логика взаимодействия для ProfileContainerView.xaml
    /// </summary>
    public partial class ProfileContainerView : UserControl, IProfileSettingsContainerView
    {
        private readonly ProfileContainerPresenter presenter;
        public ProfileContainerView(ProgramSettings settings)
        {
            InitializeComponent();
            presenter = new ProfileContainerPresenter(this, settings);
        }

        public IList ProfileControls => ProfilesPanel.Children;

        public string AddProfileText { get => AddProfileTextBox.Text; set => AddProfileTextBox.Text = value; }

        private static readonly char[] symbols = new char[] { '"', '\\', '/', ':', '|', '<', '>', '*', '?'};
        private void AddProfileButton_Click(object sender, RoutedEventArgs e)
        {
            if (AddProfileText.IndexOfAny(symbols) != -1)
                MessageBox.Show("The following characters are not allowed: \" \\ / : | < > * ? ");
            else presenter.AddProfile();
            
        }

        private void OpenDirectoryButton_Click(object sender, RoutedEventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                Arguments = presenter.AllProfilesPath,
                FileName = "explorer.exe"
            };
            Process.Start(startInfo);
        }

        private void ImportFileButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Films";
            dialog.DefaultExt = ".fdbc";
            dialog.Filter = "Film documents (.fdbc)|*.fdbc";
            Nullable<bool> result = dialog.ShowDialog();
            if (result == true)
            {
                presenter.ImportProfile(dialog.FileName);
                presenter.RefreshControl();
            }
        }
    }
}
