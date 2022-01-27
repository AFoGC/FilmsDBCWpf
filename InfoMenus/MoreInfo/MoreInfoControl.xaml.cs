using InfoMenus.Interfaces;
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

namespace InfoMenus.MoreInfo
{
    /// <summary>
    /// Логика взаимодействия для MoreInfoControl.xaml
    /// </summary>
    public partial class MoreInfoControl : UserControl
    {
        private MoreInfoVisualizer visualizer;
        public MoreInfoControl(MoreInfoVisualizer visualizer)
        {
            InitializeComponent();
            this.visualizer = visualizer;
        }

        public void Reinitialize(ISimpleControl simpleControl)
        {
            canvas_main.Children.Clear();
            canvas_main.Children.Add(simpleControl.ToMoreInfo());
        }

        private void pictureBox_Close_Click(object sender, EventArgs e)
        {
            Canvas canvas = (Canvas)this.Parent;
            canvas.Children.Remove(this);
        }

        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            visualizer.HideMoreInfoControl();
        }
    }
}
