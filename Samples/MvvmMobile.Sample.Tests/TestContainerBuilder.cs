namespace MvvmMobile.Sample.Tests
{
    public class TestContainerBuilder : MvvmMobile.Core.Common.IContainerBuilder
    {
        private readonly XLabs.Ioc.IDependencyContainer _container;

        public TestContainerBuilder(XLabs.Ioc.IResolver resolver)
        {
            XLabs.Ioc.Resolver.ResetResolver(resolver);

            _container = XLabs.Ioc.Resolver.Resolve<XLabs.Ioc.IDependencyContainer>();

            Resolver = new TestResolver();
        }

        public MvvmMobile.Core.Common.IResolver Resolver { get; private set; }

        public void Build()
        {
            throw new System.NotImplementedException();
        }

        public void Register<TInterface, TImplementation>()
            where TInterface : class
            where TImplementation : class, TInterface
        {
            _container.Register<TInterface, TImplementation>();
        }

        public void Register<TInterface>(TInterface instance) where TInterface : class
        {
            _container.Register(instance);
        }

        public void RegisterSingleton<TInterface, TImplementation>()
            where TInterface : class
            where TImplementation : class, TInterface
        {
            _container.RegisterSingle<TInterface, TImplementation>();
        }
    }
}