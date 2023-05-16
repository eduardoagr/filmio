﻿using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace filmio.Services {
    public class DailyCoServices {

        HttpClient httpClient;
        private const string API_KEY = "31fdb55d1a20d42597887e4049eec15c811c0bbe877bfac98b66a5b45dc77719";
        private const string END_POINT = "https://api.daily.co/v1/rooms";


        public DailyCoServices() {
            httpClient = new HttpClient();
        }

        public async Task<string?> CreateRoomAsync() {

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                API_KEY);

            var roomName = "test";
            var payload = new {

                name = roomName,
                properties = new {
                    enable_screenshare = true,
                    enable_recording = "local",
                }
            };

            var response = await httpClient.PostAsync(END_POINT,
                new StringContent(JsonSerializer.Serialize(payload),
                Encoding.UTF8, "application/json"));

            if (response != null && response.IsSuccessStatusCode) {

                var responseBody = await response.Content.ReadAsStringAsync();
                using JsonDocument document = JsonDocument.Parse(responseBody);
                string? url = document.RootElement.GetProperty("url").GetString();
                return url!;
            }

            return null;


        }
    }
}
