using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ESSWinApp.Core.Processor;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ESSWinApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MotionDetection : Page, INotifyPropertyChanged
    {
        public MotionDetection()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            LoadMotionDetection();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Set<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private async void LoadMotionDetection()
        {
            var motionDetection = await DataProcessor.LoadMotionDetection();

            if (motionDetection.MotionDetection != null)
            {
                foreach (var item in motionDetection.MotionDetection)
                {
                    ListView MListView = new ListView();
                    MListView.Background = new SolidColorBrush(Color.FromArgb(20, 100, 37, 129));

                    MListView.Items.Add("Date Detected: " + item.DateDetected);
                    MListView.Items.Add("Time Detected: " + item.TimeDetected);
                    MListView.Items.Add("Device IP: " + item.DeviceIp);

                    MotionListView.Items.Add(MListView);
                }
            }
        }
    }
}
