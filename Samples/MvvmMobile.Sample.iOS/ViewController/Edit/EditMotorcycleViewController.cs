using System;
using MvvmMobile.iOS.Common;
using MvvmMobile.iOS.View;
using MvvmMobile.Sample.Core.ViewModel;
using UIKit;

namespace MvvmMobile.Sample.iOS.View
{
    [Storyboard(storyboardName:"Main", storyboardId:"EditMotorcycleViewController")]
    public partial class EditMotorcycleViewController : ViewControllerBase<IEditMotorcycleViewModel>
    {
        // Constructors
        public EditMotorcycleViewController(IntPtr handle) : base(handle)
        {
            AsModal = true;
        }


        // -----------------------------------------------------------------------------

        // Lifecycle
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //ViewModel.SetBinding(BrandTextField, "Motorcycle.Brand", true);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            NavigationItem?.SetLeftBarButtonItem(new UIBarButtonItem(UIBarButtonSystemItem.Cancel, (sender, e) => 
            {
                ViewModel?.CancelCommand.Execute();
            }), false);

            NavigationItem?.SetRightBarButtonItem(new UIBarButtonItem(UIBarButtonSystemItem.Done, (sender, e) => 
            {
                ViewModel.Motorcycle.Brand = BrandTextField.Text;
                ViewModel.Motorcycle.Model = ModelTextField.Text;

                if (int.TryParse(YearTextField.Text, out int year))
                {
                    ViewModel.Motorcycle.Year = year;
                }

                ViewModel?.SaveMotorcycleCommand.Execute();
            }), false);
        }


        // -----------------------------------------------------------------------------

        // Overrides
        protected override void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModel.Motorcycle))
            {
                BrandTextField.Text = ViewModel.Motorcycle.Brand;
                ModelTextField.Text = ViewModel.Motorcycle.Model;
                YearTextField.Text = ViewModel.Motorcycle.Year.ToString();
                return;
            }
        }
    }
}