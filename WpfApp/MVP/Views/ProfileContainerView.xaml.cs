﻿using System;
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
using WpfApp.Config;
using WpfApp.MVP.Presenters;
using WpfApp.MVP.ViewsInterface;

namespace WpfApp.MVP.Views
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
        double IProfileSettingsContainerView.Height { get => grid.Height; set => grid.Height = value; }

        private void AddProfileButton_Click(object sender, RoutedEventArgs e)
        {
            presenter.AddProfile();
        }
    }
}
