using MvvmMobile.Core.Common;
using MvvmMobile.Core.ViewModel;

namespace MvvmMobile.Sample.Core.ViewModel
{
    public interface IStartViewModel : IBaseViewModel
    {
        string Name { get; }

        RelayCommand MoveNextCommand { get; }
    }
}