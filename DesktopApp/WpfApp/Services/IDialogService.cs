﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Services
{
    public interface IDialogService
    {
        string FileName { get; }
        bool OpenFileDialog();
    }
}
