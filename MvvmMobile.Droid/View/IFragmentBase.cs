using System;
using Android.App;
using MvvmMobile.Core.ViewModel;

namespace MvvmMobile.Droid.View
{
    public interface IFragmentBase
    {
        string Title { get; }

        void SetPayload(IPayload payload);
        void SetCallback(Action<Guid> callbackAction);
        Fragment AsFragment();
    }
}