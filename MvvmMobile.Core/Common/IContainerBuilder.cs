using System;

namespace MvvmMobile.Core.Common
{
    public interface IContainerBuilder
    {
        void Register<TInterface, TImplementation>();
        void Register<TInterface>(TInterface instance) where TInterface : class;
        void RegisterSingleton<TInterface>(TInterface instance) where TInterface : class;
        void RegisterSingleton<TInterface, TImplementation>();
        void RegisterGeneric(Type interfaceType, Type implentation);

        IContainer Build();
    }
}