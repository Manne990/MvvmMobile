using System;
using MvvmMobile.Core.Common;
using MvvmMobile.Core.ViewModel;

namespace MvvmMobile.Core
{
    public sealed class Mvvm
    {
        private static volatile Mvvm _instance;
        private static readonly object _syncRoot = new Object();

        private Mvvm() {}

        public static Mvvm Api
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new Mvvm();
                        }
                    }
                }

                return _instance;
            }
        }

        public IResolver Resolver { get; private set; }

        public void SetupIoC(IContainerBuilder container)
        {
            container.RegisterSingleton<IPayloads, Payloads>();
        }

        public void Init(IContainerBuilder container)
        {
            Resolver = container.Resolver;
        }
    }
}