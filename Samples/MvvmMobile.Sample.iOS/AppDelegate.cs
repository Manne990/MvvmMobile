using Foundation;
using MvvmMobile.Sample.Core.Navigation;
using MvvmMobile.Sample.Core.ViewModel.Motorcycles;
using MvvmMobile.Sample.Core.ViewModel.Navigation;
using MvvmMobile.Sample.iOS.Navigation;
using MvvmMobile.Sample.iOS.View;
using MvvmMobile.Sample.iOS.ViewController.Navigation;
using MvvmMobile.Sample.iOS.ViewController.Start;
using UIKit;

namespace MvvmMobile.Sample.iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        public override UIWindow Window
        {
            get;
            set;
        }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            // Init IoC
            var builder = Core.Bootstrapper.Init();

            MvvmMobile.iOS.Bootstrapper.SetupIoC(builder);

            builder.RegisterSingleton<ICustomNavigation, CustomNavigation>();

            builder.Build();

            // Init MvvmMobile
            MvvmMobile.iOS.Bootstrapper.Init();

            MvvmMobile.iOS.Bootstrapper.AddViewMapping<IStartViewModel, StartContainerViewController>();
            MvvmMobile.iOS.Bootstrapper.AddViewMapping<IEditMotorcycleViewModel, EditMotorcycleViewController>();

            MvvmMobile.iOS.Bootstrapper.AddViewMapping<INav1ViewModel, Nav1ViewController>();
            MvvmMobile.iOS.Bootstrapper.AddViewMapping<INav1AViewModel, Nav1ASubViewController>();
            MvvmMobile.iOS.Bootstrapper.AddViewMapping<INav1BViewModel, Nav1BSubViewController>();
            MvvmMobile.iOS.Bootstrapper.AddViewMapping<INav1CViewModel, Nav1CSubViewController>();
            MvvmMobile.iOS.Bootstrapper.AddViewMapping<INav2ViewModel, Nav2ViewController>();
            MvvmMobile.iOS.Bootstrapper.AddViewMapping<INav2AViewModel, Nav2ASubViewController>();
            MvvmMobile.iOS.Bootstrapper.AddViewMapping<INav2BViewModel, Nav2BSubViewController>();
            MvvmMobile.iOS.Bootstrapper.AddViewMapping<INav2CViewModel, Nav2CSubViewController>();
            MvvmMobile.iOS.Bootstrapper.AddViewMapping<INav3ViewModel, Nav3ViewController>();
            MvvmMobile.iOS.Bootstrapper.AddViewMapping<INav3AViewModel, Nav3ASubViewController>();
            MvvmMobile.iOS.Bootstrapper.AddViewMapping<INav3BViewModel, Nav3BSubViewController>();
            MvvmMobile.iOS.Bootstrapper.AddViewMapping<INav3CViewModel, Nav3CSubViewController>();

            //var rootViewController = new ViewController.TabBarViewController();

            //Window = new UIWindow(UIScreen.MainScreen.Bounds)
            //{
            //    RootViewController = rootViewController
            //};

            //Window?.MakeKeyAndVisible();

            return true;
        }

        public override void OnResignActivation(UIApplication application)
        {
            // Invoked when the application is about to move from active to inactive state.
            // This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
            // or when the user quits the application and it begins the transition to the background state.
            // Games should use this method to pause the game.
        }

        public override void DidEnterBackground(UIApplication application)
        {
            // Use this method to release shared resources, save user data, invalidate timers and store the application state.
            // If your application supports background exection this method is called instead of WillTerminate when the user quits.
        }

        public override void WillEnterForeground(UIApplication application)
        {
            // Called as part of the transiton from background to active state.
            // Here you can undo many of the changes made on entering the background.
        }

        public override void OnActivated(UIApplication application)
        {
            // Restart any tasks that were paused (or not yet started) while the application was inactive. 
            // If the application was previously in the background, optionally refresh the user interface.
        }

        public override void WillTerminate(UIApplication application)
        {
            // Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
        }
    }
}