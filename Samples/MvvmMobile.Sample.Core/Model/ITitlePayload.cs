using MvvmMobile.Core.ViewModel;

namespace MvvmMobile.Sample.Core.Model
{
    public interface ITitlePayload : IPayload
    {
        string Title { get; set; }
    }
}