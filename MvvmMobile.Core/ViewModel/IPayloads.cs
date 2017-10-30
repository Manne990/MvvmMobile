using System;
namespace MvvmMobile.Core.ViewModel
{
    public interface IPayloads
    {
        void Add (Guid id, IPayload payload);

        T GetAndRemove<T> (Guid id);
    }
}