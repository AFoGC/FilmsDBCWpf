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
using TL_Objects.CellDataClasses;

namespace FilmsDBCWpf.Visual.MainWindow.GlobalElements.Menus.ACommonElements.InfoMenus.UpdateInfo.SourcesControl
{
    /// <summary>
    /// Логика взаимодействия для SourcesControl.xaml
    /// </summary>
    public partial class SourcesControl : UserControl
    {
        private List<Source> sources = null;
        public List<Source> Sources
        {
            get { return sources; }
        }
        private List<Source> exportSources = null;
        public List<Source> ExportSources
        {
            get { return exportSources; }
        }

        private Canvas parentControl;
        public SourcesControl(UpdateFormVisualizer visualizer)
        {
            InitializeComponent();
            parentControl = visualizer.ParentControl;
            Canvas.SetLeft(this, 940);

            sources = new List<Source>();
        }

        public void Reinitialize(List<Source> exportSources)
        {
            this.exportSources = exportSources;
            wrapPanel_sources.Children.Clear();
            while (sources.Count != 0)
            {
                sources.Remove(this.sources[0]);
            }

            foreach (Source source in exportSources)
            {
                sources.Add(source);
                addElement(source);
            }
        }

        private void addElement(Source source)
        {
            wrapPanel_sources.Children.Add(new SourceControl(source, this));
        }

        public void button_update_Click(object sender, EventArgs e)
        {
            while (exportSources.Count != 0)
            {
                exportSources.Remove(sources[0]);
            }

            foreach (SourceControl sourceControl in wrapPanel_sources.Children)
            {
                exportSources.Add(sourceControl.Source);
            }
        }

        private void button_addElement_Click(object sender, EventArgs e)
        {
            addElement(new Source());
        }

        public WrapPanel FlowLayoutPanel_sources
        {
            get { return wrapPanel_sources; }
        }
    }
}
