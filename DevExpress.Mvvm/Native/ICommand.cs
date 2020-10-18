using DevExpress.Mvvm.Native;
using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DevExpress.Mvvm.Native {
    public interface IDelegateCommand : ICommand {
        void RaiseCanExecuteChanged();
    }
    public interface IAsyncCommand : IDelegateCommand {
        bool IsExecuting { get; }
        [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use the IsCancellationRequested property instead.")]
        bool ShouldCancel { get; }
        CancellationTokenSource CancellationTokenSource { get; }
        bool IsCancellationRequested { get; }
        ICommand CancelCommand { get; }
        void Wait(TimeSpan timeout);
        Task ExecuteAsync(object parameter);
    }
}
namespace DevExpress.Mvvm {
    public static class IAsyncCommandExtensions {
        public static void Wait(this IAsyncCommand service) {
            VerifyService(service);
            service.Wait(TimeSpan.FromMilliseconds(-1));
        }
        static void VerifyService(IAsyncCommand service) {
            if(service == null) throw new ArgumentNullException("service");
        }
    }
}