using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using TablesLibrary.Interpreter.Table;

namespace FilmsUCWpf.View
{
    /// <summary>
    /// Логика взаимодействия для DateBox.xaml
    /// </summary>
    public partial class DateBox : UserControl
    {
        public bool IsEmpty =>
            year.Text == String.Empty &&
            month.Text == String.Empty &&
            day.Text == String.Empty;

        public DateBox()
        {
            InitializeComponent();
        }

        public DateTime Date
        {
            get { return (DateTime)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }

        
        public static readonly DependencyProperty DateProperty =
            DependencyProperty.Register(
                "Date", 
                typeof(DateTime), 
                typeof(DateBox),
                new FrameworkPropertyMetadata(new DateTime(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, DatePropertyChanged)
                );

        private static readonly DateTime defDate = new DateTime();
        private static void DatePropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            DateBox box = (DateBox)source;
            DateTime dateTime = (DateTime)e.NewValue;

            if (defDate == dateTime)
            {
                box.day.Text = String.Empty;
                box.month.Text = String.Empty;
                box.year.Text = String.Empty;
            }
            else
            {
                string daystr = dateTime.Day.ToString();
                if (daystr.Length < 2)
                    daystr = '0' + daystr;
                box.day.Text = daystr;

                string monthstr = dateTime.Month.ToString();
                if (monthstr.Length < 2)
                    monthstr = '0' + monthstr;
                box.month.Text = monthstr;

                box.year.Text = dateTime.Year.ToString();
            }
        }

        private void UserControl_LostFocus(object sender, RoutedEventArgs e)
        {
            int year = 0, month = 0, day = 0;

            year = convert(this.year.Text);
            month = convert(this.month.Text);
            day = convert(this.day.Text);

            if (month > 12)
                month = 12;

            if (month < 1)
                month = 1;


            int maxdays = DateTime.DaysInMonth(year, month);
            if (day > maxdays)
                day = maxdays;
            if (day < 1)
                day = 1;

            Date = new DateTime(year, month, day);
        }

        private void CheckIsNumber(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private int convert(String str)
        {
            if (str != String.Empty)
                return Convert.ToInt32(str);
            else
                return 1;
        }

        private void FillIfEmpty(object sender, MouseButtonEventArgs e)
        {
            if (IsEmpty)
            {
                Date = DateTime.Today;
            }
        }

        private void OpenCM(object sender, MouseButtonEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text != String.Empty)
            {
                cm.IsOpen = true;
            }
        }

        private void wipe_Click(object sender, RoutedEventArgs e)
        {
            this.Date = new DateTime();
        }
    }
}
