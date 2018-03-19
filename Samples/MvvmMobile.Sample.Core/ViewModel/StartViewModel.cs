using System;
using System.Collections.ObjectModel;
using MvvmMobile.Core;
using MvvmMobile.Core.Common;
using MvvmMobile.Core.Navigation;
using MvvmMobile.Core.ViewModel;
using MvvmMobile.Sample.Core.Model;

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
                if (mc == null)
                {
                    return;
                }

                var payload = Mvvm.Api.Resolver.Resolve<IMotorcyclePayload>();

                payload.Motorcycle = mc;

                navigation.NavigateTo<IEditMotorcycleViewModel>(payload, MotorcycleChanged);
            });

            DeleteMotorcycleCommand = new RelayCommand<IMotorcycle>(mc =>
            {
                if (mc == null)
                {
                    return;
                }

                Motorcycles.Remove(mc);

                NotifyPropertyChanged(nameof(Motorcycles));
            });

            // Create Mock Data
            var motorcycles = new ObservableCollection<IMotorcycle>();

            motorcycles.Add(new Motorcycle { Id = Guid.NewGuid(), Brand = "Honda", Model = "VFR 800", Year = 1999 });
            motorcycles.Add(new Motorcycle { Id = Guid.NewGuid(), Brand = "Honda", Model = "VFR 800", Year = 2002 });
            motorcycles.Add(new Motorcycle { Id = Guid.NewGuid(), Brand = "Honda", Model = "VFR 800", Year = 2005 });
            motorcycles.Add(new Motorcycle { Id = Guid.NewGuid(), Brand = "KTM", Model = "625 SuperComp", Year = 2002 });
            motorcycles.Add(new Motorcycle { Id = Guid.NewGuid(), Brand = "KTM", Model = "690", Year = 2009 });
            motorcycles.Add(new Motorcycle { Id = Guid.NewGuid(), Brand = "KTM", Model = "990 SuperDuke", Year = 2005 });
            motorcycles.Add(new Motorcycle { Id = Guid.NewGuid(), Brand = "Yamaha", Model = "R1", Year = 1999 });
            motorcycles.Add(new Motorcycle { Id = Guid.NewGuid(), Brand = "Yamaha", Model = "R1", Year = 2003 });
            motorcycles.Add(new Motorcycle { Id = Guid.NewGuid(), Brand = "Yamaha", Model = "R1", Year = 2007 });
            motorcycles.Add(new Motorcycle { Id = Guid.NewGuid(), Brand = "Yamaha", Model = "R1", Year = 2015 });
            motorcycles.Add(new Motorcycle { Id = Guid.NewGuid(), Brand = "Yamaha", Model = "R6", Year = 2000 });
            motorcycles.Add(new Motorcycle { Id = Guid.NewGuid(), Brand = "Yamaha", Model = "R6", Year = 2005 });
            motorcycles.Add(new Motorcycle { Id = Guid.NewGuid(), Brand = "Yamaha", Model = "R6", Year = 2010 });
            motorcycles.Add(new Motorcycle { Id = Guid.NewGuid(), Brand = "Yamaha", Model = "R6", Year = 2011 });

            Motorcycles = motorcycles;

            Motorcycles.CollectionChanged += (sender, e) => 
            {
                System.Diagnostics.Debug.WriteLine("CollectionChanged!");
            };
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
            var payloads = Mvvm.Api.Resolver.Resolve<IPayloads>();
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