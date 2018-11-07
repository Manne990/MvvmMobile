using MvvmMobile.Core.Common;
using MvvmMobile.Core.Navigation;
using MvvmMobile.Core.ViewModel;

namespace MvvmMobile.Sample.Core.ViewModel.Navigation
{
    public class Nav1ViewModel : BaseViewModel, INav1ViewModel
    {
        public Nav1ViewModel(INavigation navigation)
        {
            NextCommand = new RelayCommand(() => 
            {
                navigation.NavigateTo<INav2ViewModel>();
            });

            BackCommand = new RelayCommand(() => 
            {
                navigation.NavigateBack();
            });

            HomeCommand = new RelayCommand(() => 
            {
                navigation.NavigateBack<INavStartViewModel>();
            });
        }

        public RelayCommand NextCommand { get; }
        public RelayCommand BackCommand { get; }
        public RelayCommand HomeCommand { get; }
    }
}