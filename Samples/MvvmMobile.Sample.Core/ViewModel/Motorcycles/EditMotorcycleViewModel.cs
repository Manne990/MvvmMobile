﻿using System;
using System.Threading.Tasks;
using MvvmMobile.Core;
using MvvmMobile.Core.Common;
using MvvmMobile.Core.Navigation;
using MvvmMobile.Core.ViewModel;
using MvvmMobile.Sample.Core.Model;
using MvvmMobile.Sample.Core.Navigation;

namespace MvvmMobile.Sample.Core.ViewModel.Motorcycles
{
    public class EditMotorcycleViewModel : BaseViewModel, IEditMotorcycleViewModel
    {
        // Constructors
        public EditMotorcycleViewModel()
        {
            CancelCommand = new RelayCommand(() => 
            {
                NavigateBack(payload: null);
                //navigation?.NavigateBack<IStartViewModel>();
                //navigation?.NavigateToRoot();
                //NavigateBack();
            });

            SaveMotorcycleCommand = new RelayCommand(() =>
            {
                var mcPayload = Mvvm.Api.Resolver.Resolve<IMotorcyclePayload>();

                mcPayload.Motorcycle = _motorcycle;

                NavigateBack(payload: mcPayload);
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
                Task.Factory.StartNew(async () =>
                {
                    await Task.Delay(1);
                    Motorcycle = new Motorcycle { Id = Guid.NewGuid() };
                });
                return;
            }

            Task.Factory.StartNew(async () =>
            {
                await Task.Delay(1);
                Motorcycle = payload.Motorcycle;
            });
        }
    }
}