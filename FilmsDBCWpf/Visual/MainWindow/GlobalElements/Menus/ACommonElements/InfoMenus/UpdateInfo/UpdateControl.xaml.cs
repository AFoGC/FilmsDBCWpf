using FilmsDBCWpf.Visual.MainWindow.GlobalElements.Menus.ACommonElements.ControlsInterface;
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

namespace FilmsDBCWpf.Visual.MainWindow.GlobalElements.Menus.ACommonElements.InfoMenus.UpdateInfo
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

		public void Reinitialize(IControls icontrol)
		{
			canvas_main.Children.Clear();
			canvas_main.Children.Add(icontrol.ToUpdateControl());
		}

		private void button_update_Click(object sender, EventArgs e)
		{
			IUpdateControl control = (IUpdateControl)canvas_main.Children[0];
			control.UpdateElement();

			if (visualizer.SourcesVisualizer.IsOpen)
			{
				visualizer.SourcesVisualizer.SourcesControl.button_update_Click(sender, e);
			}

			//MainInfo.MainForm.InfoUnsaved = true;
		}

		private void pictureBox_Close_Click(object sender, EventArgs e)
		{
			Canvas canvas = (Canvas)this.Parent;
			canvas.Children.Remove(this);
		}
	}
}
