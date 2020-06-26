using System;
using CoreGraphics;
using MvvmMobile.iOS.Common;
using MvvmMobile.iOS.Navigation;
using MvvmMobile.iOS.View;
using MvvmMobile.Sample.Core.ViewModel.Motorcycles;
using MvvmMobile.Sample.iOS.ViewController.Edit;
using UIKit;

namespace MvvmMobile.Sample.iOS.View
{
    [Storyboard(storyboardName:"Main", storyboardId:"EditMotorcycleViewController")]
    public partial class EditMotorcycleViewController : ViewControllerBase<IEditMotorcycleViewModel>, IViewControllerWithTransition
    {
        // Constructors
        public EditMotorcycleViewController()
        {
            AsModal = true;
        }

        public EditMotorcycleViewController(IntPtr handle) : base(handle)
        {
            AsModal = true;
        }


        // -----------------------------------------------------------------------------

        // Lifecycle
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var tap = new UITapGestureRecognizer(() => DismissViewController(true, null));
            View.AddGestureRecognizer(tap);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            NavigationItem.SetLeftBarButtonItem(new UIBarButtonItem(UIBarButtonSystemItem.Cancel, (sender, e) => 
            {
                ViewModel.CancelCommand.Execute();
            }), false);

            NavigationItem.SetRightBarButtonItem(new UIBarButtonItem(UIBarButtonSystemItem.Done, (sender, e) => 
            {
                ViewModel.Motorcycle.Brand = BrandTextField.Text;
                ViewModel.Motorcycle.Model = ModelTextField.Text;

                if (int.TryParse(YearTextField.Text, out int year))
                {
                    ViewModel.Motorcycle.Year = year;
                }

                ViewModel.SaveMotorcycleCommand.Execute();
            }), false);
        }


        // -----------------------------------------------------------------------------

        // Overrides
        protected override void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModel.Motorcycle))
            {
                BrandTextField.Text = ViewModel.Motorcycle?.Brand;
                ModelTextField.Text = ViewModel.Motorcycle?.Model;
                YearTextField.Text = ViewModel.Motorcycle?.Year.ToString();
                return;
            }
        }

        public override ITransitionAnimator LoadPresentTransitionAnimator(UIViewController sourceViewController)
        {
            return new PresentAnimator();
        }

        public override ITransitionAnimator LoadDismissTransitionAnimator(UIViewController sourceViewController)
        {
            return new DismissAnimator();
        }


        // -----------------------------------------------------------------------------

        // IViewControllerWithTransition Implementation
        public UIView GetViewForSnapshot(Type relatedViewControllerType, ViewControllerTransitioningAnimatorPresentationType transitionType)
        {
            return NavigationController?.View ?? View;
        }

        public CGRect TransitionTargetRect()
        {
            return new CGRect(0, 0, View.Frame.Width, 200f);
        }
    }
}