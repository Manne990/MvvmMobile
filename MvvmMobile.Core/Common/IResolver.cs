namespace MvvmMobile.Core.Common
{
    public interface IResolver
    {
        T Resolve<T>();
    }
}