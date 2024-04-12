using Client.ViewModels;
using Common.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string apiUrl = "https://localhost:7096/api/Asset";

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(apiUrl);
                ObservableCollection<Asset> assets = new ObservableCollection<Asset>();
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    assets = JsonConvert.DeserializeObject<ObservableCollection<Asset>>(result) ?? new ObservableCollection<Asset>();

                    this.DataContext = new MainWindowViewModel(assets, "Loading successful");
                }
                else
                {
                    this.DataContext = new MainWindowViewModel(assets, "Loading failed");
                }
            }
        }
    }
}