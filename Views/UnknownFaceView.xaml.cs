using ESSWinApp.Core.Processor;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ESSWinApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UnknownFaceView : Page, INotifyPropertyChanged
    {
        public UnknownFaceView()
        {
            this.InitializeComponent();

            LoadUnknownFaces();
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

        private async void LoadUnknownFaces()
        {
            var unknownFaces = await DataProcessor.LoadUnknownFaces();

            

            if (unknownFaces.UnknownFaces != null)
            {
                foreach (var item in unknownFaces.UnknownFaces)
                {
                    ListView UFListView = new ListView();
                    UFListView.Background = new SolidColorBrush(Color.FromArgb(20,148,193,32));

                    UFListView.Items.Add("Name: " + item.Name);
                    UFListView.Items.Add("File Name: " + item.FileName);
                    UFListView.Items.Add("Date Found: " + item.DateFound);
                    UFListView.Items.Add("Time Found: " + item.TimeFound);

                    UnknownListView.Items.Add(UFListView);
                }
            }
        }
    }
}
