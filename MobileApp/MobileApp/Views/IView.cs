using MobileApp.PresenterInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Views
{
    public interface IView
    {
        void SetVisualDefault();
        void SetVisualSelected();
        void SetVisualFinded();
        void SelfRemove();
        double Height { get; }
    }
}
