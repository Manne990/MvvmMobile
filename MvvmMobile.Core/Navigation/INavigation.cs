using System;
using System.Threading.Tasks;
using MvvmMobile.Core.ViewModel;

namespace MvvmMobile.Core.Navigation
{
    public enum BackBehaviour
    {
        SkipFromLastSubView,
        CloseLastSubView,
        FullViewsOnly,

    }
    public interface INavigation
    {
        void NavigateTo(Type viewModelType, IPayload parameter = null, Action<Guid> callback = null, bool clearHistory = false);
        void NavigateTo<T>(IPayload parameter = null, Action<Guid> callback = null, bool clearHistory = false) where T : IBaseViewModel;

        void NavigateToSubView(Type viewModelType, IPayload parameter = null, Action<Guid> callback = null, bool clearHistory = false);
        void NavigateToSubView<T>(IPayload parameter = null, Action<Guid> callback = null, bool clearHistory = false) where T : IBaseViewModel;

        void NavigateBack(Action done = null, BackBehaviour behaviour = BackBehaviour.CloseLastSubView);
        void NavigateBack(Action<Guid> callbackAction, Guid payloadId, Action done = null, BackBehaviour behaviour = BackBehaviour.CloseLastSubView);

        Task NavigateBack<T>() where T : IBaseViewModel;
        Task NavigateBack<T>(Action<Guid> callbackAction, Guid payloadId) where T : IBaseViewModel;
    }
}