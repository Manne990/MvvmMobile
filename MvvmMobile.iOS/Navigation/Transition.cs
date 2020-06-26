using UIKit;

namespace MvvmMobile.iOS.Navigation
{
    public interface ITransitionAnimator : IUIViewControllerAnimatedTransitioning
    {
        void InitWithTransitionType(ViewControllerTransitioningAnimatorPresentationType type);
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
            _transitionPresentedAnimator?.InitWithTransitionType(ViewControllerTransitioningAnimatorPresentationType.Present);
            return _transitionPresentedAnimator;
        }

        public override IUIViewControllerAnimatedTransitioning GetAnimationControllerForDismissedController(UIViewController dismissed)
        {
            _transitionDismissedAnimator?.InitWithTransitionType(ViewControllerTransitioningAnimatorPresentationType.Dismiss);
            return _transitionDismissedAnimator;
        }
    }

    public enum ViewControllerTransitioningAnimatorPresentationType
    {
        Present,
        Dismiss
    }
}