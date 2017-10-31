using System;
using MvvmMobile.Core.ViewModel;

namespace MvvmMobile.Droid.Model
{
    internal interface ICallbackPayload : IPayload
    {
        Action<Guid> CallbackAction { get; set; }
    }
}