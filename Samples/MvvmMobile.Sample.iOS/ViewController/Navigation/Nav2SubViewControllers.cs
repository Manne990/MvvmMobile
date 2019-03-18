using MvvmMobile.Sample.Core.ViewModel.Navigation;
using UIKit;

namespace MvvmMobile.Sample.iOS.ViewController.Navigation
{
    public class Nav2ASubViewController : NavSubViewControllerBase<INav2AViewModel>
    {
        public Nav2ASubViewController()
        {
            BackgroundColor = UIColor.FromRGB(0xFF, 0xF0, 0xF5); // LavenderBlush;
            TitleText = "Sub View 2A";
        }
    }

    public class Nav2BSubViewController : NavSubViewControllerBase<INav2BViewModel>
    {
        public Nav2BSubViewController()
        {
            BackgroundColor = UIColor.FromRGB(0xFF, 0xEF, 0xD5); // PapayaWhip;
            TitleText = "Sub View 2B";
        }
    }

    public class Nav2CSubViewController : NavSubViewControllerBase<INav2CViewModel>
    {
        public Nav2CSubViewController()
        {
            BackgroundColor = UIColor.FromRGB(0xF5, 0xF5, 0xF5); // WhiteSmoke
            TitleText = "Sub View 2C";
        }
    }
}