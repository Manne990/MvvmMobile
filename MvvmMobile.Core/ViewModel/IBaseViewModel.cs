using System;
using System.ComponentModel;

namespace MvvmMobile.Core.ViewModel
{
    public interface IBaseViewModel : INotifyPropertyChanged
    {
        void OnLoaded();
        void OnActivated();
        void OnPaused();

        Action<Guid> CallbackAction { set; }
        void InitWithPayload(Guid payloadId);
    }
}