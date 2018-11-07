using MvvmMobile.Core.Common;
using MvvmMobile.Core.Navigation;
using MvvmMobile.Core.ViewModel;
using MvvmMobile.Sample.Core.ViewModel.Motorcycles;

namespace MvvmMobile.Sample.Core.ViewModel.Navigation
{
    public class Nav2ViewModel : BaseViewModel, INav2ViewModel
    {
        public Nav2ViewModel(INavigation navigation)
        {
            NextCommand = new RelayCommand(() => 
            {
                navigation.NavigateTo<INav3ViewModel>();
            });

            BackCommand = new RelayCommand(() => 
            {
                navigation.NavigateBack();
            });

            HomeCommand = new RelayCommand(() => 
            {
                navigation.NavigateBack<IStartViewModel>();
            });
        }

        public RelayCommand NextCommand { get; }
        public RelayCommand BackCommand { get; }
        public RelayCommand HomeCommand { get; }
    }
}