using System;
using MvvmMobile.Core.ViewModel;

namespace MvvmMobile.Droid.Model
{
    internal interface ILoadTabPayload : IPayload
    {
        int ActivateTab { get; set; }
        Type LoadSubType { get; set; }
        Action Done { get; set; }
    }
}