using System;
using Foundation;
using MvvmMobile.iOS.Navigation;
using MvvmMobile.Sample.iOS.ViewController.Edit;
using UIKit;

namespace MvvmMobile.Sample.iOS.Navigation
{
    public class SampleNavigationController : UINavigationController
    {
        public SampleNavigationController(Type navigationBarType, Type toolbarType) : base(navigationBarType, toolbarType) { }
        public SampleNavigationController() { }
        public SampleNavigationController(NSCoder coder) : base(coder) { }
        protected SampleNavigationController(NSObjectFlag t) : base(t) { }
        protected internal SampleNavigationController(IntPtr handle) : base(handle) { }
        public SampleNavigationController(string nibName, NSBundle bundle) : base(nibName, bundle) { }
        public SampleNavigationController(UIViewController rootViewController) : base(rootViewController) { }

        //public UIView GetViewForSnapshot(Type relatedViewControllerType, ViewControllerTransitioningAnimatorPresentationType transitionType)
        //{
        //    return VisibleViewController is IViewControllerWithTransition vc ? vc.GetViewForSnapshot(relatedViewControllerType, transitionType) : View;
        //}
    }
}