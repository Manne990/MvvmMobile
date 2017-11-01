using System;
using MvvmMobile.iOS.Common;
using MvvmMobile.iOS.View;
using MvvmMobile.Sample.Core.Model;
using MvvmMobile.Sample.Core.ViewModel;

namespace MvvmMobile.Sample.iOS.View
{
    [Storyboard(storyboardName:"Main", storyboardId:"EditMotorcycleViewController")]
    public partial class EditMotorcycleViewController : ViewControllerBase<IEditMotorcycleViewModel>
    {
        // Private Members


        // -----------------------------------------------------------------------------

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


        }


        // -----------------------------------------------------------------------------

        // Overrides
        protected override void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModel.Motorcycle))
            {
                return;
            }
        }


        // -----------------------------------------------------------------------------

        // Private Methods
    }
}