﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp.Views.Interfaces
{
    public interface IChangeMenu
    {
        void ChangeMenu(SelectedMenu menu);
    }

    public enum SelectedMenu
    {
        FilmsMenu,
        BooksMenu,
        SettingMenu
    }
}
