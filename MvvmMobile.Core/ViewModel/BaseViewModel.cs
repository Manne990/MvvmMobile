using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using XLabs.Ioc;

namespace MvvmMobile.Core.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnLoaded()
        {}

        public virtual void OnActivated()
        {}

        public virtual void OnPaused()
        {}

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void NotifyPropertyChanged(string propertyName)
        { 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Action<Guid> CallbackAction { private get; set; }

        protected void RunCallback(IPayload payload)
        {
            if (CallbackAction == null)
            {
                return;
            }

            // Set payload id
            var payloadId = Guid.NewGuid();

            // Add payload
            var payloads = Resolver.Resolve<IPayloads>();
            payloads.Add(payloadId, payload);

            // Execute
            CallbackAction.Invoke(payloadId);
        }
    }
}