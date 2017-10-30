using System;

namespace MvvmMobile.Droid.Model
{
    public class LoadTabPayload : ILoadTabPayload
    {
        public int ActivateTab { get; set; }
        public Type LoadSubType { get; set; }
        public Action Done { get; set; }
    }
}