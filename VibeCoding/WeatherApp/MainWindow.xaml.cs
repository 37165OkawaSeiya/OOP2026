using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WeatherApp // ※ご自身のプロジェクト名に合わせてください
{
    public partial class MainWindow : Window {
        private readonly WeatherService _weatherService = new();
        private string _currentLocationName = "東京";
        private double _currentLatitude = 35.6895;
        private double _currentLongitude = 139.6917;
        private bool _isInitializing = true;

        // 気温色分け用のブラシ
        private static readonly Brush HotTempBrush = new SolidColorBrush(Color.FromRgb(220, 38, 38));   // 25℃以上：赤
        private static readonly Brush WarmTempBrush = new SolidColorBrush(Color.FromRgb(234, 88, 12));  // 15℃以上：オレンジ
        private static readonly Brush MildTempBrush = new SolidColorBrush(Color.FromRgb(21, 128, 61));  // 5℃以上：緑
        private static readonly Brush ColdTempBrush = new SolidColorBrush(Color.FromRgb(37, 99, 235));  // 5℃未満：青
        private static readonly Brush FreezingBrush = new SolidColorBrush(Color.FromRgb(3, 105, 161));  // 0℃未満：濃い青

        public MainWindow() {
            InitializeComponent();

            // 都市プリセット一覧を ComboBox に設定（先頭は東京）
            var cities = WeatherService.GetPresetCities();
            CityComboBox.ItemsSource = cities;
            CityComboBox.SelectedIndex = 0;

            // 初期座標を入力欄にも表示しておく
            CustomNameTextBox.Text = _currentLocationName;
            LatitudeTextBox.Text = _currentLatitude.ToString("0.####", CultureInfo.InvariantCulture);
            LongitudeTextBox.Text = _currentLongitude.ToString("0.####", CultureInfo.InvariantCulture);

            _isInitializing = false;
            Loaded += MainWindow_Loaded;
            StateChanged += MainWindow_StateChanged;
        }

        private void MainWindow_StateChanged(object? sender, EventArgs e) {
            // 最大化時は7日間予報の行高を少し広げて余白を活かす
            ForecastDataGrid.RowHeight = WindowState == WindowState.Maximized ? 52 : 42;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e) {
            await LoadWeatherAsync(_currentLocationName, _currentLatitude, _currentLongitude);
        }

        private async void RefreshButton_Click(object sender, RoutedEventArgs e) {
            await LoadWeatherAsync(_currentLocationName, _currentLatitude, _currentLongitude);
        }

        private async void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (_isInitializing) {
                return;
            }

            if (CityComboBox.SelectedItem is CityOption selectedCity) {
                CustomNameTextBox.Text = selectedCity.Name;
                LatitudeTextBox.Text = selectedCity.Latitude.ToString("0.####", CultureInfo.InvariantCulture);
                LongitudeTextBox.Text = selectedCity.Longitude.ToString("0.####", CultureInfo.InvariantCulture);

                await LoadWeatherAsync(selectedCity.Name, selectedCity.Latitude, selectedCity.Longitude);
            }
        }

        private async void FetchCustomButton_Click(object sender, RoutedEventArgs e) {
            string latInput = LatitudeTextBox.Text.Trim();
            string lonInput = LongitudeTextBox.Text.Trim();

            bool latOk = double.TryParse(latInput, NumberStyles.Float, CultureInfo.InvariantCulture, out double latitude)
                         || double.TryParse(latInput, NumberStyles.Float, CultureInfo.CurrentCulture, out latitude);

            bool lonOk = double.TryParse(lonInput, NumberStyles.Float, CultureInfo.InvariantCulture, out double longitude)
                         || double.TryParse(lonInput, NumberStyles.Float, CultureInfo.CurrentCulture, out longitude);

            if (!latOk || !lonOk) {
                MessageBox.Show(
                    "緯度と経度は数値（例: 36.2911 や 139.3736）で入力してください。",
                    "入力エラー",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            string customName = string.IsNullOrWhiteSpace(CustomNameTextBox.Text)
                ? "指定座標"
                : CustomNameTextBox.Text.Trim();

            await LoadWeatherAsync(customName, latitude, longitude);
        }

        private async Task LoadWeatherAsync(string locationName, double latitude, double longitude) {
            try {
                SetControlsEnabled(false);
                StatusTextBlock.Text = $"{locationName} の天気データを取得中...";

                WeatherSummary summary = await _weatherService.GetWeatherAsync(locationName, latitude, longitude);

                _currentLocationName = summary.LocationName;
                _currentLatitude = latitude;
                _currentLongitude = longitude;

                TitleLocationTextBlock.Text = $"{summary.LocationName}の天気予報";
                CurrentWeatherIconTextBlock.Text = ToWeatherEmoji(summary.CurrentWeatherText);
                CurrentTempTextBlock.Text = $"{summary.CurrentTemperature:0.0}℃";
                CurrentTempTextBlock.Foreground = ToTemperatureBrush(summary.CurrentTemperature);
                CurrentWeatherTextBlock.Text = summary.CurrentWeatherText;
                CurrentHumidityTextBlock.Text = $"湿度: {summary.CurrentHumidity}%";
                CurrentWindTextBlock.Text = $"風速: {summary.CurrentWindSpeed:0.0} m/s";

                var displayRows = summary.DailyForecasts.Select(item => new DailyForecastDisplayItem {
                    DateText = item.DateText,
                    WeatherIcon = ToWeatherEmoji(item.WeatherText),
                    WeatherText = item.WeatherText,
                    MaxTemperature = item.MaxTemperature,
                    MinTemperature = item.MinTemperature,
                    MaxTempBrush = ToTemperatureBrush(item.MaxTemperature),
                    MinTempBrush = ToTemperatureBrush(item.MinTemperature),
                    PrecipitationProbability = item.PrecipitationProbability
                }).ToList();

                ForecastDataGrid.ItemsSource = displayRows;
                StatusTextBlock.Text = $"最終更新: {DateTime.Now:HH:mm:ss}（{summary.LocationName}）";
            }
            catch (Exception ex) {
                StatusTextBlock.Text = "取得エラーが発生しました";
                MessageBox.Show(
                    $"天気データの取得に失敗しました。\n\n詳細: {ex.Message}",
                    "エラー",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
            finally {
                SetControlsEnabled(true);
            }
        }

        private void SetControlsEnabled(bool isEnabled) {
            RefreshButton.IsEnabled = isEnabled;
            CityComboBox.IsEnabled = isEnabled;
            FetchCustomButton.IsEnabled = isEnabled;
        }

        private static string ToWeatherEmoji(string weatherText) {
            if (weatherText.Contains("雷雨")) return "⛈️";
            if (weatherText.Contains("雪")) return "❄️";
            if (weatherText.Contains("雨") || weatherText.Contains("霧雨")) return "🌧️";
            if (weatherText.Contains("霧")) return "🌫️";
            if (weatherText.Contains("快晴")) return "☀️";
            if (weatherText.Contains("晴れ")) return "🌤️";
            if (weatherText.Contains("一部曇り")) return "⛅";
            if (weatherText.Contains("曇り")) return "☁️";
            return "🌡️";
        }

        private static Brush ToTemperatureBrush(double temp) {
            if (temp < 0) return FreezingBrush;
            if (temp < 5) return ColdTempBrush;
            if (temp < 15) return MildTempBrush;
            if (temp < 25) return WarmTempBrush;
            return HotTempBrush;
        }

        private sealed class DailyForecastDisplayItem {
            public string DateText { get; set; } = "";
            public string WeatherIcon { get; set; } = "";
            public string WeatherText { get; set; } = "";
            public double MaxTemperature { get; set; }
            public double MinTemperature { get; set; }
            public Brush MaxTempBrush { get; set; } = Brushes.Black;
            public Brush MinTempBrush { get; set; } = Brushes.Black;
            public int PrecipitationProbability { get; set; }
        }
    }
}