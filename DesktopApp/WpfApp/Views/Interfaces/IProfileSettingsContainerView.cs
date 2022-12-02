using System.Collections;

namespace WpfApp.Views.Interfaces
{
    public interface IProfileSettingsContainerView
    {
        IList ProfileControls { get; }
        string AddProfileText { get; set; }
    }
}
