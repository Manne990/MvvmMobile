using System;
using System.Collections.ObjectModel;
using MvvmMobile.Core;
using MvvmMobile.Core.Common;
using MvvmMobile.Core.ViewModel;
using MvvmMobile.Sample.Core.Model;
using MvvmMobile.Sample.Core.Navigation;
using MvvmMobile.Sample.Core.ViewModel.Navigation;

namespace MvvmMobile.Sample.Core.ViewModel.Motorcycles
{
    public class StartViewModel : BaseViewModel, IStartViewModel
    {
        // Constructors
        public StartViewModel(ICustomNavigation navigation)
        {
            AddMotorcycleCommand = new RelayCommand(() =>
            {
                navigation.NavigateToSubView<IEditMotorcycleViewModel>(null, MotorcycleAdded);
                IsShowingEditMotorcycleSubView = true;
            });

            EditMotorcycleCommand = new RelayCommand<IMotorcycle>(mc =>
            {
                if (mc == null)
                {
                    return;
                }

                var payload = Mvvm.Api.Resolver.Resolve<IMotorcyclePayload>();

                payload.Motorcycle = mc;

                //navigation.NavigateTo<IEditMotorcycleViewModel>(payload, MotorcycleChanged);

                navigation.NavigateToSubView<IEditMotorcycleViewModel>(payload, MotorcycleChanged);
                IsShowingEditMotorcycleSubView = true;
            });

            DeleteMotorcycleCommand = new RelayCommand<IMotorcycle>(mc =>
            {
                if (mc == null)
                {
                    return;
                }

                Motorcycles?.Remove(mc);

                NotifyPropertyChanged(nameof(Motorcycles));
            });

            StartNavigationDemoCommand = new RelayCommand(() => 
            {
                navigation.NavigateTo<INav1ViewModel>();
            });

            // Create Mock Data
            Motorcycles = new ObservableCollection<IMotorcycle>
            {
                new Motorcycle { Id = Guid.NewGuid(), Brand = "Honda", Model = "VFR 800", Year = 1999 },
                new Motorcycle { Id = Guid.NewGuid(), Brand = "Honda", Model = "VFR 800", Year = 2002 },
                new Motorcycle { Id = Guid.NewGuid(), Brand = "Honda", Model = "VFR 800", Year = 2005 },
                new Motorcycle { Id = Guid.NewGuid(), Brand = "KTM", Model = "625 SuperComp", Year = 2002 },
                new Motorcycle { Id = Guid.NewGuid(), Brand = "KTM", Model = "690", Year = 2009 },
                new Motorcycle { Id = Guid.NewGuid(), Brand = "KTM", Model = "990 SuperDuke", Year = 2005 },
                new Motorcycle { Id = Guid.NewGuid(), Brand = "Yamaha", Model = "R1", Year = 1999 },
                new Motorcycle { Id = Guid.NewGuid(), Brand = "Yamaha", Model = "R1", Year = 2003 },
                new Motorcycle { Id = Guid.NewGuid(), Brand = "Yamaha", Model = "R1", Year = 2007 },
                new Motorcycle { Id = Guid.NewGuid(), Brand = "Yamaha", Model = "R1", Year = 2015 },
                new Motorcycle { Id = Guid.NewGuid(), Brand = "Yamaha", Model = "R6", Year = 2000 },
                new Motorcycle { Id = Guid.NewGuid(), Brand = "Yamaha", Model = "R6", Year = 2005 },
                new Motorcycle { Id = Guid.NewGuid(), Brand = "Yamaha", Model = "R6", Year = 2010 },
                new Motorcycle { Id = Guid.NewGuid(), Brand = "Yamaha", Model = "R6", Year = 2011 }
            };
        }


        // -----------------------------------------------------------------------------

        // Properties
        private bool _isShowingEditMotorcycleSubView;
        public bool IsShowingEditMotorcycleSubView
        {
            get { return _isShowingEditMotorcycleSubView; }
            set
            {
                _isShowingEditMotorcycleSubView = value;
                NotifyPropertyChanged(nameof(IsShowingEditMotorcycleSubView));
            }
        }

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
        public RelayCommand StartNavigationDemoCommand { get; }


        // -----------------------------------------------------------------------------

        // Private Methods
        private void MotorcycleAdded(Guid payloadId)
        {
            IsShowingEditMotorcycleSubView = false;

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
            IsShowingEditMotorcycleSubView = false;
            NotifyPropertyChanged(nameof(Motorcycles));
        }
    }
}