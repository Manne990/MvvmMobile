using System;
using System.Collections.Generic;

namespace MvvmMobile.Droid.View
{
    public interface IFragmentWithSharedElementTransition
    {
        List<(Android.Views.View sourceFragmentView, string destinationFragmentSharedElementName)> GetSharedElementsForTransition(FragmentBase destinationFragment, Type destinationViewModelType);
    }

    public interface IFragmentWithCustomAnimationTransition
    {
        (int enter, int exit, int popEnter, int popExit)? GetCustomAnimationsForTransition(FragmentBase destinationFragment, Type destinationViewModelType);
    }
}