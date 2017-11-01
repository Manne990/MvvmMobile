﻿using System;
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
            AddMotorcycleCommand = new RelayCommand(o =>
            {
                navigation.NavigateTo(typeof(IEditMotorcycleViewModel), null, MotorcycleAdded);
            });

            EditMotorcycleCommand = new RelayCommand(o =>
            {
                var mc = o as IMotorcycle;
                if (mc == null)
                {
                    return;
                }

                var payload = Resolver.Resolve<IMotorcyclePayload>();

                payload.Motorcycle = mc;

                navigation.NavigateTo(typeof(IEditMotorcycleViewModel), payload, MotorcycleChanged);
            });

            DeleteMotorcycleCommand = new RelayCommand(o =>
            {

            });
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
        public RelayCommand EditMotorcycleCommand { get; }
        public RelayCommand DeleteMotorcycleCommand { get; }


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
        }

        private void MotorcycleChanged(Guid payloadId)
        {
            NotifyPropertyChanged(nameof(Motorcycles));
        }
    }
}