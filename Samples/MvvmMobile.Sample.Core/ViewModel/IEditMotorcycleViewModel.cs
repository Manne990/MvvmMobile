using System;
using MvvmMobile.Sample.Core.Model;
using MvvmMobile.Core.Common;
using MvvmMobile.Core.ViewModel;

namespace MvvmMobile.Sample.Core.ViewModel
{
    public interface IEditMotorcycleViewModel : IPayloadViewModel
    {
        IMotorcycle Motorcycle { get; set; }

        RelayCommand SaveMotorcycleCommand { get; }
    }
}