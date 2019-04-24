using System;
using System.Collections.Generic;
using UIKit;

namespace MvvmMobile.iOS.View
{
    public interface ISubViewContainerController : IViewControllerBase
    {
        UIView SubViewContainerView { get; }
        NSLayoutConstraint[] SubViewOriginalConstraints { get; set; }
        Stack<UIViewController> SubViewNavigationStack { get; }
    }
}
