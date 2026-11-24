using System.Globalization;
using System.Net.Http;
using System.Net.Http.Json;

namespace WeatherApp // ※ご自身のプロジェクト名に合わせてください（WeatherData.cs と同じにする）
{
    public class WeatherService {
        // アプリ全体で使い回せる HttpClient（ソケット枯渇を防ぐため static で保持）
        private static readonly HttpClient _httpClient = new() {
            Timeout = TimeSpan.FromSeconds(15)
        };

        // 選択できる都市プリセット一覧
        public static IReadOnlyList<CityOption> GetPresetCities() => new List<CityOption>
        {
            new() { Name = "東京", Latitude = 35.6895, Longitude = 139.6917 },
            new() { Name = "大阪", Latitude = 34.6937, Longitude = 135.5023 },
            new() { Name = "札幌", Latitude = 43.0618, Longitude = 141.3545 },
            new() { Name = "福岡", Latitude = 33.5902, Longitude = 130.4017 },
            new() { Name = "名古屋", Latitude = 35.1815, Longitude = 136.9066 },
            new() { Name = "那覇", Latitude = 26.2124, Longitude = 127.6809 }
        };

        // 都市名・地名から緯度・経度を検索（Open-Meteo Geocoding API）
        public async Task<IReadOnlyList<GeocodingLocation>> SearchLocationsAsync(string query, int count = 5) {
            if (string.IsNullOrWhiteSpace(query)) {
                return Array.Empty<GeocodingLocation>();
            }

            string encodedQuery = Uri.EscapeDataString(query.Trim());
            string url =
                $"https://geocoding-api.open-meteo.com/v1/search" +
                $"?name={encodedQuery}&count={count}&language=ja&format=json";

            var response = await _httpClient.GetFromJsonAsync<GeocodingResponse>(url);
            return response?.Results ?? new List<GeocodingLocation>();
        }

        // 既存の互換用（東京の天気を取得）
        public Task<WeatherSummary> GetTokyoWeatherAsync() {
            return GetWeatherAsync("東京", 35.6895, 139.6917);
        }

        // 任意の都市名・緯度・経度で現在＋7日間予報を取得
        public async Task<WeatherSummary> GetWeatherAsync(string locationName, double latitude, double longitude) {
            if (latitude is < -90 or > 90) {
                throw new ArgumentOutOfRangeException(nameof(latitude), "緯度は -90 から 90 の範囲で指定してください。");
            }

            if (longitude is < -180 or > 180) {
                throw new ArgumentOutOfRangeException(nameof(longitude), "経度は -180 から 180 の範囲で指定してください。");
            }

            string latText = latitude.ToString("0.####", CultureInfo.InvariantCulture);
            string lonText = longitude.ToString("0.####", CultureInfo.InvariantCulture);

            string url =
                $"https://api.open-meteo.com/v1/forecast" +
                $"?latitude={latText}&longitude={lonText}" +
                $"&current=temperature_2m,relative_humidity_2m,weather_code,wind_speed_10m" +
                $"&daily=weather_code,temperature_2m_max,temperature_2m_min,precipitation_probability_max" +
                $"&timezone=Asia%2FTokyo";

            // 非同期でAPIを呼び出し、JSONを OpenMeteoResponse に変換
            var response = await _httpClient.GetFromJsonAsync<OpenMeteoResponse>(url);

            if (response?.Current is null || response.Daily is null) {
                throw new InvalidOperationException("天気データの取得に失敗しました（レスポンスが空です）。");
            }

            string displayLocationName = string.IsNullOrWhiteSpace(locationName)
                ? $"緯度 {latText}, 経度 {lonText}"
                : locationName.Trim();

            var summary = new WeatherSummary {
                LocationName = displayLocationName,
                CurrentTemperature = response.Current.Temperature2m,
                CurrentWeatherText = ToJapaneseWeatherText(response.Current.WeatherCode),
                CurrentHumidity = response.Current.RelativeHumidity2m,
                CurrentWindSpeed = response.Current.WindSpeed10m
            };

            // 7日分の配列長を確認しながら DailyForecastItem に変換
            int dayCount = Math.Min(7, response.Daily.Time.Count);

            for (int i = 0; i < dayCount; i++) {
                string dateLabel = FormatDateLabel(response.Daily.Time[i]);
                int code = i < response.Daily.WeatherCode.Count ? response.Daily.WeatherCode[i] : -1;
                double maxTemp = i < response.Daily.Temperature2mMax.Count ? response.Daily.Temperature2mMax[i] : 0;
                double minTemp = i < response.Daily.Temperature2mMin.Count ? response.Daily.Temperature2mMin[i] : 0;
                int precipProb = (i < response.Daily.PrecipitationProbabilityMax.Count &&
                                  response.Daily.PrecipitationProbabilityMax[i].HasValue)
                    ? response.Daily.PrecipitationProbabilityMax[i]!.Value
                    : 0;

                summary.DailyForecasts.Add(new DailyForecastItem {
                    DateText = dateLabel,
                    WeatherText = ToJapaneseWeatherText(code),
                    MaxTemperature = maxTemp,
                    MinTemperature = minTemp,
                    PrecipitationProbability = precipProb
                });
            }

            return summary;
        }

        // "2026-11-24" のような日付を "11/24 (火)" のような表示に変換
        private static string FormatDateLabel(string isoDate) {
            if (DateOnly.TryParseExact(isoDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date)) {
                var jpCulture = new CultureInfo("ja-JP");
                string dayOfWeek = date.ToString("ddd", jpCulture);
                return $"{date:MM/dd} ({dayOfWeek})";
            }

            return isoDate;
        }

        // Open-Meteo の WMO weather_code を日本語に変換
        private static string ToJapaneseWeatherText(int code) => code switch {
            0 => "快晴",
            1 => "おおむね晴れ",
            2 => "一部曇り",
            3 => "曇り",
            45 or 48 => "霧",
            51 or 53 or 55 => "霧雨",
            56 or 57 => "着氷性の霧雨",
            61 or 63 or 65 => "雨",
            66 or 67 => "着氷性の雨",
            71 or 73 or 75 => "雪",
            77 => "霧雪・雪粒",
            80 or 81 or 82 => "にわか雨",
            85 or 86 => "にわか雪",
            95 => "雷雨",
            96 or 99 => "ひょうを伴う雷雨",
            _ => $"不明（コード:{code}）"
        };
    }
}