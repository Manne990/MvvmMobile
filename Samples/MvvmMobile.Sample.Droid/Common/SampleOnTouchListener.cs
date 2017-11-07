using System;
using Android.Views;

namespace MvvmMobile.Sample.Droid.Common
{
    public class SampleOnTouchListener : Java.Lang.Object, View.IOnTouchListener
    {
        private const int MoveThreshold = 5;

        private readonly Action<float> _onPanStartListener;
        private readonly Action<SamplePanMoveArgs> _onPanMovedListener;
        private readonly Action _onPanEndedListener;
        private readonly Action _onTapListener;
        private float _startX, _startY;
        private bool _hasMoved;

        public SampleOnTouchListener(Action<float> onPanStartListener, Action<SamplePanMoveArgs> onPanMovedListener, Action onPanEndedListener, Action onTapListener)
        {
            _onPanStartListener = onPanStartListener;
            _onPanMovedListener = onPanMovedListener;
            _onPanEndedListener = onPanEndedListener;
            _onTapListener = onTapListener;
        }

        public bool OnTouch(View v, MotionEvent e)
        {
            switch (e.Action)
            {
                case MotionEventActions.Down:
                    _hasMoved = false;
                    _startX = e.RawX;
                    _startY = e.RawY;
                    _onPanStartListener?.Invoke(_startX);
                    return true;

                case MotionEventActions.Move:
                    if (Math.Abs(e.RawX - _startX) < MoveThreshold && Math.Abs(e.RawY - _startY) < MoveThreshold)
                    {
                        return true;
                    }

                    _hasMoved = true;
                    _onPanMovedListener?.Invoke(new SamplePanMoveArgs() { MovedX = e.RawX - _startX, MovedY = e.RawY - _startY, InnerX = e.GetX() });
                    return true;

                case MotionEventActions.Up:
                    if(_hasMoved == false)
                    {
                        _onTapListener?.Invoke();
                    }
                    else
                    {
                        _onPanEndedListener?.Invoke();
                    }
                    _startX = 0f;
                    _startY = 0f;
                    _hasMoved = false;
                    return true;

                case MotionEventActions.Cancel:
                case MotionEventActions.Outside:
                    _onPanEndedListener?.Invoke();
                    _startX = 0f;
                    _startY = 0f;
                    _hasMoved = false;
                    return true;
            }

            return false;
        }
    }

    public class SamplePanMoveArgs
    {
        public float MovedX { get; set; }
        public float MovedY { get; set; }
        public float InnerX { get; set; }
    }
}