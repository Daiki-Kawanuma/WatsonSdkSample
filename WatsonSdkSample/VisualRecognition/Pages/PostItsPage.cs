using System;

using Xamarin.Forms;

namespace WatsonSdkSample.VisualRecognition.Pages
{
    public class PostItsPage : ContentPage
    {
        public PostItsPage()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}

