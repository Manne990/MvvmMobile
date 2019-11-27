using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Java.Lang;
using MvvmMobile.Core.Common;
using MvvmMobile.Core.Navigation;
using MvvmMobile.Core.ViewModel;
using MvvmMobile.Droid.Common;
using MvvmMobile.Droid.Model;
using MvvmMobile.Droid.View;

namespace MvvmMobile.Droid.Navigation
{
	public class AppNavigation : INavigation
    {
        // Constants
        internal const int CallbackActivityRequestCode = 9999;
        internal const string PayloadAppParameter = "MvvmMobile-PayloadAppParameter";
        internal const string CallbackAppParameter = "MvvmMobile-CallbackAppParameter";


        // -----------------------------------------------------------------------------

        // Private Members
        private bool _useActivityTransitions;
        private bool CanUseActivityTransitions
        {
            get
            {
                //return false;
                return _useActivityTransitions && GetContext() != null && GetContext() is AppCompatActivity && Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop;
            }
        }


        // -----------------------------------------------------------------------------

        // Virtual Methods
        public virtual Context GetContext()
        {
            return Context;
        }

        public virtual Dictionary<Type, Type> GetViewMapper()
        {
            if (ViewMapperDictionary == null)
            {
                Init();
            }

            return ViewMapperDictionary;
        }


        // -----------------------------------------------------------------------------

        // Public Properties
        public Context Context { private get; set; }
        public int FragmentContainerId { get; set; }

        public Dictionary<Type, Type> ViewMapperDictionary { get; private set; }


        // -----------------------------------------------------------------------------

        // Public Methods
        public void Init(bool useActivityTransitions = false)
        {
            ViewMapperDictionary = new Dictionary<Type, Type>();
            _useActivityTransitions = useActivityTransitions;
        }

        public FragmentBase RequestFragment<TViewModel>() where TViewModel : IBaseViewModel
        {
            // Get the vc type
            var viewModelType = typeof(TViewModel);

            if (GetViewMapper().TryGetValue(viewModelType, out Type concreteType) == false)
            {
                throw new System.Exception($"The viewmodel '{viewModelType.ToString()}' does not exist in view mapper!");
            }

            return concreteType.IsSubclassOf(typeof(FragmentBase)) ? (FragmentBase)Activator.CreateInstance(concreteType) : null;
        }

        public void AddViewMapping<TViewModel, TPlatformView>() where TViewModel : IBaseViewModel where TPlatformView : IPlatformView
        {
            if (ViewMapperDictionary == null)
            {
                Init();
            }

            if (ViewMapperDictionary.ContainsKey(typeof(TViewModel)))
            {
                throw new System.Exception($"The viewmodel '{typeof(TViewModel).ToString()}' does already exist in view mapper!");
            }

            ViewMapperDictionary.Add(typeof(TViewModel), typeof(TPlatformView));
        }

        public void NavigateTo<T>(IPayload parameter = null, Action<Guid> callback = null, bool clearHistory = false) where T : IBaseViewModel
        {
            NavigateTo(typeof(T), parameter, callback, clearHistory);
        }

