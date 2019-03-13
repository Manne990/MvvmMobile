using Cirrious.FluentLayouts.Touch;
using MvvmMobile.iOS.View;
using MvvmMobile.Sample.Core.ViewModel.Navigation;
using UIKit;

namespace MvvmMobile.Sample.iOS.ViewController.Navigation
{
    public class Nav1ASubViewController : NavBaseSubViewController<INav1AViewModel>
    {
        public Nav1ASubViewController()
        {
            BackgroundColor = UIColor.FromRGB(0xF0, 0xF8, 0xFF); //AliceBlue;
            TitleText = "Sub View 1A";
        }
    }

    public class Nav1BSubViewController : NavBaseSubViewController<INav1BViewModel>
    {
        public Nav1BSubViewController()
        {
            BackgroundColor = UIColor.FromRGB(0xFA, 0xEB, 0xD7); //AntiqueWhite;
            TitleText = "Sub View 1B";
        }
    }

    public class Nav1CSubViewController : NavBaseSubViewController<INav1CViewModel>
    {
        public Nav1CSubViewController()
        {
            BackgroundColor = UIColor.FromRGB(0xFF, 0xF8, 0xF8);
            TitleText = "Sub View 1C";
        }
    }
}