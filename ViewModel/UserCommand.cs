using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel
{
    internal class UserCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private readonly Action execute;
        private readonly Func<bool> canExecute;

        public UserCommand(Action execute, Func<bool> canExecute)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
        }

        public UserCommand(Action execute) : this(execute, null) { }

        public bool CanExecute(object? parameter)
        {
            if (this.canExecute == null)
            {
                return true;
            }
            if (parameter == null)
            {
                return this.canExecute();
            }
            return this.canExecute();
        }

        public virtual void Execute(object? parameter)
        {
            this.execute();
        }

        public void RaiseCanExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
