using UIKit;

namespace MvvmMobile.iOS.Navigation
{
    public interface ITransitionAnimator : IUIViewControllerAnimatedTransitioning
    {
    }

    public class ViewControllerTransitioningDelegate : UIViewControllerTransitioningDelegate
    {
        private readonly ITransitionAnimator _transitionPresentedAnimator;
        private readonly ITransitionAnimator _transitionDismissedAnimator;

        public ViewControllerTransitioningDelegate(ITransitionAnimator transitionPresentedAnimator, ITransitionAnimator transitionDismissedAnimator)
        {
            _transitionPresentedAnimator = transitionPresentedAnimator;
            _transitionDismissedAnimator = transitionDismissedAnimator;
        }

        public override IUIViewControllerAnimatedTransitioning GetAnimationControllerForPresentedController(UIViewController presented, UIViewController presenting, UIViewController source)
        {
            return _transitionPresentedAnimator;
        }

        public override IUIViewControllerAnimatedTransitioning GetAnimationControllerForDismissedController(UIViewController dismissed)
        {
            return _transitionDismissedAnimator;
        }
    }

    public enum ViewControllerTransitioningAnimatorPresentationType
    {
        Present,
        Dismiss
    }
}