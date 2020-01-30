using System;
using Foundation;
using UIKit;

namespace MvvmMobile.Sample.iOS.ViewController
{
    public class CustomNavigationController : UINavigationController, IUINavigationBarDelegate
    {
        public CustomNavigationController(Type navigationBarType, Type toolbarType) : base(navigationBarType, toolbarType) { }
        public CustomNavigationController() { }
        public CustomNavigationController(NSCoder coder) : base(coder) { }
        protected CustomNavigationController(NSObjectFlag t) : base(t) { }
        protected internal CustomNavigationController(IntPtr handle) : base(handle) { }
        public CustomNavigationController(string nibName, NSBundle bundle) : base(nibName, bundle) { }
        public CustomNavigationController(UIViewController rootViewController) : base(rootViewController) { }

        [Export("navigationBar:shouldPopItem:")]
        public bool ShouldPopItem(UINavigationBar navigationBar, UINavigationItem item)
        {
            return true;
        }
    }
}