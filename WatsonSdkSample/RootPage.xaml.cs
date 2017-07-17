using System;
using System.Collections.Generic;
using WatsonSdkSample.VisualRecognition;
using Xamarin.Forms;

namespace WatsonSdkSample
{
    public partial class RootPage : ContentPage
    {
        public RootPage()
        {
            InitializeComponent();
        }

        void TransitToVisualRecognition(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new VisualRecognitionPage1());
        }
    }
}
