using System;
using Android.Content;
using MvvmMobile.Core.ViewModel;
using MvvmMobile.Droid.Model;
using MvvmMobile.Droid.Navigation;

namespace MvvmMobile.Droid.Common
{
    internal static class IntentExtensions
    {
        internal static void SetPayload(this Intent intent, IPayload payload)
        { 
            if (payload == null)
            {
                return;
            }

            AddPayloadToIntent(intent, AppNavigation.PayloadAppParameter, payload);
        }

        internal static void SetCallback(this Intent intent, Action<Guid> callbackAction)
        {
            if (callbackAction == null)
            {
                return;
            }

            var payload = Core.Mvvm.Api.Resolver.Resolve<ICallbackPayload>();

            payload.CallbackAction = callbackAction;

            AddPayloadToIntent(intent, AppNavigation.CallbackAppParameter, payload);
        }

        private static void AddPayloadToIntent(Intent intent, string parameterName, IPayload payload)
        {
            if (intent == null)
            {
                return;
            }

            // Set payload id
            var payloadId = Guid.NewGuid();

            intent.PutExtra(parameterName, payloadId.ToString());

            // Add payload
            var payloads = Core.Mvvm.Api.Resolver.Resolve<IPayloads>();

            payloads.Add(payloadId, payload);
        }
    }
}