﻿using System;
using MvvmMobile.Core.Common;
using MvvmMobile.Core.Navigation;

namespace MvvmMobile.Sample.Core.ViewModel.Navigation
{
    public class Nav1ViewModel : NavBaseViewModel, INav1ViewModel
    {
        public Nav1ViewModel(INavigation navigation)
        {
            NextViewCommand = new RelayCommand(() => navigation.NavigateTo<INav2ViewModel>());
            NextSubViewCommand = new RelayCommand(() => navigation.NavigateToSubView<INav1AViewModel>());
            BackCommand = new RelayCommand(() => navigation.NavigateBack());
            //PrevViewCommand = new RelayCommand(() => navigation.NavigateBackToLastFullView());
            HomeCommand = new RelayCommand(() => navigation.NavigateBack<INav1ViewModel>());
        }
    }

    public class Nav1AViewModel : Nav1ViewModel, INav1AViewModel
    {
        public Nav1AViewModel(INavigation navigation)
            : base(navigation)
        {
            NextSubViewCommand = new RelayCommand(() => navigation.NavigateToSubView<INav1BViewModel>());
            //PrevViewCommand = new RelayCommand(() => navigation.NavigateBackToLastFullView());
        }
    }

    public class Nav1BViewModel : Nav1ViewModel, INav1BViewModel
    {
        public Nav1BViewModel(INavigation navigation)
            : base(navigation)
        {
            NextSubViewCommand = new RelayCommand(() => navigation.NavigateToSubView<INav1CViewModel>());
            //PrevViewCommand = new RelayCommand(() => navigation.NavigateBackToLastFullView());
        }
    }

    public class Nav1CViewModel : Nav1ViewModel, INav1CViewModel
    {
        public Nav1CViewModel(INavigation navigation)
            : base(navigation)
        {
            NextSubViewCommand = new RelayCommand(() => navigation.NavigateTo<INav2ViewModel>());
            //PrevViewCommand = new RelayCommand(() => navigation.NavigateBackToLastFullView());
        }
    }
}