using System;
using MvvmMobile.Core.ViewModel;

namespace MvvmMobile.Droid.View
{
    internal static class ViewHelper
    {
        internal static T RecreateIfNeeded<T>(T vm, Guid payloadId) where T : class, IBaseViewModel
        {
            if (vm != null)
            {
                return vm;
            }

            vm = Core.Mvvm.Api.Resolver.Resolve<T>();
            if (vm == null)
            {
                return null;
            }

            vm?.InitWithPayload(payloadId);

            return null;
        }
    }
}