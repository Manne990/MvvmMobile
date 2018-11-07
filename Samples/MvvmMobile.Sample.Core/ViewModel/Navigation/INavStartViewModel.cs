using MvvmMobile.Core.Common;
using MvvmMobile.Core.ViewModel;

namespace MvvmMobile.Sample.Core.ViewModel.Navigation
{
    public interface INavStartViewModel : IBaseViewModel
    {
        RelayCommand StartCommand { get; }
        RelayCommand DoneCommand { get; }
    }
}