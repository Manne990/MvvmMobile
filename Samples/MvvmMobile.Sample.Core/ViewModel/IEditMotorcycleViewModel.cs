using MvvmMobile.Core.Common;
using MvvmMobile.Core.ViewModel;
using MvvmMobile.Sample.Core.Model;

namespace MvvmMobile.Sample.Core.ViewModel
{
    public interface IEditMotorcycleViewModel : IPayloadViewModel
    {
        IMotorcycle Motorcycle { get; set; }

        RelayCommand CancelCommand { get; }
        RelayCommand SaveMotorcycleCommand { get; }
    }
}