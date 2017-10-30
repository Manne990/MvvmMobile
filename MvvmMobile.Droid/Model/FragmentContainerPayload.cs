using System;
using MvvmMobile.Core.ViewModel;

namespace MvvmMobile.Droid.Model
{
    public class FragmentContainerPayload : IFragmentContainerPayload
    {
        public Type FragmentType { get; set; }
        public IPayload FragmentPayload { get; set; }
    }
}