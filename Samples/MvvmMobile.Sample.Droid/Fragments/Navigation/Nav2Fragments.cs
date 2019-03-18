using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using MvvmMobile.Droid.View;
using MvvmMobile.Sample.Core.ViewModel.Navigation;

namespace MvvmMobile.Sample.Droid.Fragments.Navigation
{
    public class Nav2AFragment : NavFragmentBase<INav2AViewModel>
    {
        public Nav2AFragment()
        {
            BackgroundColor = Color.LavenderBlush;
            TitleText = "Sub View 2A";
        }
    }

    public class Nav2BFragment : NavFragmentBase<INav2BViewModel>
    {
        public Nav2BFragment()
        {
            BackgroundColor = Color.PapayaWhip;
            TitleText = "Sub View 2B";
        }
    }

    public class Nav2CFragment : NavFragmentBase<INav2CViewModel>
    {
        public Nav2CFragment()
        {
            BackgroundColor = Color.WhiteSmoke;
            TitleText = "Sub View 2C";
        }
    }
}