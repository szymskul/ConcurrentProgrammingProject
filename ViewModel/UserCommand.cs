using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ViewModel
{
    internal class UserCommand : ICommand
    {
        private readonly Action execute;
        private readonly Func<bool> canExecute;

        public event EventHandler CanExecuteChanged;

        public UserCommand(Action execute, Func<bool> canExecute)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
        }

        public UserCommand(Action execute) : this(execute, null) { }
        public virtual void Execute(object obj)
        {
            this.execute();
        }

        public bool CanExecute(object obj)
        {
            if (this.canExecute == null)
            {
                return true;
            }
            if (obj == null)
            {
                return this.canExecute();
            }
            return this.canExecute();
        }

        public void RaiseCanExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}