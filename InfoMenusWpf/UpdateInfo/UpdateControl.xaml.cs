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

namespace InfoMenusWpf.UpdateInfo
{
    /// <summary>
    /// Логика взаимодействия для UpdateControl.xaml
    /// </summary>
    public partial class UpdateControl : UserControl
    {
		UpdateFormVisualizer visualizer;
		public UpdateControl(UpdateFormVisualizer visualizer)
		{
			InitializeComponent();
			this.visualizer = visualizer;
		}

		public void Reinitialize(Control control)
		{
			canvas_main.Children.Clear();
			canvas_main.Children.Add(control);
		}

		private void pictureBox_Close_Click(object sender, EventArgs e)
		{
			Canvas canvas = (Canvas)this.Parent;
			canvas.Children.Remove(this);
		}

		private void btn_exit_Click(object sender, RoutedEventArgs e)
		{
			visualizer.HideUpdateControl();
		}

		private void btn_update_Click(object sender, RoutedEventArgs e)
		{
			Update();
		}

		public void Update()
        {
			if (canvas_main.Children.Count != 0)
			{
				IUpdateControl control = (IUpdateControl)canvas_main.Children[0];
				control.UpdateElement();

				if (visualizer.SourcesVisualizer.IsOpen)
				{
					visualizer.SourcesVisualizer.SourcesControl.Update();
				}
			}
			else
			{
				visualizer.HideUpdateControl();
			}
		}
	}
}
