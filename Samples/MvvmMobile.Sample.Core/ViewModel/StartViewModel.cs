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
        public StartViewModel(INavigation lindexApp)
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

                lindexApp.OpenPage(typeof(ISecondViewModel), payload, NameSelected);
            });
        }

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

        public RelayCommand MoveNextCommand { get; }

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