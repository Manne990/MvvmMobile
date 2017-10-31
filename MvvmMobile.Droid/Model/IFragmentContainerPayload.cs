using System;
using MvvmMobile.Core.ViewModel;

namespace MvvmMobile.Droid.Model
{
    internal interface IFragmentContainerPayload : IPayload
    {
        Type FragmentType { get; set; }
        IPayload FragmentPayload { get; set; }
    }
}