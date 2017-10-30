using MvvmMobile.Core.ViewModel;

namespace MvvmMobile.Sample.Core.Model
{
    public interface INamePayload : IPayload
    {
        string Name { get; set; }
    }
}