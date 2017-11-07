using System;
using Android.Views;

namespace MvvmMobile.Sample.Droid.Common
{
    public class PreDrawListener : Java.Lang.Object, ViewTreeObserver.IOnPreDrawListener
    {
        private readonly Func<View> _preDrawListener;

        public PreDrawListener(Func<View> preDrawListener)
        {
            _preDrawListener = preDrawListener;
        }

        public bool OnPreDraw()
        {
            var view = _preDrawListener?.Invoke();

            view?.ViewTreeObserver?.RemoveOnPreDrawListener(this);

            return true;
        }
    }
}