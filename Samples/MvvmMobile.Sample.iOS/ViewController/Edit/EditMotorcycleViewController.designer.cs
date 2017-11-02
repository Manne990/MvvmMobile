// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
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
