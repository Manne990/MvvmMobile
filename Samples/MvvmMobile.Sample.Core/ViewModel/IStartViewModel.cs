using System.Collections.ObjectModel;
using MvvmMobile.Core.Common;
using MvvmMobile.Core.ViewModel;
using MvvmMobile.Sample.Core.Model;

namespace MvvmMobile.Sample.Core.ViewModel
{
    public interface IStartViewModel : IBaseViewModel
    {
        ObservableCollection<IMotorcycle> Motorcycles { get; }

        RelayCommand AddMotorcycleCommand { get; }
        RelayCommand EditMotorcycleCommand { get; }
        RelayCommand DeleteMotorcycleCommand { get; }
    }
}