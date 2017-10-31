using System;
using MvvmMobile.Core.Common;
using MvvmMobile.Core.ViewModel;
using MvvmMobile.Sample.Core.Model;
using XLabs.Ioc;

namespace MvvmMobile.Sample.Core.ViewModel
{
    public class SecondViewModel : BaseViewModel, ISecondViewModel
    {
        // Constructors
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

                NavigateBack(namePayload);
            });
        }


        // -----------------------------------------------------------------------------

        // Properties
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


        // -----------------------------------------------------------------------------

        // Commands
        public RelayCommand NameSelectedCommand { get; }


        // -----------------------------------------------------------------------------

        // Public Methods
        public void Load(Guid payloadId)
        {
            var payload = LoadPayload<ITitlePayload>(payloadId);
            if (payload == null)
            {
                return;
            }

            Title = payload.Title;
        }
    }
}