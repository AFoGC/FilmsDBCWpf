using System.Windows.Input;
using WpfApp.Commands;
using WpfApp.Models;

namespace WpfApp.ViewModels
{
    public class SignInViewModel : BaseViewModel
    {
        private string _username;
        private string _password;
        private ICommand _signInCommand;

        public SignInModel Model { get; private set; }

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public ICommand SignInCommand
        {
            get
            {
                return _signInCommand ??
                (_signInCommand = new Command(obj =>
                {
                    Model.SignIn(Username, Password);
                }));
            }
        }

    }
}
