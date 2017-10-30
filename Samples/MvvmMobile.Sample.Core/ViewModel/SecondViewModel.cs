using System;
using MvvmMobile.Core.Common;
using MvvmMobile.Core.ViewModel;
using MvvmMobile.Sample.Core.Model;
using XLabs.Ioc;

namespace MvvmMobile.Sample.Core.ViewModel
{
    public class SecondViewModel : BaseViewModel, ISecondViewModel
    {
        public SecondViewModel()
        {
            NameSelectedCommand = new RelayCommand(o =>
            {
                var name = o as string;
                if (string.IsNullOrWhiteSpace(name))
                {
                    return;
                }

                var namePayload = Resolver.Resolve<INamePayload>();

                namePayload.Name = name;

                RunCallback(namePayload);
            });
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                NotifyPropertyChanged(nameof(Title));
            }
        }

        public RelayCommand NameSelectedCommand { get; }

        public void Load(Guid payloadId)
        {
            // Get Payload
            var payloads = Resolver.Resolve<IPayloads>();
            var payload = payloads.GetAndRemove<ITitlePayload>(payloadId);
            if (payload == null)
            {
                return;
            }

            Title = payload.Title;
        }
    }
}