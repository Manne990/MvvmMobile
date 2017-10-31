using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Widget;
using Java.Lang;
using MvvmMobile.Core.Navigation;
using MvvmMobile.Core.ViewModel;
using MvvmMobile.Droid.Common;
using MvvmMobile.Droid.Model;
using MvvmMobile.Droid.View;
using XLabs.Ioc;

namespace MvvmMobile.Droid.Navigation
{
    public class AppNavigation : INavigation
    {
        // Constants
        internal static int CallbackActivityRequestCode = 9999;
        internal static string PayloadAppParameter = "MvvmMobile-PayloadAppParameter";
        internal static string CallbackAppParameter = "MvvmMobile-CallbackAppParameter";


        // -----------------------------------------------------------------------------

        // Private Members
        private Dictionary<Type, Type> _viewMapperDictionary;


        // -----------------------------------------------------------------------------

        // Public Properties
        public Context Context { get; set; }
        public int FragmentContainerId { private get; set; }


        // -----------------------------------------------------------------------------

        // Public Methods
        public void Init(Dictionary<Type, Type> viewMapper)
        {
            _viewMapperDictionary = viewMapper;
        }

        public void NavigateTo(Type activityType, IPayload parameter = null, Action<Guid> callback = null)
        {
            if (activityType == null)
            {
                return;
            }

            Type concreteType;
            if (_viewMapperDictionary.TryGetValue(activityType, out concreteType) == false)
            {
                //TODO: Handle Error!
                return;
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
                var currentActivity = Context as ActivityBase;
                if (currentActivity == null)
                {
                    //TODO: Handle Error!
                    return;
                }

                intent.SetCallback(callback);

                currentActivity.StartActivityForResult(intent, CallbackActivityRequestCode);

                return;
            }

            Context.StartActivity(intent);
        }

        public void Pop(Action done)
        {
            try
            {
                ((Activity) Context).FragmentManager.PopBackStackImmediate();
            }
            catch
            {
                // swallow exceptions, there's a bug in FragmentManager.java
            }
        }

        public void GoHome(int activateTab, Action done = null)
        {
            // Clear all activities
            ((Activity)Context).FinishAffinity();

            // Prepare the payload
            var payload = Resolver.Resolve<ILoadTabPayload>();

            payload.ActivateTab = activateTab;
            payload.LoadSubType = null;
            payload.Done = done;

            // Open main menu activity

            //TODO: Find abstract solution for this!

            //OpenPage(typeof(IMainMenuViewModel), payload);
        }

        public void GoHome(int activateTab, Type loadSubType, Action done = null)
        {
            // Clear all activities
            ((Activity)Context).FinishAffinity();

            // Prepare the payload
            var payload = Resolver.Resolve<ILoadTabPayload>();

            payload.ActivateTab = activateTab;
            payload.LoadSubType = loadSubType;
            payload.Done = done;

            // Open main menu activity

            //TODO: Find abstract solution for this!

            //OpenPage(typeof(IMainMenuViewModel), payload);
        }

        public void PopAndOpenPage(Type popToActivityType, Type activityType)
        {
            Type popConcreteType;
            if (_viewMapperDictionary.TryGetValue(popToActivityType, out popConcreteType) == false)
            {
                //TODO: Handle Error!
                return;
            }

            if (popConcreteType.IsSubclassOf(typeof(Fragment)))
            {
                throw new ArgumentException("PopAndOpenPage: Fragments not allowed!");
            }

            // Open the pop to activity
            var popIntent = new Intent(Context, popConcreteType);

            popIntent.AddFlags(ActivityFlags.ClearTop);

            Context.StartActivity(popIntent);

            if (activityType == null)
            {
                return;
            }

            Type concreteType;
            if (_viewMapperDictionary.TryGetValue(activityType, out concreteType) == false)
            {
                //TODO: Handle Error!
                return;
            }

            if (concreteType.IsSubclassOf(typeof(Fragment)))
            {
                throw new ArgumentException("PopAndOpenPage: Fragments not allowed!");
            }

            // Open the target activity
            var intent = new Intent(Context, concreteType);

            Context.StartActivity(intent);
        }

        public FragmentBase LoadFragment(Type concreteType, IPayload payload = null, Action<Guid> callback = null)
        {
            try
            {
                // Get the current activity
                var activity = (Activity)Context;

                // Check if the activity has a fragment container
                var fragmentContainer = activity.FindViewById<FrameLayout>(FragmentContainerId);
                if (fragmentContainer == null)
                {
                    // No container -> Use generic fragment container activity
                    var intent = new Intent(Context, typeof(FragmentContainerActivity));

                    var activityPayload = Resolver.Resolve<IFragmentContainerPayload>();

                    activityPayload.FragmentType = concreteType;
                    activityPayload.FragmentPayload = payload;

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
                var ft = activity.FragmentManager.BeginTransaction();

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