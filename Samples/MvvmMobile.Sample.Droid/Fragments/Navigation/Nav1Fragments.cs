using System;
using Android.Graphics;
using MvvmMobile.Sample.Core.ViewModel.Navigation;

namespace MvvmMobile.Sample.Droid.Fragments.Navigation
{
    public class Nav1AFragment : NavFragmentBase<INav1AViewModel>
    {
        public Nav1AFragment()
        {
            BackgroundColor = Color.AliceBlue;
            TitleText = "Sub View 1A";
        }
    }

    public class Nav1BFragment : NavFragmentBase<INav1BViewModel>
    {
        public Nav1BFragment()
        {
            BackgroundColor = Color.AntiqueWhite;
            TitleText = "Sub View 1B";
        }
    }

    public class Nav1CFragment : NavFragmentBase<INav1CViewModel>
    {
        public Nav1CFragment()
        {
            BackgroundColor = new Color(0xFF, 0xF8, 0xF8);
            TitleText = "Sub View 1C";
        }
    }
}
