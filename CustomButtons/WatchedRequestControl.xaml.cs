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

namespace CustomButtons
{
    /// <summary>
    /// Логика взаимодействия для WatchedRequestControl.xaml
    /// </summary>
    public partial class WatchedRequestControl : UserControl
    {
        public WatchedRequestControl()
        {
            InitializeComponent();
        }

        public PressButton PressButtonWatched
        {
            get { return pbtn_watched; }
        }

        public PressButton PressButtonUnwatched
        {
            get { return pbtn_unwatched; }
        }

        public object LeftText { get => pbtn_watched.Content; set => pbtn_watched.Content = value; }
        public object RightText { get => pbtn_unwatched.Content; set => pbtn_unwatched.Content = value; }

        public bool IsWatched
        {
            get { return pbtn_watched.Included; }
            set { pbtn_watched.Included = value; }
        }

        public bool IsUnwatched
        {
            get { return pbtn_unwatched.Included; }
            set { pbtn_unwatched.Included = value; }
        }

        public bool IsAllIncluded
        {
            get => pbtn_watched.Included && pbtn_unwatched.Included;
        }
    }
}
