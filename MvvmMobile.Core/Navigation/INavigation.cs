using System;
using System.Collections.Generic;
using MvvmMobile.Core.ViewModel;

namespace MvvmMobile.Core.Navigation
{
    public interface INavigation
    {
        void Init(Dictionary<Type, Type> viewMapper);
        void OpenPage(Type activityType, IPayload parameter = null, Action<Guid> callback = null);
        void PopAndOpenPage(Type popToActivityType, Type activityType);
        void GoHome(int activateTab, Action done = null);
        void GoHome(int activateTab, Type loadSubType, Action done = null);
        void Pop();
    }
}