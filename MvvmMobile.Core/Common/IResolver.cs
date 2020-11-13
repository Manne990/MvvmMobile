using System;

namespace MvvmMobile.Core.Common
{
    public interface IResolver
    {
        T Resolve<T>() where T : class;
        object Resolve(Type interfaceType);

        bool IsRegistered<T>() where T : class;
    }
}