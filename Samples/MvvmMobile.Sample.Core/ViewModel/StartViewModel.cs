using System;
using MvvmMobile.Core.Common;
using MvvmMobile.Core.Navigation;
using MvvmMobile.Core.ViewModel;
using MvvmMobile.Sample.Core.Model;
using XLabs.Ioc;

namespace MvvmMobile.Sample.Core.ViewModel
{
    public class StartViewModel : BaseViewModel, IStartViewModel
    {
        // Constructors
        public StartViewModel(INavigation navigation)
        {
            MoveNextCommand = new RelayCommand(o =>
            {
                var title = o as string;
                if (string.IsNullOrWhiteSpace(title))
                {
                    return;
                }

                var payload = Resolver.Resolve<ITitlePayload>();

                payload.Title = title;

                navigation.NavigateTo(typeof(ISecondViewModel), payload, NameSelected);
            });
        }


        // -----------------------------------------------------------------------------

        // Properties
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }


        // -----------------------------------------------------------------------------

        // Commands
        public RelayCommand MoveNextCommand { get; }


        // -----------------------------------------------------------------------------

        // Private Methods
        private void NameSelected(Guid payloadId)
        {
            // Get Payload
            var payloads = Resolver.Resolve<IPayloads>();
            var payload = payloads.GetAndRemove<INamePayload>(payloadId);
            if (payload == null)
            {
                return;
            }

            Name = payload.Name;
        }
    }
}