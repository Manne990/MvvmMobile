// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace MvvmMobile.Sample.iOS.View
{
    [Register ("EditMotorcycleViewController")]
    partial class EditMotorcycleViewController
    {
        [Outlet]
        UIKit.UITextField BrandTextField { get; set; }


        [Outlet]
        UIKit.UITextField ModelTextField { get; set; }


        [Outlet]
        UIKit.UITextField YearTextField { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (BrandTextField != null) {
                BrandTextField.Dispose ();
                BrandTextField = null;
            }

            if (ModelTextField != null) {
                ModelTextField.Dispose ();
                ModelTextField = null;
            }

            if (YearTextField != null) {
                YearTextField.Dispose ();
                YearTextField = null;
            }
        }
    }
}