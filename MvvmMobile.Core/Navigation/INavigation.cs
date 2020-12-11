using System;
using System.Threading.Tasks;
using MvvmMobile.Core.ViewModel;

namespace MvvmMobile.Core.Navigation
{
    public enum BackBehaviour
    {
        /// <summary>Dismiss the container view controller/activity when going back from the only subview in the subview navigation stack.</summary>
        SkipFromLastSubView,
        /// <summary>Only dismiss the subview when going back, don't dismiss the container view controller/activity.</summary>
        CloseLastSubView,
        /// <summary>Always dismiss the container view controller/activity and all of its subviews when going back.</summary>
        FullViewsOnly
    }

    public interface INavigation
    {
        void NavigateTo(Type viewModelType, IPayload parameter = null, Action<Guid> callback = null, bool clearHistory = false, bool animated = true, bool transitions = true);
        void NavigateTo<T>(IPayload parameter = null, Action<Guid> callback = null, bool clearHistory = false, bool animated = true, bool transitions = true) where T : IBaseViewModel;

        void NavigateToSubView(Type viewModelType, IPayload parameter = null, Action<Guid> callback = null, bool clearHistory = false);
        void NavigateToSubView<T>(IPayload parameter = null, Action<Guid> callback = null, bool clearHistory = false) where T : IBaseViewModel;

        void NavigateBack(Action done = null, BackBehaviour behaviour = BackBehaviour.CloseLastSubView, bool animated = true);
        void NavigateBack(Action<Guid> callbackAction, Guid payloadId, Action done = null, BackBehaviour behaviour = BackBehaviour.CloseLastSubView, bool animated = true);

        Task NavigateBack<T>() where T : IBaseViewModel;
        Task NavigateBack<T>(Action<Guid> callbackAction, Guid payloadId) where T : IBaseViewModel;

        Task NavigateBack(Type viewModelInterfaceType);
        Task NavigateBack(Type viewModelInterfaceType, Action<Guid> callbackAction, Guid payloadId);
    }
}