# MvvmMobile #
## Introduction ##
MvvmMobile is an MVVM framework developed for Xamarin iOS and Xamarin Android with a focus on abstracted navigation. This ensures high testability of the viewmodel classes.

## Contributors ##
Mikael Stalvik  
Jonas Frid

## Getting Started ##
- Create a .NET Standard project for the shared code
- Create a Xamarin iOS project
- Create a Xamarin Android project

## Create the shared project ##
- Create a .NET Standard (version 2.0 or later) project for your shared code and add a reference to MvvmMobile.Core
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

## Create the Xamarin iOS project ##
- Create a Xamarin iOS project
- Add a reference to your shared project
- Add references to MvvmMobile.Core and MvvmMobile.iOS
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

In AppDelegate.cs and the FinishedLaunching method. Initialize MvvmMobile with the mapping between your viewmodels and your view controllers.
```
MvvmMobile.iOS.Bootstrapper.Init(new Dictionary<Type, Type>
{
    { typeof(IMyFirstViewModel), typeof(MyFirstController) },
    { typeof(IMySecondViewModel), typeof(MySecondViewController) }
});
```

## Create the Xamarin Android project ##
- Create a Xamarin Android project
- Add a reference to your shared project
- Add references to MvvmMobile.Core and MvvmMobile.Droid
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

In your application class. Initialize MvvmMobile with the mapping between your viewmodels and your activities/fragments.
```
MvvmMobile.Droid.Bootstrapper.Init(new Dictionary<Type, Type>
{
    { typeof(IMyFirstViewModel), typeof(MyFirstActivity) },
    { typeof(IMySecondViewModel), typeof(SomeFragment) }
});
```

## Samples ##
For a more detailed view of MvvmMobile, have a look at the sample project included in this repo!
Happy coding!
