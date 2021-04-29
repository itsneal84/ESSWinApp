using ESSWinApp.Core.Models;
using ESSWinApp.Core.Processor;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ESSWinApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddDeviceView : Page
    {
        public AddDeviceView()
        {
            this.InitializeComponent();
        }

        private async void Save_btn_OnClick(object sender, RoutedEventArgs e)
        {
            Device device = new Device();

            device.DeviceName = device_name_txt.Text;
            device.DeviceType = device_type_txt.Text;
            device.DeviceIp = device_ip_txt.Text;
            device.StreamLink = stream_link_txt.Text;

            if (face_rdo.IsChecked == true)
            {
                device.FaceDetection = "on";
            }
            else
            {
                device.FaceDetection = "off";
            }

            if (motion_rdo.IsChecked == true)
            {
                device.Motion = "on";
            }
            else
            {
                device.Motion = "off";
            }

            if (face_rdo.IsChecked == false && motion_rdo.IsChecked == false)
            {
                message_lbl.Text = "Please select one or both face detection and/or motion detection";
            }
            else
            {
                if (device.DeviceName != "" && device.DeviceType != "" && device.DeviceIp != "" && device.StreamLink != "")
                {
                    string returnMsg = await DataProcessor.AddDevice(device);
                    message_lbl.Text = returnMsg;
                }
                else
                {
                    message_lbl.Text = "Please enter all the device details";
                }
            }
        }
    }
}