        public void NavigateTo(Type viewModelType, IPayload parameter = null, Action<Guid> callback = null, bool clearHistory = false)
        {
            if (viewModelType == null)
            {
                return;
            }

            if (GetViewMapper().TryGetValue(viewModelType, out Type concreteType) == false)
            {
                throw new System.Exception($"The viewmodel '{viewModelType.ToString()}' does not exist in view mapper!");
            }

            if (concreteType.IsSubclassOf(typeof(FragmentBase)))
            {
#if BACKWARD_COMPATIBLE_MODE
                Console.WriteLine("Navigating to Fragments through NavigateTo() is deprecated.");
                LoadFragment(concreteType, parameter, callback);
                return;

#else
                throw new NotSupportedException("Navigating to Fragments through NavigateTo() is deprecated.");
#endif
            }

            var concreteTypeJava = Class.FromType(concreteType);
            var intent = new Intent(GetContext(), concreteTypeJava);

            if (clearHistory)
            {
                intent.AddFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
            }

            if (parameter != null)
            {
                intent.SetPayload(parameter);
            }

            var currentActivity = GetContext() as AppCompatActivity;
            if (currentActivity == null)
            {
                System.Diagnostics.Debug.WriteLine("AppNavigation.NavigateTo: Context is null or not an activity!");
                return;
            }

            if (callback != null)
            {
                intent.SetCallback(callback);

                currentActivity.RunOnUiThread(() =>
                {
                    if (CanUseActivityTransitions)
                    {
                        try
                        {
                            currentActivity.StartActivityForResult(intent, CallbackActivityRequestCode, Android.App.ActivityOptions.MakeSceneTransitionAnimation(GetContext() as AppCompatActivity).ToBundle());
                        }
                        catch //REMARK: This is due to that this crashes on some devices even if the Android version supports this
                        {
                            System.Diagnostics.Debug.WriteLine("Activity transitions are not working on this device. Transitions will be disabled.");
                            _useActivityTransitions = false;
                            currentActivity.StartActivityForResult(intent, CallbackActivityRequestCode);
                        }
                    }
                    else
                    {
                        currentActivity.StartActivityForResult(intent, CallbackActivityRequestCode);
                    }
                });

                return;
            }

            currentActivity.RunOnUiThread(() =>
            {
                if (CanUseActivityTransitions)
                {
                    try
                    {
                        currentActivity.StartActivity(intent, Android.App.ActivityOptions.MakeSceneTransitionAnimation(GetContext() as AppCompatActivity).ToBundle());
                    }
                    catch //REMARK: This is due to that this crashes on some devices even if the Android version supports this
                    {
                        System.Diagnostics.Debug.WriteLine("Activity transitions are not working on this device. Transitions will be disabled.");
                        _useActivityTransitions = false;
                        currentActivity.StartActivity(intent);
                    }
                }
                else
                {
                    currentActivity.StartActivity(intent);
                }
            });
        }

        public void NavigateToSubView<T>(IPayload parameter = null, Action<Guid> callback = null, bool clearHistory = false) where T : IBaseViewModel
        {
            NavigateToSubView(typeof(T), parameter, callback, clearHistory);
        }

        public void NavigateToSubView(Type viewModelType, IPayload parameter = null, Action<Guid> callback = null, bool clearHistory = false)
        {
            if (viewModelType == null)
            {
                return;
            }

            if (GetViewMapper().TryGetValue(viewModelType, out Type concreteType) == false)
            {
                throw new System.Exception($"The viewmodel '{viewModelType.ToString()}' does not exist in view mapper!");
            }

            if (concreteType.IsSubclassOf(typeof(FragmentBase)) == false)
            {
                throw new ArgumentException($"The viewmodel '{viewModelType.ToString()}' does not inherit from FragmentBase!");
            }

            LoadFragment(concreteType, parameter, callback);
            return;
        }

        public void NavigateBack(Action done = null, BackBehaviour behaviour = BackBehaviour.CloseLastSubView)
        {
            NavigateBackInternal(null, done, behaviour);
        }

        public void NavigateBack(Action<Guid> callbackAction, Guid payloadId, Action done = null, BackBehaviour behaviour = BackBehaviour.CloseLastSubView)
        {
            NavigateBackInternal(() => 
            {
                callbackAction.Invoke(payloadId);
            }, done, behaviour);
        }

        private void NavigateBackInternal(Action callbackListener, Action done = null, BackBehaviour behaviour = BackBehaviour.CloseLastSubView)
        {
            if (GetContext() is AppCompatActivity activity)
            {
                int subViewCountThreshold = 0;
                switch (behaviour)
                {
                    case BackBehaviour.CloseLastSubView:
                        subViewCountThreshold = 0;
                        break;
                    case BackBehaviour.SkipFromLastSubView:
                        subViewCountThreshold = 1;
                        break;
                    case BackBehaviour.FullViewsOnly:
                        subViewCountThreshold = int.MaxValue;
                        break;
                }

                if (activity.SupportFragmentManager?.BackStackEntryCount <= subViewCountThreshold)
                {
                    callbackListener?.Invoke();

                    if (CanUseActivityTransitions)
                    {
                        activity.FinishAfterTransition();
                    }
                    else
                    {
                        activity.Finish();
                    }
                }
                else
                {
                    try
                    {
                        activity.SupportFragmentManager?.PopBackStackImmediate();
                    }
                    catch
                    {
                        // swallow exceptions, there's a bug in FragmentManager.java
                    }

                    callbackListener?.Invoke();
                }

                done?.Invoke();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("AppNavigation.NavigateBack: Context is null or not an activity!");
            }
        }

