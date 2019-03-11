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
                navigation.NavigateToSubView<INav1AViewModel>();
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