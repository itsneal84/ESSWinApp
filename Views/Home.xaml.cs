using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ESSWinApp.Core.Models;
using ESSWinApp.Core.Processor;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ESSWinApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Home : Page
    {
        public Home()
        {
            this.InitializeComponent();

            start_btn.Visibility = Visibility.Collapsed;
            add_btn.Visibility = Visibility.Collapsed;
            process_lbl.Visibility = Visibility.Collapsed;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            LoadConnectionDetails();
            await LoadNewCount();
            await LoadFaceCount();
            await LoadUnknownCount();
            await LoadMotionCount();
            await LoadDevices();
        }

        private void LoadConnectionDetails()
        {
            if (UserModel.DeviceIp != null)
            {
                device_ip_lbl.Text = UserModel.DeviceIp;
            }

            if (UserModel.Name != null)
            {
                username_lbl.Text = UserModel.Name;
            }

            if (UserModel.DeviceIp == null && UserModel.Name == null)
            {
                error_rec.Fill = new SolidColorBrush(Colors.Red);
                error_lbl.Text = "No Device Connected";
            }
        }

        private async Task<string> LoadNewCount()
        {
            string count;

            if (UserModel.Connected == true)
            {
                count = await DataProcessor.LoadDataCount();
                
                if (count != "")
                {
                    data_count_lbl.Text = count;
                    return count;
                }
                else
                {
                    data_count_lbl.Text = "0";
                    return "0";
                }
            }
            else
            {
                count = "0";
                data_count_lbl.Text = count;
                return count;
            }
        }

        private async Task<string> LoadFaceCount()
        {
            string count;

            if (UserModel.Connected == true)
            {
                count = await DataProcessor.LoadFaceCount();
                if(count != "")
                {
                    face_count_lbl.Text = count;
                    return count;
                }
                else
                {
                    face_count_lbl.Text = "0";
                    return "0";
                }
                
            }
            else
            {
                count = "0";
                face_count_lbl.Text = count;
                return count;
            }
        }

        private async Task<string> LoadUnknownCount()
        {
            string count;

            if (UserModel.Connected == true)
            {
                count = await DataProcessor.LoadUnknownFaceCount();

                if (count != "")
                {
                    unknown_count_lbl.Text = count;
                    return count;
                }
                else
                {
                    unknown_count_lbl.Text = "0";
                    return "0";
                }
            }
            else
            {
                count = "0";
                unknown_count_lbl.Text = count;
                return count;
            }
        }

        private async Task<string> LoadMotionCount()
        {
            string count;

            if (UserModel.Connected == true)
            {
                count = await DataProcessor.LoadMotionDetectionCount();

                if(count != "")
                {
                    motion_count_lbl.Text = count;
                    return count;
                }
                else
                {
                    motion_count_lbl.Text = "0";
                    return "0";
                }
                
            }
            else
            {
                count = "0";
                motion_count_lbl.Text = count;
                return count;
            }
        }

        private async Task LoadDevices()
        {
            if (UserModel.Connected == true)
            {
                var devices = await DataProcessor.LoadDevices();

                if (devices.Devices.Count > 0)
                {
                    foreach (var device in devices.Devices)
                    {
                        TextBlock deviceName = new TextBlock();
                        deviceName.FontSize = 18;
                        deviceName.Foreground = new SolidColorBrush(Colors.White);
                        deviceName.FontWeight = FontWeights.Bold;
                        deviceName.TextWrapping = TextWrapping.Wrap;
                        deviceName.TextAlignment = TextAlignment.Center;

                        TextBlock deviceIp = new TextBlock();
                        deviceIp.FontSize = 18;
                        deviceIp.Foreground = new SolidColorBrush(Colors.White);
                        deviceIp.FontWeight = FontWeights.Bold;
                        deviceIp.TextAlignment = TextAlignment.Center;

                        deviceName.Text = "Name: " + device.DeviceName;
                        deviceIp.Text = "IP: " + device.DeviceIp;

                        DevicesList.Items.Add(deviceName);
                        DevicesList.Items.Add(deviceIp);
                        DevicesList.Items.Add("");
                    }

                    start_btn.Visibility = Visibility.Visible;
                    add_btn.Visibility = Visibility.Visible;
                    process_lbl.Visibility = Visibility.Visible;
                }
                else
                {
                    TextBlock deviceName = new TextBlock();
                    deviceName.FontSize = 18;
                    deviceName.Foreground = new SolidColorBrush(Colors.White);
                    deviceName.FontWeight = FontWeights.Bold;
                    deviceName.TextWrapping = TextWrapping.Wrap;
                    deviceName.TextAlignment = TextAlignment.Center;

                    deviceName.Text = "No Devices added";

                    DevicesList.Items.Add(deviceName);
                }
            }
        }

        private async void start_btn_Click(object sender, RoutedEventArgs e)
        {
            string processMsg = await DataProcessor.StartDevices();

            process_lbl.Text = processMsg;
        }

        private void add_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddDeviceView));
        }
    }
}
