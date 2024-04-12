using Common.Models;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using System.Windows.Input;

namespace Client.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        string apiUrl = "https://localhost:7096/api/Asset";

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<Asset> Images { get; set; } = new ObservableCollection<Asset>();

        public MainWindowViewModel(ObservableCollection<Asset> assets, string message)
        {
            this.Message = message;
            this.Images = assets;
        }

        public MainWindowViewModel() { }

        private string _text;
        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged(nameof(Text));
            }
        }

        private string _message;
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        private string _selectedImagePath;
        public string SelectedImagePath
        {
            get => _selectedImagePath;
            set
            {
                _selectedImagePath = value;
                OnPropertyChanged(nameof(SelectedImagePath));
            }
        }

        private ICommand _sendCommand;
        public ICommand SendCommand
        {
            get
            {
                if (_sendCommand == null)
                    _sendCommand = new RelayCommand(Send);
                return _sendCommand;
            }
        }

        private async void Send()
        {
            using (var client = new HttpClient())
            {
                var openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    SelectedImagePath = openFileDialog.FileName;

                    byte[] imageBytes = File.ReadAllBytes(SelectedImagePath);

                    var response = await client.PutAsJsonAsync(apiUrl, new Asset(0, Text, imageBytes));
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();

                        Images.Add(JsonConvert.DeserializeObject<Asset>(result));
                        Message = "Update successful!";
                    }
                    else
                    {
                        Message = $"Update error: {response.Content}";
                    }
                }                
            }
        }
    }
}
