using CoreGraphics;
using MvvmMobile.iOS.View;
using MvvmMobile.Sample.Core.ViewModel;
using UIKit;

namespace MvvmMobile.Sample.iOS.ViewController
{
    public class SecondViewController : ViewControllerBase<ISecondViewModel>
    {
        // Private Members
        private UILabel _titleLabel;
        private UIButton _firstButton;
        private UIButton _secondButton;


        // -----------------------------------------------------------------------------

        // Constructors
        public SecondViewController()
        {
            AsModal = false;
        }


        // -----------------------------------------------------------------------------

        // Lifecycle
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Init
            Title = "Second";
            View.BackgroundColor = UIColor.White;

            // Controls
            _titleLabel = new UILabel { TextColor = UIColor.Black, TextAlignment = UITextAlignment.Center };

            _firstButton = new UIButton();
            _firstButton.SetTitle("Send back 'Jonas'", UIControlState.Normal);
            _firstButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
            _firstButton.TouchUpInside += (sender, e) => 
            {
                // Report back
                ViewModel.NameSelectedCommand.Execute("Jonas");
            };

            _secondButton = new UIButton();
            _secondButton.SetTitle("Send back 'Kalle'", UIControlState.Normal);
            _secondButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
            _secondButton.TouchUpInside += (sender, e) => 
            {
                // Report back
                ViewModel.NameSelectedCommand.Execute("Kalle");
            };

            // Add Controls
            Add(_titleLabel);
            Add(_firstButton);
            Add(_secondButton);
        }

        protected override void ViewFramesReady()
        {
            base.ViewFramesReady();

            // Set Frames
            _titleLabel.Frame = new CGRect(0, 60f, View.Bounds.Width, 40f);
            _firstButton.Frame = new CGRect(0, _titleLabel.Frame.Bottom + 20f, View.Bounds.Width, 40f);
            _secondButton.Frame = new CGRect(0, _firstButton.Frame.Bottom + 20f, View.Bounds.Width, 40f);
        }


        // -----------------------------------------------------------------------------

        // Overrides
        protected override void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModel.Title))
            {
                _titleLabel.Text = ViewModel.Title;
                return;
            }
        }
    }
}