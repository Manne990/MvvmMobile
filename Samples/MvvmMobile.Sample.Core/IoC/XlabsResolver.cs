namespace MvvmMobile.Sample.Core.IoC
{
    public class XlabsResolver : MvvmMobile.Core.Common.IResolver
    {
        public T Resolve<T>() where T : class
        {
            return XLabs.Ioc.Resolver.Resolve<T>();
        }
    }
}