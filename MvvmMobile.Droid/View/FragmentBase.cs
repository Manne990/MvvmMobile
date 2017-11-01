﻿using System;
using System.ComponentModel;
using Android.App;
using MvvmMobile.Core.ViewModel;
using XLabs.Ioc;

namespace MvvmMobile.Droid.View
{
    public class FragmentBase : Fragment
    {
        // Properties
        public string Title { get; protected set; }
        protected Guid PayloadId { get; set; }
        protected Action<Guid> CallbackAction { get; set; }


        // -----------------------------------------------------------------------------

        // Lifecycle
        protected virtual void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
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
            var payloads = Resolver.Resolve<IPayloads>();

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

    public class FragmentBase<T> : FragmentBase where T : class, IBaseViewModel //IFragmentBase
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

            ViewModel = Resolver.Resolve<T>();
        }

        public override void OnResume()
        {
            base.OnResume();

            if (_viewModel != null)
            {
                _viewModel.PropertyChanged -= ViewModel_PropertyChanged;
                _viewModel.PropertyChanged += ViewModel_PropertyChanged;
            }

            _viewModel?.OnActivated();

            if (_viewModel is IPayloadViewModel vm)
            {
                vm.Load(PayloadId);
            }
        }

        public override void OnPause()
        {
            base.OnPause();

            _viewModel?.OnPaused();

            if (_viewModel != null)
            { 
                _viewModel.PropertyChanged -= ViewModel_PropertyChanged;
            }
        }
    }
}