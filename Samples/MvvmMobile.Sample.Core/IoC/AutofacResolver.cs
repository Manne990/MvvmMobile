using Autofac;

namespace MvvmMobile.Sample.Core.IoC
{
    public class AutofacResolver : MvvmMobile.Core.Common.IResolver
    {
        private readonly IContainer _container;

        public AutofacResolver(IContainer container)
        {
            _container = container;
        }

        public T Resolve<T>() where T : class
        {
            return _container.Resolve<T>();
        }
    }
}