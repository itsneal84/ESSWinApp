using ESSWinApp.Core.Models;
using ESSWinApp.Core.Processor;
using System;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Graphics.Imaging;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ESSWinApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ImageDetailsView : Page
    {
        public ImageDetailsView()
        {
            this.InitializeComponent();

            save_btn.Visibility = Visibility.Collapsed;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string origFilename = e.Parameter.ToString();

            GetImageDetails(origFilename);

            name_lbl.Text = origFilename.Split("_")[0];
            filename_lbl.Text = origFilename;
            date_detected_lbl.Text = origFilename.Split("_")[1];
            time_detected_lbl.Text = origFilename.Split("_")[2];

            // set the original Name so we know when its been changed in the text box
            origName = origFilename.Split("_")[0];
        }

        private async void GetImageDetails(string fileName)
        {
            fileName = fileName + ".jpg"; // need to add the extension for the app to find the file
            var image = await DataProcessor.GetImage(fileName);

            if (image.EncodedImage != null)
            {
                // decode the image from the received base64 encoded string
                var decodedImg = Convert.FromBase64String(image.EncodedImage);
                var buffer = decodedImg.AsBuffer();
                var stream = buffer.AsStream();
                var img = stream.AsRandomAccessStream();
                var decoder = await BitmapDecoder.CreateAsync(img);
                img.Seek(0);

                var imgOutput = new WriteableBitmap((int)decoder.PixelHeight, (int)decoder.PixelWidth);
                await imgOutput.SetSourceAsync(img);

                image_view.Source = imgOutput;
            }
        }

        public string origName { get; set; }

        private void Name_lbl_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (name_lbl.Text != origName)
            {
                save_btn.Visibility = Visibility.Visible;
            }
        }

        private async void save_btn_Click(object sender, RoutedEventArgs e)
        {
            ImageDetails imageDetails = new ImageDetails();
            imageDetails.Name = name_lbl.Text;
            imageDetails.FileName = filename_lbl.Text;

            string msg = await DataProcessor.UpdateImageName(imageDetails);

            if (msg != "")
            {
                this.Frame.Navigate(typeof(UnknownFaceView));
                msg_lbl.Text = msg;
            }

            msg_lbl.Text = "Error saving details: " + msg;
        }
    }
}
