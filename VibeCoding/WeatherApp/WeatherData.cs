using System.Text.Json.Serialization;

namespace WeatherApp // ※ご自身のプロジェクト名に合わせてください（WeatherService.cs と同じにする）
{
    // Open-Meteo API のレスポンス全体を受け取るクラス
    public class OpenMeteoResponse {
        [JsonPropertyName("current")]
        public CurrentWeatherJson? Current { get; set; }

        [JsonPropertyName("daily")]
        public DailyWeatherJson? Daily { get; set; }
    }

    // APIの "current" 項目に対応
    public class CurrentWeatherJson {
        [JsonPropertyName("temperature_2m")]
        public double Temperature2m { get; set; }

        [JsonPropertyName("relative_humidity_2m")]
        public int RelativeHumidity2m { get; set; }

        [JsonPropertyName("weather_code")]
        public int WeatherCode { get; set; }

        [JsonPropertyName("wind_speed_10m")]
        public double WindSpeed10m { get; set; }
    }

    // APIの "daily" 項目に対応（各項目が7日分の配列で返ってくる）
    public class DailyWeatherJson {
        [JsonPropertyName("time")]
        public List<string> Time { get; set; } = new();

        [JsonPropertyName("weather_code")]
        public List<int> WeatherCode { get; set; } = new();

        [JsonPropertyName("temperature_2m_max")]
        public List<double> Temperature2mMax { get; set; } = new();

        [JsonPropertyName("temperature_2m_min")]
        public List<double> Temperature2mMin { get; set; } = new();

        [JsonPropertyName("precipitation_probability_max")]
        public List<int?> PrecipitationProbabilityMax { get; set; } = new();
    }

    // 画面（UI）に表示しやすい形にまとめたデータ
    public class WeatherSummary {
        public string LocationName { get; set; } = "東京";
        public double CurrentTemperature { get; set; }
        public string CurrentWeatherText { get; set; } = "";
        public int CurrentHumidity { get; set; }
        public double CurrentWindSpeed { get; set; }
        public List<DailyForecastItem> DailyForecasts { get; set; } = new();
    }

    // 1日分の予報データ（7日分リストの1要素）
    public class DailyForecastItem {
        public string DateText { get; set; } = "";
        public string WeatherText { get; set; } = "";
        public double MaxTemperature { get; set; }
        public double MinTemperature { get; set; }
        public int PrecipitationProbability { get; set; }
    }

    // 選択可能な都市（またはカスタム緯度経度）を表すモデル
    public class CityOption {
        public string Name { get; set; } = "";
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public override string ToString() => $"{Name} ({Latitude:0.00}, {Longitude:0.00})";
    }

    // Geocoding API のレスポンス全体
    public class GeocodingResponse {
        [JsonPropertyName("results")]
        public List<GeocodingLocation>? Results { get; set; }
    }

    // 検索ヒットした1地点の情報
    public class GeocodingLocation {
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [JsonPropertyName("admin1")]
        public string? Admin1 { get; set; }

        public string DisplayLabel {
            get {
                var parts = new List<string>();
                if (!string.IsNullOrWhiteSpace(Admin1)) parts.Add(Admin1);
                if (!string.IsNullOrWhiteSpace(Country)) parts.Add(Country);
                string suffix = parts.Count > 0 ? $" / {string.Join(", ", parts)}" : "";
                return $"{Name}{suffix} ({Latitude:0.00}, {Longitude:0.00})";
            }
        }
    }
}