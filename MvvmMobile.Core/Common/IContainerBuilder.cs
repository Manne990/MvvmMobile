namespace MvvmMobile.Core.Common
{
    public interface IContainerBuilder
    {
        IResolver Resolver { get; }

        void Register<TInterface, TImplementation>() where TInterface : class where TImplementation : class, TInterface;
        void Register<TInterface>(TInterface instance) where TInterface : class;
        void RegisterSingleton<TInterface, TImplementation>() where TInterface : class where TImplementation : class, TInterface;

        void Build();
    }
}