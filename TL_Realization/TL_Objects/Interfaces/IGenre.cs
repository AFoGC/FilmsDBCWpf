using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TL_Objects.Interfaces
{
    public interface IGenre : INotifyPropertyChanged
    {
        string Name { get; }
    }
}
