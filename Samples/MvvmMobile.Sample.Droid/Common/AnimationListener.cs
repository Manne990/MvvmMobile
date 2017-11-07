using System;
using Android.Views.Animations;

namespace MvvmMobile.Sample.Droid.Common
{
    public class AnimationListener :  Java.Lang.Object, Animation.IAnimationListener
    {
        private readonly Action _animationStartListener;
        private readonly Action _animationRepeatListener;
        private readonly Action _animationEndListener;

        public AnimationListener(Action animationStartListener, Action animationRepeatListener, Action animationEndListener)
        {
            _animationStartListener = animationStartListener;
            _animationRepeatListener = animationRepeatListener;
            _animationEndListener = animationEndListener;
        }

        public void OnAnimationEnd(Animation animation)
        {
            _animationEndListener?.Invoke();
        }

        public void OnAnimationRepeat(Animation animation)
        {
            _animationRepeatListener?.Invoke();
        }

        public void OnAnimationStart(Animation animation)
        {
            _animationStartListener?.Invoke();
        }
    }
}