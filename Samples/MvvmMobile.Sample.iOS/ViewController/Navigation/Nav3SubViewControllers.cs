using MvvmMobile.Sample.Core.ViewModel.Navigation;
using UIKit;

namespace MvvmMobile.Sample.iOS.ViewController.Navigation
{
    public class Nav3ASubViewController : NavSubViewControllerBase<INav3AViewModel>
    {
        public Nav3ASubViewController()
        {
            BackgroundColor = UIColor.FromRGB(0xE6, 0xE6, 0xFA); // Lavender;
            TitleText = "Sub View 3A";
        }
    }

    public class Nav3BSubViewController : NavSubViewControllerBase<INav3BViewModel>
    {
        public Nav3BSubViewController()
        {
            BackgroundColor = UIColor.FromRGB(0xFF, 0xEB, 0xCD); // BlanchedAlmond;
            TitleText = "Sub View 3B";
        }
    }

    public class Nav3CSubViewController : NavSubViewControllerBase<INav3CViewModel>
    {
        public Nav3CSubViewController()
        {
            BackgroundColor = UIColor.FromRGB(0xF0, 0xFF, 0xFF); // Azure
            TitleText = "Sub View 3C";
        }
    }
}