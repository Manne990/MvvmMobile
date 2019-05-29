using System;
using MvvmMobile.Core.ViewModel;

namespace MvvmMobile.Sample.Core.ViewModel
{
    public interface ITabBarViewModel : IBaseViewModel
    {
    
    }

    public class TabBarViewModel : BaseViewModel, ITabBarViewModel
    {
        public TabBarViewModel()
        {
        }
    }
}