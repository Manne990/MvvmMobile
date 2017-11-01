using MvvmMobile.Core.ViewModel;

namespace MvvmMobile.Sample.Core.Model
{
    public interface IMotorcyclePayload : IPayload
    {
        IMotorcycle Motorcycle { get; set; }
    }
}