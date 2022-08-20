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
using TL_Objects.Interfaces;

namespace FilmsUCWpf.View
{
    /// <summary>
    /// Логика взаимодействия для GenrePressButton.xaml
    /// </summary>
    public partial class GenrePressButton : UserControl
    {
        public IGenre Genre { get; private set; }
        public bool IsChecked
        {
            get => (bool)button.IsChecked;
            set => button.IsChecked = value;
        }
        public GenrePressButton(IGenre genre)
        {
            InitializeComponent();
            this.Genre = genre;
            this.DataContext = genre;
        }
    }
}
