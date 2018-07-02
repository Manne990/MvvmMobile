# MvvmMobile #
## Introduction ##
MvvmMobile is an MVVM framework developed for Xamarin iOS and Xamarin Android with a focus on abstracted navigation. This ensures high testability of the viewmodel classes.

## Contributors ##
Mikael Stalvik  
Jonas Frid

## NuGet ##
https://www.nuget.org/packages/MvvmMobile.Core/   
https://www.nuget.org/packages/MvvmMobile.Droid/   
https://www.nuget.org/packages/MvvmMobile.iOS/

## Getting Started ##
- Create a .NET Standard project for the shared code
- Setup the IoC Container
- Create a Xamarin iOS project
- Create a Xamarin Android project

## Create the shared project ##
- Create a .NET Standard (version 2.0 or later) project for your shared code
- Add the NuGet package MvvmMobile.Core (https://www.nuget.org/packages/MvvmMobile.Core/)
- Make sure that all viewmodel interfaces inherit from IBaseViewModel
- Make sure that all viewmodel classes inherit from BaseViewModel

When navigating from one viewmodel to another, resolve INavigation and call the method NavigateTo.
```
var navigation = Mvvm.Api.Resolver.Resolve<INavigation>();
navigation.NavigateTo<IMySecondViewModel>(payload, ReturnAction);
```
The 'payload' is an instance of a class that implements IPayload and this is a way to pass a payload of data to the receiving viewmodel.

The 'ReturnAction' is an Action that is called (with an optional payload) when navigating back to the viewmodel (a callback).

To return to the previous viewmodel and optionally pass a payload, just call NavigateBack from the viewmodel.
```
var payload = Mvvm.Api.Resolver.Resolve<ISomePayload>();
payload.SomeData = someData;
NavigateBack(payload);
```

## Setup the IoC Container ##
MvvmMobile does not contain an IoC container. This has been abstracted away and enables you to use the IoC solution of your choosing!

First, create a class that implements MvvmMobile.Core.Common.IResolver.   
Example with Autofac:
```
public class AutofacResolver : MvvmMobile.Core.Common.IResolver
{
    private readonly IContainer _container;

    public AutofacResolver(IContainer container)
    {
        _container = container;
    }

    public bool IsRegistered<T>() where T : class
    {
        return _container.IsRegistered<T>();
    }

    public T Resolve<T>() where T : class
    {
        if (IsRegistered<T>() == false)
        {
            return default(T);
        }

        return _container.Resolve<T>();
    }
}
```

Then, create a class that implements MvvmMobile.Core.Common.IContainerBuilder.   
Example with Autofac:
```
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
```

Finally, create a class that creates the container builder and register all common core types.   
Example:
```
public static class Bootstrapper
{
    public static IContainerBuilder Init()
    {
        var builder = new AutofacContainerBuilder();

        builder.RegisterSingleton<IMyFirstViewModel, MyFirstViewModel>();
        builder.RegisterSingleton<IMySecondViewModel, MySecondViewModel>();
        builder.RegisterSingleton<IMyFirstService, MyFirstService>();

        return builder;
    }
}
```

## Create the Xamarin iOS project ##
- Create a Xamarin iOS project
- Add a reference to your shared project
- Add the NuGet package MvvmMobile.iOS (https://www.nuget.org/packages/MvvmMobile.iOS/)
- Create your view controllers

All view controllers must inherit from ViewControllerBase or one of the other view controller base classes provided by MvvmMobile.

If you use storyboards then you need to add the Storyboard attribute to your view controller class.
```
[Storyboard(storyboardName:"TheNameOfTheStoryBoard", storyboardId:"TheIdOfTheViewControllerInTheStoryboard")]
public partial class MyFirstController : ViewControllerBase<IMyFirstViewModel>
```

Your view controllers can override ViewModel_PropertyChanged to be notified of property changes in the connected view model.
```
protected override void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
{
    if (e.PropertyName == nameof(ViewModel.SomeProperty))
    {
        SomeTextField.Text = ViewModel.SomeProperty;
    }
}
```

In AppDelegate.cs and the FinishedLaunching method. Initialize MvvmMobile with the IoC container builder instance created above and the mapping between your viewmodels and your view controllers.
```
var builder = Core.Bootstrapper.Init(); // The Init method of your Bootstrapper class created in your shared project above

//TODO: Register iOS specific types here...

MvvmMobile.iOS.Bootstrapper.SetupIoC(builder);

builder.Build();

MvvmMobile.iOS.Bootstrapper.Init(new Dictionary<Type, Type>
{
    { typeof(IMyFirstViewModel), typeof(MyFirstController) },
    { typeof(IMySecondViewModel), typeof(MySecondViewController) }
});
```

## Create the Xamarin Android project ##
- Create a Xamarin Android project
- Add a reference to your shared project
- Add the NuGet package MvvmMobile.Droid (https://www.nuget.org/packages/MvvmMobile.Droid/)
- Create your activities and fragments

All activities must inherit from ActivityBase and all fragments must inherit from FragmentBase.

Your activities/fragments can override ViewModel_PropertyChanged to be notified of property changes in the connected view model.
```
protected override void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
{
    if (e.PropertyName == nameof(ViewModel.SomeProperty))
    {
        SomeEditText.Text = ViewModel.SomeProperty;
    }
}
```

In your application class. Initialize MvvmMobile with the IoC container builder instance created above and the mapping between your viewmodels and your activities/fragments.
```
var builder = Core.Bootstrapper.Init(); // The Init method of your Bootstrapper class created in your shared project above

//TODO: Register Android specific types here...

MvvmMobile.Droid.Bootstrapper.SetupIoC(builder);

builder.Build();
            
MvvmMobile.Droid.Bootstrapper.Init(new Dictionary<Type, Type>
{
    { typeof(IMyFirstViewModel), typeof(MyFirstActivity) },
    { typeof(IMySecondViewModel), typeof(SomeFragment) }
});
```

## Samples ##
For a more detailed view of MvvmMobile, have a look at the sample project included in this repo!
Happy coding!