        public async Task NavigateBack<T>() where T : IBaseViewModel
        {
            await NavigateBack(typeof(T));
        }

        public async Task NavigateBack<T>(Action<Guid> callbackAction, Guid payloadId) where T : IBaseViewModel
        {
			await NavigateBack<T>();

            callbackAction.Invoke(payloadId);
        }

        public async Task NavigateBack(Type viewModelInterfaceType)
        {
            await Task.Delay(1);

            if (GetContext() is AppCompatActivity activity)
            {
                if (GetViewMapper().TryGetValue(viewModelInterfaceType, out Type concreteType) == false)
                {
                    throw new System.Exception($"The viewmodel '{viewModelInterfaceType.ToString()}' does not exist in view mapper!");
                }

                var targetIsFragment = concreteType.IsSubclassOf(typeof(FragmentBase));
                if (targetIsFragment)
                {
                    // Target = Fragment
                    while (activity.SupportFragmentManager?.BackStackEntryCount > 0)
                    {
                        var fragment = activity.SupportFragmentManager?.Fragments.LastOrDefault();
                        if (fragment.GetType() == concreteType)
                        {
                            // The target fragment was found!
                            return;
                        }

                        activity.SupportFragmentManager?.PopBackStackImmediate();
                    }

                    // If we reach this point then the target fragment was not found in the current activitys fragment back stack...
                    // ...or the fragment back stack was empty.
                    // What to do now?

                    //TODO: Implement!
                }
                else
                {
                    // Target = Activity
                    var targetEqualsSourceActivity = activity.GetType() == concreteType;
                    if (targetEqualsSourceActivity)
                    {
                        // Back to same activity
                        if (activity.SupportFragmentManager?.BackStackEntryCount <= 1)
                        {
                            // Back to same activity and no fragment back stack -> Do nothing
                            return;
                        }

                        // Empty the fragment back stack
                        while (activity.SupportFragmentManager?.BackStackEntryCount > 1)
                        {
                            activity.SupportFragmentManager?.PopBackStackImmediate();
                        }

                        return;
                    }

                    // Back to another activity
                    var concreteTypeJava = Class.FromType(concreteType);
                    var intent = new Intent(GetContext(), concreteTypeJava);

                    intent.AddFlags(ActivityFlags.ClearTop);

                    GetContext().StartActivity(intent);
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("AppNavigation.NavigateBack<T>: Context is null or not an AppCompatActivity!");
            }
        }

        public async Task NavigateBack(Type viewModelInterfaceType, Action<Guid> callbackAction, Guid payloadId)
        {
            await NavigateBack(viewModelInterfaceType);

            callbackAction.Invoke(payloadId);
        }

        public FragmentBase LoadFragment(Type concreteType, IPayload payload = null, Action<Guid> callback = null)
        {
            try
            {
                // Get the current activity
                var activity = GetContext() as AppCompatActivity;
                if (activity == null)
                {
                    System.Diagnostics.Debug.WriteLine("AppNavigation.LoadFragment: Context is null or not an activity!");
                    return null;
                }

                // Check if the activity has a fragment container
                var fragmentContainer = activity.FindViewById<FrameLayout>(FragmentContainerId);
                if (fragmentContainer == null)
                {
                    // No container -> Use generic fragment container activity
                    var intent = new Intent(GetContext(), typeof(FragmentContainerActivity));

                    var activityPayload = Core.Mvvm.Api.Resolver.Resolve<IFragmentContainerPayload>();

                    activityPayload.FragmentType = concreteType;
                    activityPayload.FragmentPayload = payload;
                    activityPayload.FragmentCallback = callback;

                    intent.SetPayload(activityPayload);

                    GetContext().StartActivity(intent);

                    return null;
                }

                // Create the fragment
                var fragment = (FragmentBase)Activator.CreateInstance(concreteType);

                // Set the payload
                if (payload != null)
                {
                    fragment.SetPayload(payload);
                }

                // Handle callback
                if (callback != null)
                {
                    fragment.SetCallback(callback);
                }

                // Push the fragment
                var ft = activity.SupportFragmentManager.BeginTransaction();

                ft.Replace(FragmentContainerId, fragment, concreteType.Name);
                ft.AddToBackStack(fragment.Title);

                ft.Commit();

                return fragment;
            }
            catch
            {
                return null;
            }
        }
    }
}