using System;
namespace MvvmMobile.Droid.Model
{
    internal class CallbackPayload : ICallbackPayload
    {
        public Action<Guid> CallbackAction { get; set; }
    }
}