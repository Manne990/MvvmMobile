using System;
using MvvmMobile.Core.ViewModel;

namespace MvvmMobile.Droid.Model
{
    public interface ICallbackPayload : IPayload
    {
        Action<Guid> CallbackAction { get; set; }
    }
}