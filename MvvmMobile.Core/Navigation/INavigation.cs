using System;
using System.Collections.Generic;
using MvvmMobile.Core.ViewModel;

namespace MvvmMobile.Core.Navigation
{
    public interface INavigation
    {
        void Init(Dictionary<Type, Type> viewMapper);

        void NavigateTo(Type viewModelType, IPayload parameter = null, Action<Guid> callback = null);
        void NavigateBack(Action done = null);
        void NavigateBack(Action<Guid> callbackAction, Guid payloadId, Action done = null);
    }
}