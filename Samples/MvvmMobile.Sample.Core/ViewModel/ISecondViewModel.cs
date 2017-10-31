using System;
using MvvmMobile.Core.Common;
using MvvmMobile.Core.ViewModel;

namespace MvvmMobile.Sample.Core.ViewModel
{
    public interface ISecondViewModel : IPayloadViewModel
    {
        string Title { get; }

        RelayCommand NameSelectedCommand { get; }

        void Load(Guid payloadId);
    }
}