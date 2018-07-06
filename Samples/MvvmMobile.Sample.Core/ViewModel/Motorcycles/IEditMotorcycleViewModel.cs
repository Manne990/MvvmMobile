using MvvmMobile.Core.Common;
using MvvmMobile.Core.ViewModel;
using MvvmMobile.Sample.Core.Model;

namespace MvvmMobile.Sample.Core.ViewModel.Motorcycles
{
    public interface IEditMotorcycleViewModel : IBaseViewModel
    {
        IMotorcycle Motorcycle { get; set; }

        RelayCommand CancelCommand { get; }
        RelayCommand SaveMotorcycleCommand { get; }
    }
}