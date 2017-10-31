using System;
using CoreGraphics;
using MvvmMobile.iOS.View;
using MvvmMobile.Sample.Core.ViewModel;
using UIKit;
using XLabs.Ioc;

namespace MvvmMobile.Sample.iOS.ViewController
{
    public class SecondViewController : ViewControllerBase
    {
        private ISecondViewModel _viewModel;
        private UILabel _titleLabel;
        private UIButton _firstButton;
        private UIButton _secondButton;

        public SecondViewController()
        {
            AsModal = true;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Init
            Title = "Second";
            View.BackgroundColor = UIColor.White;

            // Load View Model
            _viewModel = Resolver.Resolve<ISecondViewModel>();
            ViewModel = _viewModel;

            // Controls
            _titleLabel = new UILabel { TextColor = UIColor.Black, TextAlignment = UITextAlignment.Center };

            _firstButton = new UIButton();
            _firstButton.SetTitle("Send back 'Jonas'", UIControlState.Normal);
            _firstButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
            _firstButton.TouchUpInside += (sender, e) => 
            {
                // Report back
                _viewModel.NameSelectedCommand.Execute("Jonas");

                //NavigationController?.DismissViewController(true, () => 
                //{
                //    // Report back
                //    _viewModel.NameSelectedCommand.Execute("Jonas");
                //});
            };

            _secondButton = new UIButton();
            _secondButton.SetTitle("Send back 'Kalle'", UIControlState.Normal);
            _secondButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
            _secondButton.TouchUpInside += (sender, e) => 
            {
                // Report back
                _viewModel.NameSelectedCommand.Execute("Kalle");

                //NavigationController?.DismissViewController(true, () => 
                //{
                //    // Report back
                //    _viewModel.NameSelectedCommand.Execute("Kalle");
                //});
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

            // Load
            _viewModel.Load(PayloadId);
        }

        protected override void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.Title))
            {
                _titleLabel.Text = _viewModel.Title;
                return;
            }
        }
    }
}