using System;
using MvvmMobile.Core.ViewModel;

namespace MvvmMobile.Droid.Model
{
    public interface IFragmentContainerPayload : IPayload
    {
        Type FragmentType { get; set; }
        IPayload FragmentPayload { get; set; }
    }
}