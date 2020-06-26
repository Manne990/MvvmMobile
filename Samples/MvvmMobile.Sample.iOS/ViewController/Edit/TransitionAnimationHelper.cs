using System;
using CoreGraphics;
using MvvmMobile.iOS.Navigation;
using UIKit;

namespace MvvmMobile.Sample.iOS.ViewController.Edit
{
    public interface IViewControllerWithTransition
    {
        UIView GetViewForSnapshot(Type relatedViewControllerType, ViewControllerTransitioningAnimatorPresentationType transitionType);
        CGRect TransitionTargetRect();
    }

    public static class TransitionAnimationHelper
    {
        public static IViewControllerWithTransition ExtractViewController(UIViewController viewController)
        {
            if (viewController == null)
            {
                return null;
            }

            if (viewController is UINavigationController nav)
            {
                return ExtractViewController(nav);
            }

            if (viewController is UITabBarController tabbar)
            {
                return ExtractViewController(tabbar);
            }

            return viewController as IViewControllerWithTransition;
        }

        public static IViewControllerWithTransition ExtractViewController(UINavigationController navigationController)
        {
            return navigationController?.TopViewController as IViewControllerWithTransition;
        }

        public static IViewControllerWithTransition ExtractViewController(UITabBarController tabBarController)
        {
            var viewController = tabBarController?.SelectedViewController;

            if (viewController == null)
            {
                return null;
            }

            if (viewController is UINavigationController nav)
            {
                return ExtractViewController(nav);
            }

            return viewController as IViewControllerWithTransition;
        }
    }
}