# MvvmMobile #
## Introduction ##
MvvmMobile is an MVVM framework developed for Xamarin iOS and Xamarin Android with a focus on abstracted navigation. This ensures high testability of the viewmodel classes.

## Getting Started ##
- Create a PCL project for your shared code and add a reference to MvvmMobile.Core
- MvvmMobiles uses XLabs.IoC so go ahead and add that NuGet package
- Make sure that all viewmodel interfaces inherit from IPayloadViewModel
- Make sure that all viewmodel classes inherit from BaseViewModel

When navigating from one viewmodel to another, resolve INavigation and call the method NavigateTo.
```
var payload = Resolver.Resolve<INavigation>();
navigation.NavigateTo(typeof(IEditMotorcycleViewModel), payload, ReturnAction);
```
The 'payload' is an instance of a class that implements IPayloadViewModel and this is a way to pass a payload of data to the receiving viewmodel.

The 'ReturnAction' is an Action that is called (with an optiona payload) when navigating back to the viewmodel (a callback).

To return to the previous viewmodel and optionally pass a payload, just call NavigateBack from the viewmodel.
```
var payload = Resolver.Resolve<ISomePayload>();
payload.SomeData = someData;
NavigateBack(payload);
```
