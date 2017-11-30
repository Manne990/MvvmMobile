namespace MvvmMobile.Sample.Tests
{
    public class TestResolver : MvvmMobile.Core.Common.IResolver
    {
        public bool IsRegistered<T>() where T : class
        {
            return true;
        }

        public T Resolve<T>() where T : class
        {
            return XLabs.Ioc.Resolver.Resolve<T>();
        }
    }
}