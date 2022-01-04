using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace WpfApp.Visual.Buttons
{
    /// <summary>
    /// Логика взаимодействия для DateUpdateControl.xaml
    /// </summary>
    public partial class DateUpdateControl : UserControl
    {
        DateTime date = new DateTime();
        public DateUpdateControl()
        {
            InitializeComponent();
            this.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
        }

        private void refresh()
        {
            if (date != new DateTime())
            {
                if (date.Day < 10) { day.Text = "0"; }
                day.Text += date.Day.ToString();
                if (date.Month < 10) { month.Text = "0"; }
                month.Text += date.Month.ToString();

                year.Text = date.Year.ToString();
            }
            else
            {
                day.Text = "";
                month.Text = "";
                year.Text = "";
            }
        }

        private void day_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (day.Text == "" && month.Text == "" && year.Text == "")
            {
                this.date = DateTime.Today;
                refresh();
            }
        }

        public DateTime Date
        {
            get
            {
                int year = 1, month = 1, day = 1;

                if (this.year.Text != "") { year = Convert.ToInt32(this.year.Text); }
                if (this.month.Text != "") { month = Convert.ToInt32(this.month.Text); }
                if (this.day.Text != "") { day = Convert.ToInt32(this.day.Text); }

                date = new DateTime(year, month, day);
                return date;
            }
            set
            {
                date = value;
                refresh();
            }
        }

        public bool IsEmpty
        {
            get
            {
                if (year.Text == "" && month.Text == "" && day.Text == "")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (day.Text == "" && month.Text == "" && year.Text == "")
            {
                this.date = DateTime.Today;
                refresh();
            }
        }

        private void Grid_GotFocus(object sender, RoutedEventArgs e)
        {
            if (day.Text == "" && month.Text == "" && year.Text == "")
            {
                this.date = DateTime.Today;
                refresh();
            }
        }



        /*
        [Bindable(true)]
        new public Brush Foreground
        {
            set
            {
                base.Foreground = value;
                day.Foreground = value;
                month.Foreground = value;
                year.Foreground = value;
                dot1.Foreground = value;
                dot2.Foreground = value;
            }
        }
        */
    }
}
