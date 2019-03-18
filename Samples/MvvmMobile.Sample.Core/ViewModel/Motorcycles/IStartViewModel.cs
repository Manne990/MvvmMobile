using System.Collections.ObjectModel;
using MvvmMobile.Core.Common;
using MvvmMobile.Core.ViewModel;
using MvvmMobile.Sample.Core.Model;

namespace MvvmMobile.Sample.Core.ViewModel.Motorcycles
{
    public interface IStartViewModel : IBaseViewModel
    {
        ObservableCollection<IMotorcycle> Motorcycles { get; }

        RelayCommand AddMotorcycleCommand { get; }
        RelayCommand<IMotorcycle> EditMotorcycleCommand { get; }
        RelayCommand<IMotorcycle> DeleteMotorcycleCommand { get; }
        RelayCommand StartNavigationDemoCommand { get; }
    }
}