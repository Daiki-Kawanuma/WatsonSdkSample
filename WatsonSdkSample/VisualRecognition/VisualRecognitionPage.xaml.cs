using System;
using System.Diagnostics;
using System.IO;
using IBM.WatsonDeveloperCloud.VisualRecognition.v3;
using Plugin.Media;
using Xamarin.Forms;

namespace WatsonSdkSample.VisualRecognition
{
    public partial class VisualRecognition : ContentPage
    {
        private const string ApiKey = "cf554ce5003a599b7bc830430f023b58d64cfb89";
        private readonly VisualRecognitionService _visualRecognition;

        public VisualRecognition()
        {
            InitializeComponent();

            _visualRecognition = new VisualRecognitionService();
            _visualRecognition.SetCredential(ApiKey);
        }

        public async void Handle_Clicked(object sender, EventArgs e)
        {
            image.Source = null;
            indicator.IsVisible = true;

            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("No Auth", "Picking images denied.", "OK");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync();

            if (file == null)
                return;

            /** 画像認識 **/
            var imageByteArray = ReadFully(file.GetStream());

            var result = _visualRecognition.DetectFaces(imageData: imageByteArray, imageDataName: file.AlbumPath,
                imageDataMimeType: "image/jpeg");

            var text = "";
            foreach (var face in result.Images[0].Faces)
            {
                text += string.Format("Left: {0}, Top: {1}, Width: {2}, Height: {3}, " +
                                      "Gender: {4}, GenderScore: {5}, AgeMax: {6}, AgeMin: {7}, AgeScore: {8}, " +
                                      "IdentifyName: {9}, IdentifyScore: {10}, IdentifyHierarchy: {11}",
                            face.FaceLocation.Left,
                            face.FaceLocation.Top,
                            face.FaceLocation.Width,
                            face.FaceLocation.Height,
                            face.Gender.Gender,
                            face.Gender.Score,
                            face.Age.Max,
                            face.Age.Min,
                            face.Age.Score,
                            face.Identity?.Name,
                            face.Identity?.Score,
                            face.Identity?.TypeHierarchy) + System.Environment.NewLine;
            }

            image.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
            label.Text = text;
            indicator.IsVisible = false;
        }

        private static byte[] ReadFully(Stream input)
        {
            using (var ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}