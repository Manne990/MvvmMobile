﻿using MvvmMobile.Core.Common;
using MvvmMobile.Core.Navigation;
using MvvmMobile.Core.ViewModel;

namespace MvvmMobile.Sample.Core.ViewModel.Navigation
{
    public class Nav2ViewModel : NavBaseViewModel, INav2ViewModel
    {
        public Nav2ViewModel(INavigation navigation)
        {
            NextViewCommand = new RelayCommand(() => navigation.NavigateTo<INav3ViewModel>());
            NextSubViewCommand = new RelayCommand(() => navigation.NavigateToSubView<INav2AViewModel>());
            BackCommand = new RelayCommand(() => navigation.NavigateBack(behaviour: BackBehaviour.SkipFromLastSubView));
            PrevViewCommand = new RelayCommand(() => navigation.NavigateBack(behaviour: BackBehaviour.FullViewsOnly));
            HomeCommand = new RelayCommand(() => navigation.NavigateBack<INav1ViewModel>()); //INav1BViewModel
        }
    }

    public class Nav2AViewModel : Nav2ViewModel, INav2AViewModel
    {
        public Nav2AViewModel(INavigation navigation)
            : base(navigation)
        {
            NextSubViewCommand = new RelayCommand(() => navigation.NavigateToSubView<INav2BViewModel>());
        }
    }

    public class Nav2BViewModel : Nav2ViewModel, INav2BViewModel
    {
        public Nav2BViewModel(INavigation navigation)
            : base(navigation)
        {
            NextSubViewCommand = new RelayCommand(() => navigation.NavigateToSubView<INav2CViewModel>());
        }
    }

    public class Nav2CViewModel : Nav2ViewModel, INav2CViewModel
    {
        public Nav2CViewModel(INavigation navigation)
            : base(navigation)
        {
            NextSubViewCommand = new RelayCommand(() => navigation.NavigateTo<INav3ViewModel>());
        }
    }
}