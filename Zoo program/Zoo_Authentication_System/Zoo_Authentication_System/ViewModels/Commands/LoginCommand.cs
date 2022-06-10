using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Zoo_Authentication_System.ViewModels.Commands
{
    public class LoginCommand : ICommand
    {
        public BaseViewModel BaseViewModel { get; set; }

        public LoginCommand(BaseViewModel baseViewModel)
        {   
            this.BaseViewModel = baseViewModel;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true; // this method allows us to enable and disable UI element for a click
        }

        public void Execute(object? parameter)
        {
            this.BaseViewModel.login();
            
        }
    }
}
