using Android.OS;
using Android.Views;
using Android.Widget;
using MvvmMobile.Droid.View;
using MvvmMobile.Sample.Core.ViewModel.Navigation;

namespace MvvmMobile.Sample.Droid.Fragments.Navigation
{
    public class Nav2Fragment : FragmentBase<INav2ViewModel>
    {
        // Private Members
        private Button _nextButton;
        private Button _prevButton;
        private Button _homeButton;


        // -----------------------------------------------------------------------------

        // Lifecycle
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Title = "Nav 2";
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.NavFragmentLayout, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            _nextButton = view.FindViewById<Button>(Resource.Id.nextButton);
            _nextButton.Click += (sender, e) =>
            {
                ViewModel?.NextCommand?.Execute();
            };

            _prevButton = view.FindViewById<Button>(Resource.Id.prevButton);
            _prevButton.Click += (sender, e) =>
            {
                ViewModel?.BackCommand?.Execute();
            };

            _homeButton = view.FindViewById<Button>(Resource.Id.homeButton);
            _homeButton.Click += (sender, e) =>
            {
                ViewModel?.HomeCommand?.Execute();
            };
        }

        public override void OnResume()
        {
            base.OnResume();

            ParentActivity.Title = Title;
            ParentActivity.EnableBackButton(true);
        }

        public override void OnDestroyView()
        {
            base.OnDestroyView();

            _nextButton = null;
            _prevButton = null;
            _homeButton = null;
        }
    }
}