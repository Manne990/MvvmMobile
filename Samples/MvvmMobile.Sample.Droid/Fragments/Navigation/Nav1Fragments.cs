using System.Threading.Tasks;
using Android.Graphics;
using AndroidX.AppCompat.App;
using MvvmMobile.Sample.Core.ViewModel.Navigation;

namespace MvvmMobile.Sample.Droid.Fragments.Navigation
{
    public class Nav1AFragment : NavFragmentBase<INav1AViewModel>
    {
        public Nav1AFragment()
        {
            BackgroundColor = Color.AliceBlue;
            TitleText = "Sub View 1A";
        }

        public override bool OnBackPressed()
        {
            var isDone = new TaskCompletionSource<bool>();

            var alert = new AlertDialog.Builder(Context, Resource.Style.Theme_AppCompat_Light_Dialog);
            alert.SetTitle("Really Go Back?");
            alert.SetMessage("Are you sure you want to go back?");
            alert.SetPositiveButton("Yes", (s, a) => ViewModel?.BackCommand?.Execute());
            alert.SetNegativeButton("No", (s, a) => { });
            alert.Create().Show();

            return true;
        }
    }

    public class Nav1BFragment : NavFragmentBase<INav1BViewModel>
    {
        public Nav1BFragment()
        {
            BackgroundColor = Color.AntiqueWhite;
            TitleText = "Sub View 1B";
        }
    }

    public class Nav1CFragment : NavFragmentBase<INav1CViewModel>
    {
        public Nav1CFragment()
        {
            BackgroundColor = new Color(0xFF, 0xF8, 0xF8);
            TitleText = "Sub View 1C";
        }
    }
}
