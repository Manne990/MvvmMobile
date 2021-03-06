﻿using System.Collections.Generic;
using Cirrious.FluentLayouts.Touch;
using MvvmMobile.Core.Navigation;
using MvvmMobile.iOS.Navigation;
using MvvmMobile.iOS.View;
using MvvmMobile.Sample.Core.ViewModel.Navigation;
using UIKit;

namespace MvvmMobile.Sample.iOS.ViewController.Navigation
{
    public class NavViewControllerBase<T> : ViewControllerBase<T>, ISubViewContainerController 
        where T : class, INavBaseViewModel
    {
        // Private Members
        private UILabel _titleLabel;

        protected UIColor BackgroundColor { private get; set; }


        // -----------------------------------------------------------------------------

        // Constructors
        public NavViewControllerBase()
        {
            AsModal = true;

            SubViewNavigationStack = new Stack<UIViewController>();
        }

        // -----------------------------------------------------------------------------

        // Lifecycle
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Init
            NavigationItem.LeftBarButtonItem = new UIBarButtonItem("Back", UIBarButtonItemStyle.Done, null);
            NavigationItem.LeftBarButtonItem.Clicked += (sender, e) => ViewModel?.BackCommand?.Execute();

            View.BackgroundColor = BackgroundColor;

            _titleLabel = new UILabel
            {
                Text = Title,
                TextAlignment = UITextAlignment.Center,
                Font = UIFont.PreferredTitle1,
                TextColor = UIColor.Black,
                //BackgroundColor = BackgroundColor
            };

            //NavigationItem.TitleView = _titleLabel;

            var containerView = new UIView { BackgroundColor = UIColor.Magenta, ClipsToBounds = true };

            View.AddSubviews(containerView);

            // Add Constraints
            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
            containerView.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            View.AddConstraints(
                //_titleLabel.AtTopOf(View, 96f),
                //_titleLabel.AtLeftOf(View, 8f),
                //_titleLabel.WithSameWidth(View).Minus(16f),
                containerView.AtTopOf(View, 108f),
                containerView.AtLeftOf(View, 16f),
                containerView.WithSameWidth(View).Minus(32f),
                containerView.WithSameHeight(View).Minus(130f));

            SubViewContainerView = containerView;

            ViewModel?.NextSubViewCommand?.Execute();
        }


        // -----------------------------------------------------------------------------

        // ISubViewContainerController Implementation
        public UIView SubViewContainerView { get; protected set; }
        public Stack<UIViewController> SubViewNavigationStack { get; }
        public NSLayoutConstraint[] SubViewOriginalConstraints { get; set; }
    }
}