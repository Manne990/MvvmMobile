using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using MvvmMobile.Droid.View;
using MvvmMobile.Sample.Core.ViewModel.Navigation;

namespace MvvmMobile.Sample.Droid.Fragments.Navigation
{
    public class Nav3AFragment : NavFragmentBase<INav3AViewModel>
    {
        public Nav3AFragment()
        {
            BackgroundColor = Color.Lavender;
            TitleText = "Sub View 3A";
        }
    }

    public class Nav3BFragment : NavFragmentBase<INav3BViewModel>
    {
        public Nav3BFragment()
        {
            BackgroundColor = Color.BlanchedAlmond;
            TitleText = "Sub View 3B";
        }
    }

    public class Nav3CFragment : NavFragmentBase<INav3CViewModel>
    {
        public Nav3CFragment()
        {
            BackgroundColor = Color.Azure;
            TitleText = "Sub View 3C";
        }
    }
}
