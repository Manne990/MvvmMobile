using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Content;
using AndroidX.AppCompat.App;
using MvvmMobile.Core.Navigation;
using MvvmMobile.Core.ViewModel;
using MvvmMobile.Droid.Navigation;
using MvvmMobile.Sample.Core.Navigation;

namespace MvvmMobile.Sample.Droid.Navigation
{
    public class CustomNavigation : ICustomNavigation
    {
        private AppNavigation _appNavigation;

        public CustomNavigation(INavigation anAppNavigation)
        {
            _appNavigation = anAppNavigation as AppNavigation;
        }

        public Context GetContext()
        {
            return _appNavigation.GetContext();
        }

        public Dictionary<Type, Type> GetViewMapper()
        {
            return _appNavigation.GetViewMapper();
        }

        public void NavigateBack(Action done = null, BackBehaviour behaviour = BackBehaviour.CloseLastSubView)
        {
            _appNavigation.NavigateBack(done, behaviour);
        }

        public void NavigateBack(Action<Guid> callbackAction, Guid payloadId, Action done = null, BackBehaviour behaviour = BackBehaviour.CloseLastSubView)
        {
            _appNavigation.NavigateBack(callbackAction, payloadId, done, behaviour);
        }

        public Task NavigateBack<T>() where T : IBaseViewModel
        {
            return _appNavigation.NavigateBack<T>();
        }

        public Task NavigateBack<T>(Action<Guid> callbackAction, Guid payloadId) where T : IBaseViewModel
        {
            return _appNavigation.NavigateBack<T>(callbackAction, payloadId);
        }

        public void NavigateTo(Type viewModelType, IPayload parameter = null, Action<Guid> callback = null, bool clearHistory = false)
        {
            _appNavigation.NavigateTo(viewModelType, parameter, callback);
        }

        public void NavigateTo<T>(IPayload parameter = null, Action<Guid> callback = null, bool clearHistory = false) where T : IBaseViewModel
        {
            _appNavigation.NavigateTo<T>(parameter, callback, clearHistory);
        }

        public void NavigateToSubView(Type viewModelType, IPayload parameter = null, Action<Guid> callback = null, bool clearHistory = false)
        {
            _appNavigation.NavigateToSubView(viewModelType, parameter, callback, clearHistory);
        }

        public void NavigateToSubView<T>(IPayload parameter = null, Action<Guid> callback = null, bool clearHistory = false) where T : IBaseViewModel
        {
            _appNavigation.NavigateToSubView<T>(parameter, callback, clearHistory);
        }

        public async Task NavigateToRoot()
        {
            await Task.Delay(1);

            if (GetContext() is AppCompatActivity activity)
            {
                var intent = new Intent(GetContext(), typeof(Activities.Start.StartActivity));

                intent.AddFlags(ActivityFlags.ClearTop);

                GetContext().StartActivity(intent);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("AppNavigation.NavigateBack<T>: Context is null or not an AppCompatActivity!");
            }
        }

        public async Task NavigateBack(Type viewModelInterfaceType)
        {
            await _appNavigation?.NavigateBack(viewModelInterfaceType);
        }

        public async Task NavigateBack(Type viewModelInterfaceType, Action<Guid> callbackAction, Guid payloadId)
        {
            await _appNavigation?.NavigateBack(viewModelInterfaceType, callbackAction, payloadId);
        }
    }
}