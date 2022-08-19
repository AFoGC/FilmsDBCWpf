using FilmsUCWpf.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
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

        public ObservableCollection<Source> Sources { get; private set; }
        public void Open(ObservableCollection<Source> sources)
        {
            if (Sources != null)
                Sources.CollectionChanged -= Sources_CollectionChanged;

            Sources = sources;
            Sources.CollectionChanged += Sources_CollectionChanged;

            
            SourcesPanel.Children.Clear();

            foreach (Source source in sources)
            {
                SourcesPanel.Children.Add(new SourceControl(source, sources));
            }
        }

        private void Sources_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Source source;

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    source = (Source)e.NewItems[0];
                    SourcesPanel.Children.Insert(e.NewStartingIndex, new SourceControl(source, Sources));
                    break;
                case NotifyCollectionChangedAction.Remove:
                    SourcesPanel.Children.RemoveAt(e.OldStartingIndex);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    SourcesPanel.Children.Clear();
                    break;  
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Source source = new Source();
            Sources.Add(source);
        }
    }
}
