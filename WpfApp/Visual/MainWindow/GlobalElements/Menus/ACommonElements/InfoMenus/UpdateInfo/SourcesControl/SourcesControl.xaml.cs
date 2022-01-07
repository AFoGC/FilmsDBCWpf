﻿using System;
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

namespace WpfApp.Visual.MainWindow.GlobalElements.Menus.ACommonElements.InfoMenus.UpdateInfo.SourcesControl
{
    /// <summary>
    /// Логика взаимодействия для SourcesControl.xaml
    /// </summary>
    public partial class SourcesControl : UserControl
    {
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
        }

        public void Reinitialize(List<Source> exportSources)
        {
            this.exportSources = exportSources;
            wrapPanel_sources.Children.Clear();

            foreach (Source source in exportSources)
            {
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
                exportSources.Remove(exportSources[0]);
            }

            foreach (SourceControl sourceControl in wrapPanel_sources.Children)
            {
                exportSources.Add(sourceControl.Source);
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
