namespace WpfApp.Services.Interfaces
{
    public interface IDialogService
    {
        string FileName { get; }
        bool OpenFileDialog();
    }
}
