using MvvmMobile.Core.Common;
using MvvmMobile.Core.Navigation;
using MvvmMobile.Core.ViewModel;

namespace MvvmMobile.Sample.Core.ViewModel.Navigation
{
    public class NavBaseViewModel : BaseViewModel, INavBaseViewModel
    {
        public RelayCommand NextViewCommand { get; protected set; }
        public RelayCommand NextSubViewCommand { get; protected set; }
        public RelayCommand BackCommand { get; protected set; }
        public RelayCommand PrevViewCommand { get; protected set; }
        public RelayCommand HomeCommand { get; protected set; }
    }
}