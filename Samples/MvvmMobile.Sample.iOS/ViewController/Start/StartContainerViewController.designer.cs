// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace MvvmMobile.Sample.iOS.ViewController.Start
{
	[Register ("StartContainerViewController")]
	partial class StartContainerViewController
	{
		[Outlet]
		UIKit.UIBarButtonItem AddButton { get; set; }

		[Outlet]
		UIKit.UITableView MotorcyclesTableView { get; set; }

		[Outlet]
		UIKit.UIView SubViewBlockerView { get; set; }

		[Outlet]
		UIKit.UIView SubViewOverlayView { get; set; }

		[Action ("AddMotorcycle:")]
		partial void AddMotorcycle (Foundation.NSObject sender);

		[Action ("StartNavDemo:")]
		partial void StartNavDemo (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (AddButton != null) {
				AddButton.Dispose ();
				AddButton = null;
			}

			if (MotorcyclesTableView != null) {
				MotorcyclesTableView.Dispose ();
				MotorcyclesTableView = null;
			}

			if (SubViewOverlayView != null) {
				SubViewOverlayView.Dispose ();
				SubViewOverlayView = null;
			}

			if (SubViewBlockerView != null) {
				SubViewBlockerView.Dispose ();
				SubViewBlockerView = null;
			}
		}
	}
}
