﻿using Foundation;
using NoteTakingApp.Constants;
using NoteTakingApp.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SearchBar), typeof(CustomSearchBarRenderer))]
namespace NoteTakingApp.iOS.Renderers
{
    public class CustomSearchBarRenderer : SearchBarRenderer
    {
        public CustomSearchBarRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);

            var searchbar = Control;
            if (e.NewElement != null)
            {
                NSString searchField = new NSString("searchField");
                var textField = (UITextField)searchbar.ValueForKey(searchField);

                var colorResource = AppGlobal.SearchBarColor;
                var backgroundColor = colorResource.ToUIColor();

                textField.BackgroundColor = backgroundColor;
                searchbar.SetShowsCancelButton(false, true);
            }
        }
    }
}
