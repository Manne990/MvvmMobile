using System;
using System.Collections.Generic;
//using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Java.Lang;
using MvvmMobile.Core.Navigation;
using MvvmMobile.Core.ViewModel;
using MvvmMobile.Droid.Common;
using MvvmMobile.Droid.Model;
using MvvmMobile.Droid.View;

namespace MvvmMobile.Droid.Navigation
{
    public sealed class AppNavigation : INavigation
    {
        // Constants
        internal const int CallbackActivityRequestCode = 9999;
        internal const string PayloadAppParameter = "MvvmMobile-PayloadAppParameter";
        internal const string CallbackAppParameter = "MvvmMobile-CallbackAppParameter";


        // -----------------------------------------------------------------------------

        // Private Members
        private Dictionary<Type, Type> _viewMapperDictionary;
        private bool _useActivityTransitions;

        private bool CanUseActivityTransitions
        {
            get
            {
                return _useActivityTransitions && Context != null && Context is AppCompatActivity && Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop;
            }
        }


        // -----------------------------------------------------------------------------

        // Public Properties
        public Context Context { get; set; }
        public int FragmentContainerId { get; set; }


        // -----------------------------------------------------------------------------

        // Public Methods
        public void Init(Dictionary<Type, Type> viewMapper, bool useActivityTransitions = false)
        {
            _viewMapperDictionary = viewMapper;
            _useActivityTransitions = useActivityTransitions;
        }

        public void NavigateTo<T>(IPayload parameter = null, Action<Guid> callback = null) where T : IBaseViewModel
        {
            NavigateTo(typeof(T), parameter, callback);
        }

        public void NavigateTo(Type viewModelType, IPayload parameter = null, Action<Guid> callback = null)
        {
            if (viewModelType == null)
            {
                return;
            }

            if (_viewMapperDictionary.TryGetValue(viewModelType, out Type concreteType) == false)
            {
                throw new System.Exception($"The viewmodel '{viewModelType.ToString()}' does not exist in view mapper!");
            }

            if (concreteType.IsSubclassOf(typeof(FragmentBase)))
            {
                LoadFragment(concreteType, parameter, callback);
                return;
            }

            var concreteTypeJava = Class.FromType(concreteType);
            var intent = new Intent(Context, concreteTypeJava);

            if (parameter != null)
            {
                intent.SetPayload(parameter);
            }

            if (callback != null)
            {
                var currentActivity = Context as AppCompatActivity;
                if (currentActivity == null)
                {
                    System.Diagnostics.Debug.WriteLine("AppNavigation.NavigateTo: Context is null or not an activity!");
                    return;
                }

                intent.SetCallback(callback);

                if (CanUseActivityTransitions)
                {
                    try
                    {
                        currentActivity.StartActivityForResult(intent, CallbackActivityRequestCode, Android.App.ActivityOptions.MakeSceneTransitionAnimation(Context as AppCompatActivity).ToBundle());
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

                return;
            }

            if (CanUseActivityTransitions)
            {
                try
                {
                    Context.StartActivity(intent, Android.App.ActivityOptions.MakeSceneTransitionAnimation(Context as AppCompatActivity).ToBundle());
                }
                catch //REMARK: This is due to that this crashes on some devices even if the Android version supports this
                {
                    System.Diagnostics.Debug.WriteLine("Activity transitions are not working on this device. Transitions will be disabled.");
                    _useActivityTransitions = false;
                    Context.StartActivity(intent);
                }
            }
            else
            {
                Context.StartActivity(intent);
            }

        }

        public void NavigateBack(Action done = null)
        {
            if (Context is AppCompatActivity activity)
            {
                if (activity.FragmentManager?.BackStackEntryCount <= 1)
                {
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
                        activity.FragmentManager?.PopBackStackImmediate();
                    }
                    catch
                    {
                        // swallow exceptions, there's a bug in FragmentManager.java
                    }
                }

                done?.Invoke();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("AppNavigation.NavigateBack: Context is null or not an activity!");
            }
        }

        public void NavigateBack(Action<Guid> callbackAction, Guid payloadId, Action done = null)
        {
            if (Context is AppCompatActivity activity)
            {
                if (activity.FragmentManager?.BackStackEntryCount == 0)
                {
                    callbackAction.Invoke(payloadId);
                }
                else if (activity.FragmentManager?.BackStackEntryCount == 1)
                {
                    callbackAction.Invoke(payloadId);

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
                        activity.FragmentManager?.PopBackStackImmediate();
                    }
                    catch
                    {
                        // swallow exceptions, there's a bug in FragmentManager.java
                    }

                    callbackAction.Invoke(payloadId);
                }

                done?.Invoke();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("AppNavigation.NavigateBack: Context is null or not an activity!");
            }
        }

        public void NavigateBack<T>(Action done = null) where T : IBaseViewModel
        {
            if (Context is AppCompatActivity activity)
            {
                if (activity.FragmentManager?.BackStackEntryCount <= 1)
                {
                    if (_viewMapperDictionary.TryGetValue(typeof(T), out Type concreteType) == false)
                    {
                        throw new System.Exception($"The viewmodel '{typeof(T).ToString()}' does not exist in view mapper!");
                    }

                    //TODO: Implement scenario for when the target view is a fragment!

                    var concreteTypeJava = Class.FromType(concreteType);
                    var intent = new Intent(Context, concreteTypeJava);

                    intent.AddFlags(ActivityFlags.ClearTop);

                    Context.StartActivity(intent);
                }
                else
                {
                    //TODO: Implement for fragments!
                    System.Diagnostics.Debug.WriteLine("AppNavigation.NavigateBack<T>: Not implemented for fragments!");
                }

                done?.Invoke();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("AppNavigation.NavigateBack<T>: Context is null or not an activity!");
            }
        }

        public void NavigateBack<T>(Action<Guid> callbackAction, Guid payloadId, Action done = null) where T : IBaseViewModel
        {
            if (Context is AppCompatActivity activity)
            {
                if (activity.FragmentManager?.BackStackEntryCount <= 1)
                {
                    callbackAction.Invoke(payloadId);

                    if (_viewMapperDictionary.TryGetValue(typeof(T), out Type concreteType) == false)
                    {
                        throw new System.Exception($"The viewmodel '{typeof(T).ToString()}' does not exist in view mapper!");
                    }

                    //TODO: Implement scenario for when the target view is a fragment!

                    var concreteTypeJava = Class.FromType(concreteType);
                    var intent = new Intent(Context, concreteTypeJava);

                    intent.AddFlags(ActivityFlags.ClearTop);

                    Context.StartActivity(intent);
                }
                else
                {
                    //TODO: Implement for fragments!
                    System.Diagnostics.Debug.WriteLine("AppNavigation.NavigateBack<T>: Not implemented for fragments!");
                    //callbackAction.Invoke(payloadId);
                }

                done?.Invoke();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("AppNavigation.NavigateBack<T>: Context is null or not an activity!");
            }
        }

        public FragmentBase LoadFragment(Type concreteType, IPayload payload = null, Action<Guid> callback = null)
        {
            try
            {
                // Get the current activity
                var activity = Context as AppCompatActivity;
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
                    var intent = new Intent(Context, typeof(FragmentContainerActivity));

                    var activityPayload = Core.Mvvm.Api.Resolver.Resolve<IFragmentContainerPayload>();

                    activityPayload.FragmentType = concreteType;
                    activityPayload.FragmentPayload = payload;
                    activityPayload.FragmentCallback = callback;

                    intent.SetPayload(activityPayload);

                    Context.StartActivity(intent);

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