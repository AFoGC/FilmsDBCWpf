using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Views.Interfaces
{
    public enum StatusEnum
    {
        Normal,
        Saved,
        UnSaved
    }

    public interface IMainWindowView
    {
        void SetStatus(StatusEnum status);
    }
}
