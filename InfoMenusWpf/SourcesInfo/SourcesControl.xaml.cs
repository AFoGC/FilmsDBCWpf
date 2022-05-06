using InfoMenusWpf.UpdateInfo;
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
using TablesLibrary.Interpreter.TableCell;
using TL_Objects.CellDataClasses;

namespace InfoMenusWpf.SourcesInfo
{
    /// <summary>
    /// Логика взаимодействия для SourcesControl.xaml
    /// </summary>
    public partial class SourcesControl : UserControl
    {
        public TLCollection<Source> ExportSources { get; private set; }

        private Canvas parentControl;
        public SourcesControl(UpdateFormVisualizer visualizer)
        {
            InitializeComponent();
            parentControl = visualizer.ParentControl;
            Canvas.SetLeft(this, 940);
        }

        public void Reinitialize(TLCollection<Source> exportSources)
        {
            this.ExportSources = exportSources;
            wrapPanel_sources.Children.Clear();

            foreach (Source source in exportSources)
            {
                addElement(source);
            }
        }

        private void addElement(Source source)
        {
            if (wrapPanel_sources.Children.Count < 3)
            {
                wrapPanel_sources.Children.Add(new SourceControl(source, this));
            }
        }

        public void Update()
        {
            while (ExportSources.Count != 0)
            {
                ExportSources.Remove(ExportSources[0]);
            }

            foreach (SourceControl sourceControl in wrapPanel_sources.Children)
            {
                ExportSources.Add(sourceControl.Source);
            }
        }

        public WrapPanel FlowLayoutPanel_sources
        {
            get { return wrapPanel_sources; }
        }

        private void button_addElement_Click(object sender, RoutedEventArgs e)
        {
            addElement(new Source());
        }
    }
}
