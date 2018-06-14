using MvvmMobile.Droid.Navigation;
using MvvmMobile.Sample.Core.Navigation;

namespace MvvmMobile.Sample.Droid.Navigation
{
    public class CustomNavigation : AppNavigation, ICustomNavigation
    {
        public void NavigateToRoot()
        {
            System.Diagnostics.Debug.WriteLine("NavigateToRoot");
        }
    }
}