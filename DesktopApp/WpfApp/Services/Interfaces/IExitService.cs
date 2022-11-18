namespace WpfApp.Services.Interfaces
{
    public interface IExitService
    {
        bool? ShowDialog();
        bool Save { get; }
        bool Close { get; }
    }
}
