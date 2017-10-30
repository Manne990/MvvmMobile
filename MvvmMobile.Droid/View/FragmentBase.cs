using System;
using System.ComponentModel;
using Android.App;
using MvvmMobile.Core.ViewModel;
using XLabs.Ioc;

namespace MvvmMobile.Droid.View
{
    public class FragmentBase : Fragment
    {
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

        public string Title { get; protected set; }
        protected Guid PayloadId { get; set; }
        protected Action<Guid> CallbackAction { get; set; }

        protected ActivityBase ActionBarActivity
        {
            get
            {
                return Activity as ActivityBase;
            }
        }

        protected virtual void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
        }

        public override void OnResume()
        {
            base.OnResume();

            var vm = _viewModel as BaseViewModel;
            if (vm == null)
            {
                return;
            }

            vm.OnActivated();
        }

        public override void OnPause()
        {
            base.OnPause();

            var vm = _viewModel as BaseViewModel;
            if (vm == null)
            {
                return;
            }

            vm.OnPaused();
        }

        public override void OnDestroy()
        {
            if (_viewModel != null)
            { 
                _viewModel.PropertyChanged -= ViewModel_PropertyChanged;
            }

            base.OnDestroy();
        }

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