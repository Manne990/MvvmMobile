using Android.Views;
using Android.Views.Animations;

namespace MvvmMobile.Sample.Droid.Common
{
    public class LeftMarginAnimation : Animation
    {
        private readonly View _view;
        private readonly int _fromLeftmargin;
        private readonly int _toLeftmargin;

        public LeftMarginAnimation(View view, int toLeftmargin)
        {
            _view = view;
            _toLeftmargin = toLeftmargin;
            _fromLeftmargin = ((ViewGroup.MarginLayoutParams)_view.LayoutParameters).LeftMargin;
        }

        protected override void ApplyTransformation(float interpolatedTime, Transformation t)
        {
            var layoutParams = (ViewGroup.MarginLayoutParams)_view.LayoutParameters;

            if (_toLeftmargin > _fromLeftmargin)
            {
                layoutParams.LeftMargin = _fromLeftmargin + (int)((_toLeftmargin - _fromLeftmargin) * interpolatedTime);
                layoutParams.Width = _view.MeasuredWidth;
            }
            else
            {
                layoutParams.LeftMargin = _fromLeftmargin - (int)((_fromLeftmargin - _toLeftmargin) * interpolatedTime);
                layoutParams.Width = _view.MeasuredWidth;
            }

            _view.LayoutParameters = layoutParams;
        }
    }
}