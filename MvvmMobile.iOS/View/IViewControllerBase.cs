using System;
using MvvmMobile.Core.ViewModel;
using UIKit;

namespace MvvmMobile.iOS.View
{
    internal interface IViewControllerBase
    {
		bool AsModal { get; set; }

        void SetPayload(IPayload payload);
        void SetCallback(Action<Guid> callbackAction);
        UIViewController AsViewController();
    }
}