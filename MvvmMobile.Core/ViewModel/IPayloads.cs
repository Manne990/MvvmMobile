using System;
namespace MvvmMobile.Core.ViewModel
{
    public interface IPayloads
    {
        void Add(Guid id, IPayload payload);
        void Remove(Guid id);
        T Get<T>(Guid id);
    }
}