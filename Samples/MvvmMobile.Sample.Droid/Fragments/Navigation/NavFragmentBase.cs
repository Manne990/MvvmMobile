using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using MvvmMobile.Droid.View;
using MvvmMobile.Sample.Core.ViewModel.Navigation;

namespace MvvmMobile.Sample.Droid.Fragments.Navigation
{
    public class NavFragmentBase<T> : FragmentBase<T> where T : class, INavBaseViewModel
    {
        // Private Members
        private Button _nextViewButton;
        private Button _nextSubViewButton;
        private Button _prevViewButton;
        private Button _backButton;
        private Button _homeButton;

        protected Color BackgroundColor { private get; set; }
        protected string TitleText { private get; set; }

        // -----------------------------------------------------------------------------

        // Lifecycle
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Title = TitleText;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var fragmentView = inflater.Inflate(Resource.Layout.NavFragmentLayout, container, false);

            fragmentView.FindViewById<LinearLayout>(Resource.Id.background).SetBackgroundColor(BackgroundColor);
            fragmentView.FindViewById<TextView>(Resource.Id.title).Text = TitleText;

            return fragmentView;
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            _nextViewButton = view.FindViewById<Button>(Resource.Id.nextViewButton);
            _nextViewButton.Enabled = ViewModel?.NextViewCommand != null;
            _nextViewButton.Click += (sender, e) => 
            {
                ViewModel?.NextViewCommand?.Execute();
            };

            _nextSubViewButton = view.FindViewById<Button>(Resource.Id.nextSubViewButton);
            _nextSubViewButton.Enabled = ViewModel?.NextViewCommand != null;
            _nextSubViewButton.Click += (sender, e) =>
            {
                ViewModel?.NextSubViewCommand?.Execute();
            };

            _backButton = view.FindViewById<Button>(Resource.Id.prevButton);
            _backButton.Enabled = ViewModel?.NextViewCommand != null;
            _backButton.Click += (sender, e) =>
            {
                ViewModel?.BackCommand?.Execute();
            };

            _homeButton = view.FindViewById<Button>(Resource.Id.homeButton);
            _homeButton.Enabled = ViewModel?.NextViewCommand != null;
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

            _nextViewButton = null;
            _nextSubViewButton = null;
            _prevViewButton = null;
            _backButton = null;
            _homeButton = null;
        }
    }
}