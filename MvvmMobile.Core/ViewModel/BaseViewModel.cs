using System;
using System.ComponentModel;
using MvvmMobile.Core.Navigation;
using XLabs.Ioc;

namespace MvvmMobile.Core.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        // Property Changed Notification
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        { 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        // -----------------------------------------------------------------------------

        // Lifecycle
        public virtual void OnLoaded()
        {}

        public virtual void OnActivated()
        {}

        public virtual void OnPaused()
        {}


        // -----------------------------------------------------------------------------

        // Callback Handling
        public Action<Guid> CallbackAction { private get; set; }

        protected void NavigateBack(Action done = null)
        {
            Resolver.Resolve<INavigation>().Pop(() => 
            {
                done?.Invoke();
            });
        }

        protected void NavigateBack(IPayload payload)
        {
            NavigateBack(() => 
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
            });
        }


        // -----------------------------------------------------------------------------

        // Protected Methods
        protected T LoadPayload<T>(Guid payloadId) where T : class
        {
            // Get Payload
            var payloads = Resolver.Resolve<IPayloads>();
            return payloads.GetAndRemove<T>(payloadId);
        }
    }
}