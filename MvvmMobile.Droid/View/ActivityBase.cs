using System;
using System.ComponentModel;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7.App;
using MvvmMobile.Core.Common;
using MvvmMobile.Core.Navigation;
using MvvmMobile.Core.ViewModel;
using MvvmMobile.Droid.Model;
using MvvmMobile.Droid.Navigation;
using Fragment = Android.Support.V4.App.Fragment;

namespace MvvmMobile.Droid.View
{
    public class ActivityBase : AppCompatActivity
    {
        // Events
        public event EventHandler BackButtonPressed;


        // -----------------------------------------------------------------------------

        // Properties
        protected bool BackButtonEnabled { get; set; }

        private int _fragmentContainerId = Resource.Id.fragmentContainer;
        protected int FragmentContainerId
        {
            get { return _fragmentContainerId; }
            set
            {
                _fragmentContainerId = value;
                ((AppNavigation)Core.Mvvm.Api.Resolver.Resolve<INavigation>()).FragmentContainerId = _fragmentContainerId;
            }
        }


        // -----------------------------------------------------------------------------

        // Overrides
        public override void OnBackPressed()
        {
            bool isBackPressConsumed = !BackButtonEnabled;

            if (/*isBackPressConsumed == false &&*/ GetCurrentFragment() is FragmentBase currentFragment)
            {
                isBackPressConsumed = currentFragment.OnBackPressed();
            }

            if (isBackPressConsumed == false)
            {
                BackButtonPressed?.Invoke(this, EventArgs.Empty);

                base.OnBackPressed();
            }
        }


        // -----------------------------------------------------------------------------

        // Public Methods
        public void EnableBackButton(bool enable)
        {
            BackButtonEnabled = enable;

            ActionBar?.SetDisplayHomeAsUpEnabled(enable);  //TODO: Refactor to work without action bar
            SupportActionBar?.SetDisplayHomeAsUpEnabled(enable);  //TODO: Refactor to work without action bar
        }

        protected Fragment GetCurrentFragment()
        {
            var fragment = SupportFragmentManager?.FindFragmentById(FragmentContainerId);

            return fragment;
        }
    }

    public class ActivityBase<T> : ActivityBase, IPlatformView where T : class, IBaseViewModel
    {
        // Private Members
        private const string CallBackPayloadId = "MvvmMobileActivityBase-CallBackPayloadId";


        // -----------------------------------------------------------------------------

        // Properties
        private T _viewModel;
        protected T ViewModel
        {
            get { return _viewModel; }
            set
            {
                _viewModel = value;

                if (_viewModel == null)
                {
                    return;
                }

                _viewModel.PropertyChanged -= ViewModelPropertyChangedInternal;
                _viewModel.PropertyChanged += ViewModelPropertyChangedInternal;

                _viewModel.OnLoaded();
                _viewModel.CallbackAction = HandleCallback;
            }
        }

        protected Guid PayloadId { get; private set; }
        protected Guid CallbackId { get; private set; }


        // -----------------------------------------------------------------------------

        // Lifecycle
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            BackButtonEnabled = true;

            ViewModel = Core.Mvvm.Api.Resolver.Resolve<T>();

            ((AppNavigation)Core.Mvvm.Api.Resolver.Resolve<INavigation>()).Context = this;

            var extras = Intent.Extras;

            if (extras != null && string.IsNullOrWhiteSpace(extras.GetString(AppNavigation.PayloadAppParameter)) == false)
            {
                PayloadId = new Guid(extras.GetString(AppNavigation.PayloadAppParameter));
            }

            CallbackId = Guid.Empty;
            if (extras != null && string.IsNullOrWhiteSpace(extras.GetString(AppNavigation.CallbackAppParameter)) == false)
            {
                CallbackId = new Guid(extras.GetString(AppNavigation.CallbackAppParameter));
            }
        }

        protected override void OnStart()
        {
            base.OnStart();

            ActionBar?.SetDisplayHomeAsUpEnabled(true); //TODO: Refactor to work without action bar
            SupportActionBar?.SetDisplayHomeAsUpEnabled(true); //TODO: Refactor to work without action bar
        }

        protected override void OnResume()
        {
            base.OnResume();

            var appNavigation = ((AppNavigation)Core.Mvvm.Api.Resolver.Resolve<INavigation>());
            appNavigation.Context = this;
            appNavigation.FragmentContainerId = FragmentContainerId;

            if (_viewModel != null)
            {
                _viewModel.PropertyChanged -= ViewModelPropertyChangedInternal;
                _viewModel.PropertyChanged += ViewModelPropertyChangedInternal;
            }

            _viewModel?.InitWithPayload(PayloadId);
            _viewModel?.OnActivated();
        }

        protected override void OnPause()
        {
            base.OnPause();

            _viewModel?.OnPaused();

            if (_viewModel != null)
            { 
                _viewModel.PropertyChanged -= ViewModelPropertyChangedInternal;
            }
        }

        public override bool OnOptionsItemSelected(Android.Views.IMenuItem item)
        {
            OnBackPressed();

            return base.OnOptionsItemSelected(item);
        }

        protected virtual void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e) { }


        // -----------------------------------------------------------------------------

        // Payload and Callback Handling
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            ((AppNavigation)Core.Mvvm.Api.Resolver.Resolve<INavigation>()).Context = this;

            if (requestCode != AppNavigation.CallbackActivityRequestCode)
            {
                return;
            }

            if (resultCode != Result.Ok)
            {
                return;
            }

            var extras = data.Extras;
            if (extras == null || string.IsNullOrWhiteSpace(extras.GetString(AppNavigation.CallbackAppParameter)))
            {
                return;
            }

            // Get the callback id
            var callbackId = new Guid(extras.GetString(AppNavigation.CallbackAppParameter));

            // Get the callback payload
            var payloads = Core.Mvvm.Api.Resolver.Resolve<IPayloads>();
            var callbackPayload = payloads.GetAndRemove<ICallbackPayload>(callbackId);
            if (callbackPayload == null)
            {
                return;
            }

            // Get the payload
            var payloadId = new Guid(data.GetStringExtra(CallBackPayloadId));

            // Execute the callback
            callbackPayload.CallbackAction?.Invoke(payloadId);
        }

        protected void ClearPayload()
        {
            PayloadId = Guid.Empty;
        }

        private void HandleCallback(Guid payloadId)
        {
            // Create the callback intent
            var resultIntent = new Intent();

            resultIntent.PutExtra(AppNavigation.CallbackAppParameter, CallbackId.ToString());
            resultIntent.PutExtra(CallBackPayloadId, payloadId.ToString());

            SetResult(Result.Ok, resultIntent);

            Finish();
        }

        private void ViewModelPropertyChangedInternal(object sender, PropertyChangedEventArgs e)
        {
            RunOnUiThread(() => ViewModel_PropertyChanged(sender, e));
        }
    }
}