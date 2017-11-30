using System;
using MvvmMobile.Core.Common;
using MvvmMobile.Core.ViewModel;

namespace MvvmMobile.Core
{
    public sealed class Mvvm
    {
        private static volatile Mvvm instance;
        private static object syncRoot = new Object();

        private Mvvm() {}

        public static Mvvm Api
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new Mvvm();
                        }
                    }
                }

                return instance;
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