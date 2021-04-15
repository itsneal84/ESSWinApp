using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using ESSWinApp.Core.Processor;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ESSWinApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DetectedFacesView : Page, INotifyPropertyChanged
    {
        public DetectedFacesView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            LoadDetectedFaces();
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

        private async void LoadDetectedFaces()
        {
            var receivedFaces = await DataProcessor.LoadDetectedFaces();

            if (receivedFaces.DetectedFaces != null)
            {
                foreach (var detected in receivedFaces.DetectedFaces)
                {
                    ListView FListView = new ListView();
                    FListView.Background = new SolidColorBrush(Color.FromArgb(20,47,172,102));

                    FListView.Items.Add("Name: " + detected.Name);
                    FListView.Items.Add("File Name: " + detected.FileName);
                    FListView.Items.Add("Date Detected: " + detected.DateDetected);
                    FListView.Items.Add("Time Detected: " + detected.TimeDetected);

                    FaceListView.Items.Add(FListView);
                }
            }
        }
    }
}
