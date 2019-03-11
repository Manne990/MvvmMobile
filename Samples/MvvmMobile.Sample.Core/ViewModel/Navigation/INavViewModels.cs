using MvvmMobile.Core.Common;
using MvvmMobile.Core.ViewModel;

namespace MvvmMobile.Sample.Core.ViewModel.Navigation
{
    public interface INavBaseViewModel : IBaseViewModel
    {
        RelayCommand NextViewCommand { get; }
        RelayCommand NextSubViewCommand { get; }
        RelayCommand PrevViewCommand { get; }
        RelayCommand BackCommand { get; }
        RelayCommand HomeCommand { get; }
    }

    public interface INav1ViewModel : INavBaseViewModel
    {
    }

    public interface INav1AViewModel : INavBaseViewModel
    {
    }

    public interface INav1BViewModel : INavBaseViewModel
    {
    }

    public interface INav1CViewModel : INavBaseViewModel
    {
    }

    public interface INav2ViewModel : INavBaseViewModel
    {
    }

    public interface INav2AViewModel : INavBaseViewModel
    {
    }

    public interface INav2BViewModel : INavBaseViewModel
    {
    }

    public interface INav2CViewModel : INavBaseViewModel
    {
    }

    public interface INav3ViewModel : INavBaseViewModel
    {
    }

    public interface INav3AViewModel : INavBaseViewModel
    {
    }

    public interface INav3BViewModel : INavBaseViewModel
    {
    }

    public interface INav3CViewModel : INavBaseViewModel
    {
    }
}