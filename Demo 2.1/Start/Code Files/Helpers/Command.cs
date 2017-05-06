using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GPSImageTag.Core.Helpers  
{
    public class Command : ICommand, INotifyPropertyChanged
    {
        public Command(Func<Task> ExecTask, Func<bool> CanExec)
        {
            _ExecTask = ExecTask;
            _CanExec = CanExec;
        }

        private Func<Task> _ExecTask;
        private Func<bool> _CanExec;

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, new EventArgs());
        }

        public void NotifyPropertyChanged([CallerMemberName] string Name = null)
        {
            if (Name != null && PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(Name));
        }

        public event EventHandler CanExecuteChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public bool CanExecute(object parameter)
        {
            return !IsExecuting && (_CanExec != null ? _CanExec() : true);
        }

        private bool _IsExecuting;

        public bool IsExecuting
        {
            get { return _IsExecuting; }
            private set
            {
                if (_IsExecuting == value)
                    return;
                _IsExecuting = value;
                NotifyPropertyChanged();
                RaiseCanExecuteChanged();
            }
        }


        public async void Execute(object parameter)
        {
            try
            {
                IsExecuting = true;
                await _ExecTask();

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                IsExecuting = false;
            }
        }

    }
}
