using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Threading;

namespace WindowsSpecificDebugging
{
    public class DataContext : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected static Dispatcher RenderThreadDispatcher => Application.Current.Dispatcher;

        protected void SetPropertyAndNotifySubscribers<T>(ref T fieldToSet, T newValue, [CallerMemberName] string propertyName = "")
        {
            if (Equals(fieldToSet, newValue)) return;

            fieldToSet = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
