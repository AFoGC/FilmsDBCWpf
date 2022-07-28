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

namespace FilmsUCWpf.View
{
    /// <summary>
    /// Логика взаимодействия для SourceControl.xaml
    /// </summary>
    public partial class SourceControl : UserControl
    {
        private readonly Source source;
        public SourceControl(Source source)
        {
            InitializeComponent();
            this.source = source;
            source_name.Text = source.Name;
            source_url.Text = source.SourceUrl;
        }

        public void UpdateSource()
        {
            source.Name = source_name.Text;
            source.SourceUrl = source_url.Text;
        }
    }
}
