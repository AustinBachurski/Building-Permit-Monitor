﻿using Newtonsoft.Json;
using Building_Permit_Monitor.Cityworks;
using Building_Permit_Monitor.Interfaces;
using Building_Permit_Monitor.JSON_Objects;
using Building_Permit_Monitor.InfoWindow;

namespace Building_Permit_Monitor.CityworksAPI
{
    public class LiveCityworksAPI : ICityworksAPI
    {
        public LiveCityworksAPI(string URL)  // https://cityworks.ci.kalispell.mt.us/Cityworks/
        {
            baseURL = URL;
        }

        private static HttpClient httpClient = new HttpClient();
        public static string baseURL;
        private DateTime _tokenExpiration = DateTime.Now;
        private string? _token;

        public async Task<string> RetrieveCaseDataDetailFromServerAsync(int CaDataGroupId)
        {
            try
            {
                string requestMessage = $"{baseURL}Services/Pll/CaseDataDetail/SearchObject"
                    + $"?data={{CaDataGroupId:{CaDataGroupId}}}&token={await TokenAsync()}";

                HttpResponseMessage response = await httpClient.GetAsync(requestMessage);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }

                return "";
            }
            catch (HttpRequestException httpError)
            {
                Thread error = new Thread(() =>
                { Application.Run(new InfoDisplay("HTTP Error", $"Exception encountered during HTTP request:\n\n{httpError.Message}")); });
                error.Start();
                error.Join();
                Application.Exit();
                throw new InvalidOperationException("Application should have closed gracefully by this point, is the world on fire?");
            }
        }

        public async Task<string> RetrieveCaseDataGroupFromServerAsync(int CaObjId)
        {
            try
            {
                string requestMessage = $"{baseURL}Services/Pll/CaseDataGroup/ByCaObjectId"
                    + $"?data={{CaObjectId:{CaObjId}}}&token={await TokenAsync()}";

                HttpResponseMessage response = await httpClient.GetAsync(requestMessage);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }

                return "";
            }
            catch (HttpRequestException httpError)
            {
                Thread error = new Thread(() =>
                { Application.Run(new InfoDisplay("HTTP Error", $"Exception encountered during HTTP request:\n\n{httpError.Message}")); });
                error.Start();
                error.Join();
                Application.Exit();
                throw new InvalidOperationException("Application should have closed gracefully by this point, is the world on fire?");
            }
        }

        public async Task<string> RetrieveSavedSearchesFromServerAsync()
        {
            try
            {
                string requestMessage = $"{baseURL}Services/Ams/Search/PllSaved?token={await TokenAsync()}";

                HttpResponseMessage response = await httpClient.GetAsync(requestMessage);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }

                return "";
            }
            catch (HttpRequestException httpError)
            {
                Thread error = new Thread(() =>
                { Application.Run(new InfoDisplay("HTTP Error", $"Exception encountered during HTTP request:\n\n{httpError.Message}")); });
                error.Start();
                error.Join();
                Application.Exit();
                throw new InvalidOperationException("Application should have closed gracefully by this point, is the world on fire?");
            }
        }

        public async Task<string> RetrieveSearchResultsFromServerAsync(int searchIdNumber)
        {
            try
            {
                string requestMessage = $"{baseURL}Services/Ams/Search/Execute"
                    + $"?data={{\"SearchId\":{searchIdNumber}}}&token={await TokenAsync()}";

                HttpResponseMessage response = await httpClient.GetAsync(requestMessage);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return content;
                }

                return "";
            }
            catch (HttpRequestException httpError)
            {
                Thread error = new Thread(() =>
                { Application.Run(new InfoDisplay("HTTP Error", $"Exception encountered during HTTP request:\n\n{httpError.Message}")); });
                error.Start();
                error.Join();
                Application.Exit();
                throw new InvalidOperationException("Application should have closed gracefully by this point, is the world on fire?");
            }
        }

        private async Task<string> RetrieveTokenFromServerAsync()
        {
            try
            {
                string requestMessage = $"{baseURL}Services/General/Authentication/Authenticate?"
                    + $"data={{\"LoginName\":\"{Environment.GetEnvironmentVariable("CITYWORKS_UN")}\","
                    + $"\"Password\":\"{Environment.GetEnvironmentVariable("CITYWORKS_PW")}\"}}";

                HttpResponseMessage response = await httpClient.GetAsync(requestMessage);
                string content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return content;
                }
                else
                {
                    Thread error = new Thread(() =>
                    { Application.Run(new InfoDisplay("HTTP Error", $"HTTP request failed with status code: {response.StatusCode}")); });
                    error.Start();
                    error.Join();
                    Application.Exit();
                    throw new InvalidOperationException("Application should have closed gracefully by this point, is the world on fire?");
                }
            }
            catch (HttpRequestException httpError)
            {
                Thread error = new Thread(() =>
                { Application.Run(new InfoDisplay("HTTP Error", $"Exception encountered during HTTP request:\n\n{httpError.Message}")); });
                error.Start();
                error.Join();
                Application.Exit();
                throw new InvalidOperationException("Application should have closed gracefully by this point, is the world on fire?");
            }
        }

        private async Task<string> TokenAsync()
        {
            if (_tokenExpiration > DateTime.Now)
            {
                return _token;
            }
            await UpdateTokenAsync();
            return _token;
        }

    private async Task UpdateTokenAsync()
        {
            string serverResponse = await RetrieveTokenFromServerAsync();

            try
            {
                OAuthToken? tokenObject = JsonConvert.DeserializeObject<OAuthToken>(serverResponse);

                if (tokenObject != null && tokenObject.StatusCode == (int)StatusCode.SUCCESS)
                {
                    _tokenExpiration = DateTime.Now.AddMinutes(50);
                    _token = tokenObject.Token;
                    return;
                }
                Thread error = new Thread(() =>
                { Application.Run(new InfoDisplay("Token Error", tokenObject.StatusMessage.ToString())); });
                error.Start();
                error.Join();
                Application.Exit();
                throw new InvalidOperationException("Application should have closed gracefully by this point, is the world on fire?");
            }
            catch (JsonException jsonError)
            {
                Thread error = new Thread(() =>
                { Application.Run(new InfoDisplay("JSON Error", $"Exception encountered deserializing JSON:\n\n{jsonError.Message}")); });
                error.Start();
                error.Join();
                Application.Exit();
                throw new InvalidOperationException("Application should have closed gracefully by this point, is the world on fire?");
            }
        }
    } 
}