using System;
using MvvmMobile.Core.Common;
using MvvmMobile.Core.ViewModel;
using MvvmMobile.Sample.Core.Model;
using XLabs.Ioc;

namespace MvvmMobile.Sample.Core.ViewModel
{
    public class EditMotorcycleViewModel : BaseViewModel, IEditMotorcycleViewModel
    {
        // Constructors
        public EditMotorcycleViewModel()
        {
            CancelCommand = new RelayCommand(() => 
            {
                NavigateBack();
            });

            SaveMotorcycleCommand = new RelayCommand(() =>
            {
                var mcPayload = Resolver.Resolve<IMotorcyclePayload>();

                mcPayload.Motorcycle = _motorcycle;

                NavigateBack(mcPayload);
            });
        }


        // -----------------------------------------------------------------------------

        // Properties
        private IMotorcycle _motorcycle;
        public IMotorcycle Motorcycle
        {
            get { return _motorcycle; }
            set
            {
                _motorcycle = value;
                NotifyPropertyChanged(nameof(Motorcycle));
            }
        }


        // -----------------------------------------------------------------------------

        // Commands
        public RelayCommand CancelCommand { get; }
        public RelayCommand SaveMotorcycleCommand { get; }


        // -----------------------------------------------------------------------------

        // Public Methods
        public override void InitWithPayload(Guid payloadId)
        {
            base.InitWithPayload(payloadId);

            var payload = LoadPayload<IMotorcyclePayload>(payloadId);
            if (payload == null)
            {
                Motorcycle = new Motorcycle { Id = Guid.NewGuid() };
                return;
            }

            Motorcycle = payload.Motorcycle;
        }
    }
}