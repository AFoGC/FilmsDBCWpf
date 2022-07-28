using FilmsUCWpf.View;
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
using TablesLibrary.Interpreter.TableCell;
using TL_Objects.CellDataClasses;

namespace WpfApp.Views
{
    /// <summary>
    /// Логика взаимодействия для SourcesControl.xaml
    /// </summary>
    public partial class SourcesControl : UserControl
    {
        public SourcesControl()
        {
            InitializeComponent();
        }

        private ObservableCollection<Source> sources;
        public void Open(ObservableCollection<Source> sources)
        {
            this.sources = sources;
            SourcesPanel.Children.Clear();

            foreach (Source source in sources)
            {
                SourcesPanel.Children.Add(new SourceControl(source));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Source source = new Source();
            sources.Add(source);
            SourcesPanel.Children.Add(new SourceControl(source));
        }
    }
}
