using System;
namespace MvvmMobile.Droid.Model
{
    public class CallbackPayload : ICallbackPayload
    {
        public Action<Guid> CallbackAction { get; set; }
    }
}