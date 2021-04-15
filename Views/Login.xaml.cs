using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using ESSWinApp.Core.Models;
using ESSWinApp.Core.Processor;

namespace ESSWinApp.Views
{
    public sealed partial class Login : Page, INotifyPropertyChanged
    {
        public Login()
        {
            InitializeComponent();
            DataContext = userModel;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;

            OnPropertyChanged(propertyName);
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        UserModel userModel = new UserModel();

        private async void login_btn_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (username_txt.Text != "" && password_txt.Text != "" && device_ip_txt.Text != "")
            {
                UserModel.Name = username_txt.Text;
                userModel.Password = password_txt.Text;
                UserModel.DeviceIp = device_ip_txt.Text;

                string[] splitValues = UserModel.DeviceIp.Split('.');
                if (splitValues.Length != 4)
                {
                    error_lbl.Text = "please enter a valid ip";
                    error_lbl.Foreground = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    string returnMsg = await UserProcessor.Login(userModel);

                    if (returnMsg != "")
                    {
                        error_lbl.Text = returnMsg;
                        UserModel.Connected = true;
                    }
                    else
                    {
                        error_lbl.Text = "Error trying to connect";
                        error_lbl.Foreground = new SolidColorBrush(Colors.Red);
                    }
                }
            }
            else
            {
                error_lbl.Text = "Please complete all the information";
                error_lbl.Foreground = new SolidColorBrush(Colors.Red);
            }
        }

        private async void register_btn_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (username_txt.Text != "" && password_txt.Text != "" && device_ip_txt.Text != "")
            {
                UserModel.Name = username_txt.Text;
                userModel.Password = password_txt.Text;
                UserModel.DeviceIp = device_ip_txt.Text;

                string[] splitValues = UserModel.DeviceIp.Split('.');
                if (splitValues.Length != 4)
                {
                    error_lbl.Text = "please enter a valid ip";
                    error_lbl.Foreground = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    string returnMsg = await UserProcessor.Register(userModel);

                    if (returnMsg != "")
                    {
                        error_lbl.Text = returnMsg;
                    }
                    else
                    {
                        error_lbl.Text = "Error trying to register";
                        error_lbl.Foreground = new SolidColorBrush(Colors.Red);
                    }
                }
            }
            else
            {
                error_lbl.Text = "Please complete all the information";
                error_lbl.Foreground = new SolidColorBrush(Colors.Red);
            }
        }
    }
}
