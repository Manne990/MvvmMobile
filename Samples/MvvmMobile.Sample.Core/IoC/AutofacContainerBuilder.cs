using Autofac;

namespace MvvmMobile.Sample.Core.IoC
{
    public class AutofacContainerBuilder : MvvmMobile.Core.Common.IContainerBuilder
    {
        private readonly ContainerBuilder _containerBuilder;

        public AutofacContainerBuilder()
        {
            _containerBuilder = new ContainerBuilder();
        }

        public MvvmMobile.Core.Common.IResolver Resolver { get; private set; }

        public void Register<TInterface>(TInterface instance) where TInterface : class
        {
            _containerBuilder?.RegisterInstance(instance).As<TInterface>();
        }

        public void Register<TInterface, TImplementation>()
            where TInterface : class
            where TImplementation : class, TInterface
        {
            _containerBuilder?.RegisterType<TImplementation>()?.As<TInterface>();
        }

        public void RegisterSingleton<TInterface, TImplementation>()
            where TInterface : class
            where TImplementation : class, TInterface
        {
            _containerBuilder?.RegisterType<TImplementation>()?.As<TInterface>()?.SingleInstance();
        }

        public void Build()
        {
            var container = _containerBuilder.Build();

            Resolver = new AutofacResolver(container);
        }
    }
}