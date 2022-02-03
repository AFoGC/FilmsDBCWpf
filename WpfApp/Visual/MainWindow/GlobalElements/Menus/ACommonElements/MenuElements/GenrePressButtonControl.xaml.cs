﻿using System;
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
using TL_Objects;
using WpfApp.Visual.Buttons;

namespace WpfApp.Visual.MainWindow.GlobalElements.Menus.ACommonElements.MenuElements
{
    /// <summary>
    /// Логика взаимодействия для GenrePressButtonControl.xaml
    /// </summary>
    public partial class GenrePressButtonControl : UserControl
    {
        private Genre genre = null;
        public Genre Genre
        {
            get { return genre; }
        }
        public PressButton PressButton
        {
            get { return pbtn; }
        }
        public GenrePressButtonControl(Genre genre)
        {
            InitializeComponent();
            this.genre = genre;
            this.DataContext = genre;
        }
    }
}
