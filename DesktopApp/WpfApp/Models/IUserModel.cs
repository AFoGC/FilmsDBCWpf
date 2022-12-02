using System;

namespace WpfApp.Models
{
    public interface IUserModel
    {
        event EventHandler UserChanged;
        SettingsModel Settings { get; }
    }
}
