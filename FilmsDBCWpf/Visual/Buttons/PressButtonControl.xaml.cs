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

namespace FilmsDBCWpf.Visual.Buttons
{
    /// <summary>
    /// Логика взаимодействия для PressButtonControl.xaml
    /// </summary>
    public partial class PressButtonControl : UserControl
    {
        public PressButtonControl()
        {
            DataContext = this;
            InitializeComponent();
        }

        private Brush included_mouseEnterColor;
        private Brush included_defaultColor;
        private Brush included_mouseDownColor;
        private Brush mouseEnterColor;
        private Brush defaultColor;
        private Brush mouseDownColor;

        public Brush Included_MouseEnterColor
        {
            get { return included_mouseEnterColor; }
            set
            {
                included_mouseEnterColor = value;
            }
        }
        public Brush Included_DefaultColor
        {
            get { return included_defaultColor; }
            set
            {
                included_defaultColor = value;
            }
        }
        public Brush Included_MouseDownColor
        {
            get { return included_mouseDownColor; }
            set
            {
                included_mouseDownColor = value;
            }
        }

        public Brush MouseEnterColor
        {
            get { return mouseEnterColor; }
            set
            {
                mouseEnterColor = value;
            }
        }
        public Brush DefaultColor
        {
            get { return defaultColor; }
            set
            {
                defaultColor = value;
            }
        }
        public Brush MouseDownColor
        {
            get { return mouseDownColor; }
            set
            {
                mouseDownColor = value;
            }
        }
    }
}
