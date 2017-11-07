using System;
namespace MvvmMobile.Droid.Model
{
    internal sealed class CallbackPayload : ICallbackPayload
    {
        public Action<Guid> CallbackAction { get; set; }
    }
}