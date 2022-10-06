using FilmsUCWpf.View.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
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
using TablesLibrary.Interpreter;
using TL_Objects;

namespace FilmsUCWpf.View
{
    /// <summary>
    /// Логика взаимодействия для BookUpdateControl.xaml
    /// </summary>
    public partial class BookUpdateControl : UserControl
    {
        public BookUpdateControl()
        {
            InitializeComponent();
        }

        private void btn_comment_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_sources_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void watched_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void readed_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void TextInputIsNumber(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"\D");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
