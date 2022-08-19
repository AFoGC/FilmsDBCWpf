using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using TL_Objects.CellDataClasses;

namespace FilmsUCWpf.View
{
    /// <summary>
    /// Логика взаимодействия для SourceControl.xaml
    /// </summary>
    public partial class SourceControl : UserControl
    {
        private readonly Source source;
        private readonly ObservableCollection<Source> sources;
        public SourceControl(Source source, ObservableCollection<Source> sources)
        {
            InitializeComponent();
            this.source = source;
            this.sources = sources;
            this.DataContext = source;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            sources.Remove(source);
            sources.Insert(0, source);
        }
    }
}
