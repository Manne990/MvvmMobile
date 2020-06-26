using MvvmMobile.iOS.Navigation;
using UIKit;

namespace MvvmMobile.Sample.iOS.ViewController.Edit
{
    public class DismissAnimator : UIViewControllerAnimatedTransitioning, ITransitionAnimator
    {
        private const double Duration = 0.5;

        public void InitWithTransitionType(ViewControllerTransitioningAnimatorPresentationType type)
        {
        }

        public override double TransitionDuration(IUIViewControllerContextTransitioning transitionContext)
        {
            return Duration;
        }

        public override void AnimateTransition(IUIViewControllerContextTransitioning transitionContext)
        {
            // Get the ViewControllers
            var _sourceViewController = TransitionAnimationHelper.ExtractViewController(transitionContext.GetViewControllerForKey(UITransitionContext.FromViewControllerKey));
            var _destinationViewController = TransitionAnimationHelper.ExtractViewController(transitionContext.GetViewControllerForKey(UITransitionContext.ToViewControllerKey));

            if (_sourceViewController == null || _destinationViewController == null)
            {
                transitionContext.CompleteTransition(false);
                return;
            }

            // Get the views
            var containerView = transitionContext.ContainerView;
            var fromView = _sourceViewController.GetViewForSnapshot(_destinationViewController.GetType(), ViewControllerTransitioningAnimatorPresentationType.Dismiss);
            var toView = _destinationViewController.GetViewForSnapshot(_sourceViewController.GetType(), ViewControllerTransitioningAnimatorPresentationType.Dismiss);

            if (containerView == null || fromView == null || toView == null)
            {
                transitionContext.CompleteTransition(false);
                return;
            }

            var window = fromView.Window;

            // Calculate
            var fromViewSnapshotView = fromView.SnapshotView(true);
            var toViewRect = toView.ConvertRectToView(toView.Bounds, window);

            fromViewSnapshotView.Frame = fromView.ConvertRectToView(fromView.Bounds, window);
            fromViewSnapshotView.Alpha = 1;
            toView.Alpha = 0;

            containerView.AddSubviews(fromViewSnapshotView, toView);

            // Animate
            UIView.AnimateKeyframes(Duration, 0, UIViewKeyframeAnimationOptions.CalculationModeCubic, () =>
            {
                UIView.AddKeyframeWithRelativeStartTime(0, 1, () =>
                {
                    fromViewSnapshotView.Frame = _destinationViewController.TransitionTargetRect();
                    toView.Frame = toViewRect;
                });

                UIView.AddKeyframeWithRelativeStartTime(0, 1, () =>
                {
                    fromViewSnapshotView.Alpha = 0;
                    toView.Alpha = 1;
                });
            }, (finished) =>
            {
                transitionContext.CompleteTransition(true);
            });
        }
    }
}