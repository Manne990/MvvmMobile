using System.Threading.Tasks;
using MvvmMobile.Core.Navigation;

namespace MvvmMobile.Sample.Core.Navigation
{
    public interface ICustomNavigation : INavigation
    {
        Task NavigateToRoot();
    }
}