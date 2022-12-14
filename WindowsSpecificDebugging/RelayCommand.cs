using System;
using System.Windows.Input;

namespace WindowsSpecificDebugging
{
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Predicate<T>? _canExecute = null;

        public RelayCommand(Action<T> execute, Predicate<T>? canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        public void Execute(object? parameter)
        {
            if (parameter is T converted)
            {
                _execute(converted);
                return;
            }

            _execute(default!);
        }

        public bool CanExecute(object? parameter)
        {
            if (parameter is T converted)
            {
                return _canExecute == null || _canExecute(converted);
            }

            return _canExecute == null || _canExecute(default!);
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
