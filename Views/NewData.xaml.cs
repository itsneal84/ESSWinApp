using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml.Controls;
using ESSWinApp.Core.Processor;

namespace ESSWinApp.Views
{
    public sealed partial class NewData : Page, INotifyPropertyChanged
    {
        public NewData()
        {
            InitializeComponent();
            LoadData();
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

        private async void LoadData()
        {
            var newDataList = await DataProcessor.LoadData();
            ListView faceListView = new ListView();
            ListView unknownListView = new ListView();
            ListView motionListView = new ListView();

            if (newDataList.NewDataList != null)
            {
                foreach (var data in newDataList.NewDataList)
                {
                    if (data.Type != null)
                    {
                        if (data.Type.Contains("known image"))
                        {
                            faceListView.Items.Add("Type: " + data.Type);

                            if (data.DeviceIp != null)
                                faceListView.Items.Add("Device IP: " + data.DeviceIp);

                            if (data.FileName != null)
                                faceListView.Items.Add("Filename: " + data.FileName);

                            if (data.Name != null)
                                faceListView.Items.Add("Name: " + data.Name);

                            if (data.DateDetected != null)
                                faceListView.Items.Add("Date Detected: " + data.DateDetected);

                            if (data.TimeDetected != null)
                                faceListView.Items.Add("Time Detected: " + data.TimeDetected);

                            faceListView.Items.Add("");
                        }

                        if (data.Type.Contains("unknown image"))
                        {
                            unknownListView.Items.Add("Type: " + data.Type);

                            if (data.DeviceIp != null)
                                unknownListView.Items.Add("Device IP: " + data.DeviceIp);

                            if (data.FileName != null)
                                unknownListView.Items.Add("Filename: " + data.FileName);

                            if (data.Name != null)
                                unknownListView.Items.Add("Name: " + data.Name);

                            if (data.DateFound != null)
                                unknownListView.Items.Add("Date Found: " + data.DateFound);

                            if (data.TimeFound != null)
                                unknownListView.Items.Add("Time Found: " + data.TimeFound);

                            unknownListView.Items.Add("");
                        }

                        if (data.Type.Contains("motion detection"))
                        {
                            motionListView.Items.Add("Type: " + data.Type);

                            if (data.DeviceIp != null)
                                motionListView.Items.Add("Device IP: " + data.DeviceIp);

                            if (data.DateDetected != null)
                                motionListView.Items.Add("Date Detected: " + data.DateDetected);

                            if (data.TimeDetected != null)
                                motionListView.Items.Add("Time Detected: " + data.TimeDetected);

                            motionListView.Items.Add("");
                        }
                    }
                }

                FaceData.Children.Add(faceListView);
                UnknownData.Children.Add(unknownListView);
                MotionData.Children.Add(motionListView);
            }
            else
            {
                unknownListView.Items.Add("No Data Available");
                UnknownData.Children.Add(unknownListView);
            }
        }

        private void refresh_btn_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            LoadData();
        }
    }
}
