using MvvmMobile.Core.Common;
using MvvmMobile.Core.Navigation;
using MvvmMobile.Core.ViewModel;

namespace MvvmMobile.Sample.Core.ViewModel.Navigation
{
    public class Nav3ViewModel : NavBaseViewModel, INav3ViewModel
    {
        public Nav3ViewModel(INavigation navigation)
        {
            NextViewCommand = new RelayCommand(() => navigation.NavigateTo<INav1ViewModel>());
            NextSubViewCommand = new RelayCommand(() => navigation.NavigateToSubView<INav3AViewModel>());
            BackCommand = new RelayCommand(() => navigation.NavigateBack());
            //PrevViewCommand = new RelayCommand(() => navigation.NavigateBackToLastFullView());
            HomeCommand = new RelayCommand(() => navigation.NavigateBack<INav1ViewModel>());
        }
    }

    public class Nav3AViewModel : Nav3ViewModel, INav3AViewModel
    {
        public Nav3AViewModel(INavigation navigation)
            : base(navigation)
        {
            NextSubViewCommand = new RelayCommand(() => navigation.NavigateToSubView<INav3BViewModel>());
            //PrevViewCommand = new RelayCommand(() => navigation.NavigateBackToLastFullView());
        }
    }

    public class Nav3BViewModel : Nav3ViewModel, INav3BViewModel
    {
        public Nav3BViewModel(INavigation navigation)
            : base(navigation)
        {
            NextSubViewCommand = new RelayCommand(() => navigation.NavigateToSubView<INav3CViewModel>());
            //PrevViewCommand = new RelayCommand(() => navigation.NavigateBackToLastFullView());
        }
    }

    public class Nav3CViewModel : Nav3ViewModel, INav3CViewModel
    {
        public Nav3CViewModel(INavigation navigation)
            : base(navigation)
        {
            NextSubViewCommand = new RelayCommand(() => navigation.NavigateTo<INav1ViewModel>());
            NextSubViewCommand.CanExecute(false);
            //PrevViewCommand = new RelayCommand(() => navigation.NavigateBackToLastFullView());
        }
    }
}