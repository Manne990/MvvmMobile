namespace MvvmMobile.Core.Common
{
    public interface IContainer
    {
        T Resolve<T>();
    }
}