using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp.Visual.HelpWindows.ExitWindow
{
    /// <summary>
    /// Логика взаимодействия для ExitWindow.xaml
    /// </summary>
    public partial class ExitWindow : Window
    {
        public ExitWindow()
        {
            InitializeComponent();
            Save = false;
            CloseProg = false;
        }

        public Boolean Save { get; private set; }
        public Boolean CloseProg { get; private set; }

        

        private void button_Save_Click(object sender, EventArgs e)
        {
            Save = true;
            CloseProg = true;
            //this.DialogResult = DialogResult.HasValue;
            this.Close();
        }

        private void button_DontSave_Click(object sender, EventArgs e)
        {
            CloseProg = true;
            //this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button_close_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.OK;
            this.Close();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void ExitForm_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Process.GetCurrentProcess().MainWindowHandle, 0x112, 0xf012, 0);
        }
    }
}
