﻿using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace WpfApp.Windows
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


        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void ExitForm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ReleaseCapture();
            SendMessage(new WindowInteropHelper(this).Handle, 0x112, 0xf012, 0);
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_DontSave_Click(object sender, RoutedEventArgs e)
        {
            CloseProg = true;
            this.Close();
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            Save = true;
            CloseProg = true;
            this.Close();
        }
    }
}
