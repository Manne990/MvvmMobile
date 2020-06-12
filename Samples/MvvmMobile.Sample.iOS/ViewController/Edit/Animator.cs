using MvvmMobile.iOS.Navigation;
using UIKit;

namespace MvvmMobile.Sample.iOS.ViewController.Edit
{
    public interface IViewControllerWithTransition
    {
        UIView GetViewForSnapshot();
    }

    public class Animator : UIViewControllerAnimatedTransitioning, ITransitionAnimator
    {
        private const double Duration = 2.0;

        private readonly ViewControllerTransitioningAnimatorPresentationType _type;
        private readonly IViewControllerWithTransition _sourceViewController;
        private IViewControllerWithTransition _destinationViewController;

        public Animator(ViewControllerTransitioningAnimatorPresentationType type, IViewControllerWithTransition sourceViewController)
        {
            _type = type;
            _sourceViewController = sourceViewController;
        }

        public override double TransitionDuration(IUIViewControllerContextTransitioning transitionContext)
        {
            return Duration;
        }

        public override void AnimateTransition(IUIViewControllerContextTransitioning transitionContext)
        {
            _destinationViewController = ExtractViewController(transitionContext.GetViewControllerForKey(UITransitionContext.ToViewControllerKey));
            if (_sourceViewController == null || _destinationViewController == null)
            {
                transitionContext.CompleteTransition(false);
                return;
            }

            var isPresenting = _type == ViewControllerTransitioningAnimatorPresentationType.Present;
            var containerView = transitionContext.ContainerView;

            var fromView = (_sourceViewController as UIViewController)?.View;
            var toView = (_destinationViewController as UIViewController)?.View;

            if (fromView == null || toView == null)
            {
                transitionContext.CompleteTransition(false);
                return;
            }

            containerView.AddSubview(toView);

            var window = toView.Window;
            var fromViewSnapshot = _sourceViewController.GetViewForSnapshot();
            var fromViewSnapshotView = fromViewSnapshot.SnapshotView(true);
            var fromViewSnapshotRect = fromViewSnapshot.ConvertRectToView(fromViewSnapshot.Bounds, window);
            var toViewSnapshot = _destinationViewController.GetViewForSnapshot();
            var toViewSnapshotView = toViewSnapshot.SnapshotView(true);
            var toViewSnapshotRect = toViewSnapshot.ConvertRectToView(toViewSnapshot.Bounds, window);

            UIView backgroundView;
            var fadeView = new UIView(containerView.Bounds) { BackgroundColor = toView.BackgroundColor };

            if (isPresenting)
            {
                backgroundView = new UIView(containerView.Bounds);
                backgroundView.AddSubview(fadeView);
                fadeView.Alpha = 0;
            }
            else
            {
                backgroundView = fadeView;
            }

            containerView.AddSubviews(backgroundView, fromViewSnapshotView, toViewSnapshotView);

            fromViewSnapshotView.Alpha = isPresenting ? 1 : 0;
            toViewSnapshotView.Alpha = isPresenting ? 0 : 1;
            toView.Alpha = 0;

            fromViewSnapshotView.Frame = isPresenting ? fromViewSnapshotRect : toViewSnapshotRect;
            toViewSnapshotView.Frame = isPresenting ? fromViewSnapshotRect : toViewSnapshotRect;

            (_sourceViewController as UIViewController).BeginAppearanceTransition(false, true);
            (_destinationViewController as UIViewController).BeginAppearanceTransition(true, true);

            UIView.AnimateKeyframes(Duration, 0, UIViewKeyframeAnimationOptions.CalculationModeCubic, () =>
            {
                UIView.AddKeyframeWithRelativeStartTime(0, 1, () =>
                {
                    fromViewSnapshotView.Frame = isPresenting ? toViewSnapshotRect : fromViewSnapshotRect;
                    toViewSnapshotView.Frame = isPresenting ? toViewSnapshotRect : fromViewSnapshotRect;
                    fromViewSnapshotView.Layer.CornerRadius = isPresenting ? 0 : 12;
                    fadeView.Alpha = isPresenting ? 1 : 0;
                });

                UIView.AddKeyframeWithRelativeStartTime(0, 0.6, () =>
                {
                    fromViewSnapshotView.Alpha = isPresenting ? 0 : 1;
                    toViewSnapshotView.Alpha = isPresenting ? 1 : 0;
                });
            }, (finished) =>
            {
                backgroundView.RemoveFromSuperview();
                fromViewSnapshotView.RemoveFromSuperview();
                toViewSnapshotView.RemoveFromSuperview();

                toView.Alpha = 1;

                (_sourceViewController as UIViewController).EndAppearanceTransition();
                (_destinationViewController as UIViewController).EndAppearanceTransition();

                transitionContext.CompleteTransition(true);
            });
        }

        private IViewControllerWithTransition ExtractViewController(UIViewController viewController)
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

        private IViewControllerWithTransition ExtractViewController(UINavigationController navigationController)
        {
            return navigationController?.VisibleViewController as IViewControllerWithTransition;
        }

        private IViewControllerWithTransition ExtractViewController(UITabBarController tabBarController)
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