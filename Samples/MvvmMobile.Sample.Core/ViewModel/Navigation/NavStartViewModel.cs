using MvvmMobile.Core.Common;
using MvvmMobile.Core.Navigation;
using MvvmMobile.Core.ViewModel;

namespace MvvmMobile.Sample.Core.ViewModel.Navigation
{
    public class NavStartViewModel : BaseViewModel, INavStartViewModel
    {
        public NavStartViewModel(INavigation navigation)
        {
            StartCommand = new RelayCommand(() =>
            {
                navigation.NavigateTo<INav1ViewModel>();
            });

            DoneCommand = new RelayCommand(() => 
            {
                navigation.NavigateBack();
            });
        }

        public RelayCommand StartCommand { get; }
        public RelayCommand DoneCommand { get; }
    }
}