using System;
namespace MvvmMobile.Core.ViewModel
{
    public interface IPayloadViewModel : IBaseViewModel
    {
        void Load(Guid payloadId);
    }
}