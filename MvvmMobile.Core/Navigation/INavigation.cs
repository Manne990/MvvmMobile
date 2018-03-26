using System;
using MvvmMobile.Core.ViewModel;

namespace MvvmMobile.Core.Navigation
{
    public interface INavigation
    {
        void NavigateTo(Type viewModelType, IPayload parameter = null, Action<Guid> callback = null);
        void NavigateTo<T>(IPayload parameter = null, Action<Guid> callback = null) where T : IBaseViewModel;

        void NavigateBack(Action done = null);
        void NavigateBack(Action<Guid> callbackAction, Guid payloadId, Action done = null);

        void NavigateBack<T>(Action done = null);
        void NavigateBack<T>(Action<Guid> callbackAction, Guid payloadId, Action done = null);
    }
}