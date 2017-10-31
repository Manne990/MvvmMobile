using System;

namespace MvvmMobile.Droid.Model
{
    internal class LoadTabPayload : ILoadTabPayload
    {
        public int ActivateTab { get; set; }
        public Type LoadSubType { get; set; }
        public Action Done { get; set; }
    }
}