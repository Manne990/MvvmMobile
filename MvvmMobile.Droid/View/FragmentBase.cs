using System;
using System.ComponentModel;
using Android.Support.V4.App;
using Android.Views;
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
        protected virtual void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {}

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

                _viewModel.PropertyChanged -= ViewModel_PropertyChanged;
                _viewModel.PropertyChanged += ViewModel_PropertyChanged;

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
        public override void OnCreate(Android.OS.Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ViewModel = Core.Mvvm.Api.Resolver.Resolve<T>();
        }

        public override void OnResume()
        {
            base.OnResume();

            if (ParentActivity != null)
            {
                ParentActivity.BackButtonPressed -= ActivityBackButtonPressed;
                ParentActivity.BackButtonPressed += ActivityBackButtonPressed;
            }

            if (_viewModel != null)
            {
                _viewModel.PropertyChanged -= ViewModel_PropertyChanged;
                _viewModel.PropertyChanged += ViewModel_PropertyChanged;
            }

            _viewModel?.OnActivated();
            _viewModel?.InitWithPayload(PayloadId);
        }

        public override void OnPause()
        {
            base.OnPause();

            if (ParentActivity != null)
            {
                ParentActivity.BackButtonPressed -= ActivityBackButtonPressed;
            }

            _viewModel?.OnPaused();

            if (_viewModel != null)
            { 
                _viewModel.PropertyChanged -= ViewModel_PropertyChanged;
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            ParentActivity?.OnBackPressed();

            return base.OnOptionsItemSelected(item);
        }

        private void ActivityBackButtonPressed(object sender, EventArgs e)
        {
            OnBackPressed();
        }
    }
}