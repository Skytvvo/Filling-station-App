using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FogOilAssistant.Components.Models.MainWindow
{
    public class WMICommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }


        private Action _action;

        public bool CanExecute(object parm)
        {
            return true;
        }

        public void Execute(object parm)
        {
            _action();
        }

        public WMICommand(Action execute)
        {
            _action = execute;
        }
    }
}
