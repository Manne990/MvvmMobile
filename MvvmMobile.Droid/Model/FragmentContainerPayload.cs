using System;
using MvvmMobile.Core.ViewModel;

namespace MvvmMobile.Droid.Model
{
    internal class FragmentContainerPayload : IFragmentContainerPayload
    {
        public Type FragmentType { get; set; }
        public IPayload FragmentPayload { get; set; }
    }
}