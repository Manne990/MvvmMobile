using System;
using System.ComponentModel;
using Android.OS;
using Android.Views;
using AndroidX.Fragment.App;
using MvvmMobile.Core.Common;
using MvvmMobile.Core.ViewModel;

namespace MvvmMobile.Droid.View
{
    public class FragmentBase : Fragment, IPlatformView
    {
        // Properties
        public string Title { get; protected set; }
        protected Guid PayloadId { get; set; }
        protected Action<Guid> CallbackAction { get; set; }

        protected ActivityBase ParentActivity
        {
            get
            {
                return Activity as ActivityBase;
            }
        }


        // -----------------------------------------------------------------------------

        // Lifecycle
        public virtual bool OnBackPressed()
        {
            bool isBackPressedConsumed = false;

            return isBackPressedConsumed;
        }


        // -----------------------------------------------------------------------------

        // Payload and Callback Handling
        public void SetPayload(IPayload payload)
        { 
            if (payload == null)
            {
                return;
            }

            // Set payload id
            PayloadId = Guid.NewGuid();

            // Add payload
            var payloads = Core.Mvvm.Api.Resolver.Resolve<IPayloads>();

            payloads.Add(PayloadId, payload);
        }

        public void SetCallback(Action<Guid> callbackAction)
        {
            if (callbackAction == null)
            {
                return;
            }

            CallbackAction = callbackAction;
        }
    }

    public class FragmentBase<T> : FragmentBase, IPlatformView where T : class, IBaseViewModel
    {
        // Properties
        private T _viewModel;
        protected T ViewModel
        {
            get { return _viewModel; }
            set
            {
                var runEvents = _viewModel != value;

                _viewModel = value;

                if (_viewModel == null)
                {
                    return;
                }

                _viewModel.PropertyChanged -= ViewModelPropertyChangedInternal;
                _viewModel.PropertyChanged += ViewModelPropertyChangedInternal;

                if (runEvents == false)
                {
                    return;
                }

                _viewModel.OnLoaded();
                _viewModel.CallbackAction = CallbackAction;
            }
        }


        // -----------------------------------------------------------------------------

        // Lifecycle
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ViewModel = Core.Mvvm.Api.Resolver.Resolve<T>();
            ViewModel?.InitWithPayload(PayloadId);
        }

        public override void OnResume()
        {
            base.OnResume();

            // Recreate the VM if needed
            ViewModel ??= ViewHelper.RecreateIfNeeded(ViewModel, PayloadId);

            if (ParentActivity != null)
            {
                ParentActivity.BackButtonPressed -= ActivityBackButtonPressed;
                ParentActivity.BackButtonPressed += ActivityBackButtonPressed;
            }

            if (ViewModel != null)
            {
                ViewModel.PropertyChanged -= ViewModelPropertyChangedInternal;
                ViewModel.PropertyChanged += ViewModelPropertyChangedInternal;
            }

            ViewModel?.OnActivated();
        }

        public override void OnPause()
        {
            base.OnPause();

            if (ParentActivity != null)
            {
                ParentActivity.BackButtonPressed -= ActivityBackButtonPressed;
            }

            ViewModel?.OnPaused();

            if (ViewModel != null)
            {
                ViewModel.PropertyChanged -= ViewModelPropertyChangedInternal;
            }
        }

        public override void OnDestroy()
        {
            var payloads = Core.Mvvm.Api.Resolver.Resolve<IPayloads>();
            payloads?.Remove(PayloadId);

            base.OnDestroy();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            ParentActivity?.OnBackPressed();

            return base.OnOptionsItemSelected(item);
        }

        protected virtual void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e) { }

        private void ActivityBackButtonPressed(object sender, EventArgs e)
        {
            OnBackPressed();
        }

        private void ViewModelPropertyChangedInternal(object sender, PropertyChangedEventArgs e)
        {
            if (ParentActivity == null)
            {
                ViewModel_PropertyChanged(sender, e);
                return;
            }

            ParentActivity.RunOnUiThread(() => ViewModel_PropertyChanged(sender, e));
        }
    }
}