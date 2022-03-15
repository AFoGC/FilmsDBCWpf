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
using TL_Objects.CellDataClasses;

namespace InfoMenusWpf.SourcesInfo
{
    /// <summary>
    /// Логика взаимодействия для SourceControl.xaml
    /// </summary>
    public partial class SourceControl : UserControl
    {
		private SourcesControl sourcesControl;
		private Source source = null;
		public SourceControl(Source source, SourcesControl sourcesControl)
		{
			InitializeComponent();

			this.source = source;
			this.textBox_name.Text = source.Name;
			this.textBox_url.Text = source.SourceUrl;
			this.sourcesControl = sourcesControl;
		}

		public Source Source
		{
			get
			{
				source.Name = textBox_name.Text;
				source.SourceUrl = textBox_url.Text;
				return source;
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			sourcesControl.FlowLayoutPanel_sources.Children.Remove(this);
			sourcesControl.FlowLayoutPanel_sources.Children.Insert(0, this);
		}
	}
}
