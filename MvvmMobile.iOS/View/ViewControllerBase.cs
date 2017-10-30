﻿using System;
using System.ComponentModel;
using MvvmMobile.Core.ViewModel;
using UIKit;
using XLabs.Ioc;

namespace MvvmMobile.iOS.View
{
    public class ViewControllerBase : UIViewController
    {
        public ViewControllerBase()
        {
            AsModal = false;
        }

        public override void ViewWillLayoutSubviews()
        {
            if (IsInitialized == false)
            {
                ViewFramesReady();
            }

            IsInitialized = true;

            base.ViewWillLayoutSubviews();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            var vm = _viewModel as BaseViewModel;
            if (vm == null)
            {
                return;
            }

            vm.OnActivated();
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            var vm = _viewModel as BaseViewModel;
            if (vm != null)
            {
                vm.OnPaused();
            }

            if (_viewModel != null)
            {
                _viewModel.PropertyChanged -= ViewModel_PropertyChanged;
            }
        }






        private bool IsInitialized { get; set; }
        protected Guid PayloadId { get; set; }
        protected Action<Guid> CallbackAction { get; set; }
        public bool AsModal { get; protected set; }

        private IBaseViewModel _viewModel;
        protected IBaseViewModel ViewModel
        {
            // ReSharper disable once UnusedMember.Global
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

                ((BaseViewModel)_viewModel).OnLoaded();
                ((BaseViewModel)_viewModel).CallbackAction = CallbackAction;
            }
        }





        protected virtual void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e) { }
        protected virtual void ViewFramesReady() { }




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
}
