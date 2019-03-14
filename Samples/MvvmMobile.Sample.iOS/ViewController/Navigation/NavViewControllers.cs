using System;
using MvvmMobile.Sample.Core.ViewModel.Navigation;
using UIKit;

namespace MvvmMobile.Sample.iOS.ViewController.Navigation
{
    public class Nav1ViewController : NavViewControllerBase<INav1ViewModel>
    {
        public Nav1ViewController()
        {
            Title = "Nav View 1";
            BackgroundColor = UIColor.FromRGB(0xFF, 0xFA, 0xCD); // LemonChiffon
        }
    }

    public class Nav2ViewController : NavViewControllerBase<INav2ViewModel>
    {
        public Nav2ViewController()
        {
            Title = "Nav View 2";
            BackgroundColor = UIColor.FromRGB(0xAD, 0xD8, 0xE6); // LightBlue
        }
    }

    public class Nav3ViewController : NavViewControllerBase<INav3ViewModel>
    {
        public Nav3ViewController()
        {
            Title = "Nav View 3";
            BackgroundColor = UIColor.FromRGB(0xF0, 0x80, 0x80); // LightCoral
        }
    }
}
