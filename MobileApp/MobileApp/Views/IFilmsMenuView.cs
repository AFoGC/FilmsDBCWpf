using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MobileApp.Views
{
    public interface IFilmsMenuView
    {
        IList<View> MenuControls { get; } 
    }
}
