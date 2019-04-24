using System;
using MvvmMobile.Core.Common;
using MvvmMobile.Core.ViewModel;
using UIKit;

namespace MvvmMobile.iOS.View
{
    public interface IViewControllerBase : IPlatformView
    {
		bool AsModal { get; set; }
        bool SubViewHasNavBar { get; set; }
        bool IsSubView { get; set; }

        void SetPayload(IPayload payload);
        void SetCallback(Action<Guid> callbackAction);
        UIViewController AsViewController();
    }
}