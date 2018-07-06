using MvvmMobile.Core.Common;
using MvvmMobile.Core.ViewModel;

namespace MvvmMobile.Sample.Core.ViewModel.Navigation
{
    public interface INav3ViewModel : IBaseViewModel
    {
        RelayCommand NextCommand { get; }
        RelayCommand BackCommand { get; }
        RelayCommand HomeCommand { get; }
    }
}