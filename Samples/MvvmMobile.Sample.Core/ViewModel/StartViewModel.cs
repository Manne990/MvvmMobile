using System;
using System.Collections.ObjectModel;
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
            Motorcycles = new ObservableCollection<IMotorcycle>();

            AddMotorcycleCommand = new RelayCommand(() =>
            {
                navigation.NavigateTo<IEditMotorcycleViewModel>(null, MotorcycleAdded);
            });

            EditMotorcycleCommand = new RelayCommand<IMotorcycle>(mc =>
            {
                var payload = Resolver.Resolve<IMotorcyclePayload>();

                payload.Motorcycle = mc;

                navigation.NavigateTo<IEditMotorcycleViewModel>(payload, MotorcycleChanged);
            });

            DeleteMotorcycleCommand = new RelayCommand<IMotorcycle>(mc =>
            {
                Motorcycles.Remove(mc);

                NotifyPropertyChanged(nameof(Motorcycles));
            });

            Motorcycles.Add(new Motorcycle { Id = Guid.NewGuid(), Brand = "Yamaha", Model = "R1", Year = 2007 });
        }


        // -----------------------------------------------------------------------------

        // Properties
        private ObservableCollection<IMotorcycle> _motorcycles;
        public ObservableCollection<IMotorcycle> Motorcycles
        {
            get { return _motorcycles; }
            set
            {
                _motorcycles = value;
                NotifyPropertyChanged(nameof(Motorcycles));
            }
        }


        // -----------------------------------------------------------------------------

        // Commands
        public RelayCommand AddMotorcycleCommand { get; }
        public RelayCommand<IMotorcycle> EditMotorcycleCommand { get; }
        public RelayCommand<IMotorcycle> DeleteMotorcycleCommand { get; }


        // -----------------------------------------------------------------------------

        // Private Methods
        private void MotorcycleAdded(Guid payloadId)
        {
            // Get Payload
            var payloads = Resolver.Resolve<IPayloads>();
            var payload = payloads.GetAndRemove<IMotorcyclePayload>(payloadId);
            if (payload?.Motorcycle == null)
            {
                return;
            }

            Motorcycles?.Add(payload.Motorcycle);

            NotifyPropertyChanged(nameof(Motorcycles));
        }

        private void MotorcycleChanged(Guid payloadId)
        {
            NotifyPropertyChanged(nameof(Motorcycles));
        }
    }
}