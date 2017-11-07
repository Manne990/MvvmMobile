using System.Drawing;
using Android.App;

namespace MvvmMobile.Sample.Droid.Common
{
    public static class ScreenHelper
    {
        public static float GetScreenDensity()
        {
            return Application.Context.Resources.DisplayMetrics.Density;
        }

        public static Size GetScreenSize()
        {
            return new Size(Application.Context.Resources.DisplayMetrics.WidthPixels, Application.Context.Resources.DisplayMetrics.HeightPixels);
        }
    }
}