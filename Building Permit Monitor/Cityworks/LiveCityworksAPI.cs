using Newtonsoft.Json;
using Building_Permit_Monitor.Cityworks;
using Building_Permit_Monitor.Interfaces;
using Building_Permit_Monitor.JSON_Objects;
using Building_Permit_Monitor.InfoWindow;

namespace Building_Permit_Monitor.CityworksAPI
{
    /// <summary>
    /// Provides access to the live production instance of Cityworks.
    /// </summary>
    public class LiveCityworksAPI : ICityworksAPI
    {
        /// <summary>
        /// Initializes a new instance of <see cref="LiveCityworksAPI"/> with the provided URL.
        /// </summary>
        /// <param name="URL">URL to your Cityworks site, "/Cityworks/" suffix expected.
        /// Example: "https://cityworks.ci.cityname.state.us/Cityworks/" </param>
        ///
        public LiveCityworksAPI(string URL)  // https://cityworks.ci.kalispell.mt.us/Cityworks/
        {
            baseURL = URL;
        }

        private static HttpClient httpClient = new HttpClient();
        public static string baseURL;
        private DateTime _tokenExpiration = DateTime.Now;
        private string? _token;

        public async Task<string> RetrieveCaseDataDetailFromServer(int CaDataGroupId)
        {
            try
            {
                string requestMessage = $"{baseURL}Services/Pll/CaseDataDetail/SearchObject"
                    + $"?data={{CaDataGroupId:{CaDataGroupId}}}&token={await Token()}";

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

        public async Task<string> RetrieveCaseDataGroupFromServer(int CaObjId)
        {
            try
            {
                string requestMessage = $"{baseURL}Services/Pll/CaseDataGroup/ByCaObjectId"
                    + $"?data={{CaObjectId:{CaObjId}}}&token={await Token()}";

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

        public async Task<string> RetrieveSavedSearchesFromServer()
        {
            try
            {
                string requestMessage = $"{baseURL}Services/Ams/Search/PllSaved?token={await Token()}";

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

        public async Task<string> RetrieveSearchResultsFromServer(int searchIdNumber)
        {
            try
            {
                string requestMessage = $"{baseURL}Services/Ams/Search/Execute"
                    + $"?data={{\"SearchId\":{searchIdNumber}}}&token={await Token()}";

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

        private async Task<string> RetrieveTokenFromServer()
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

        private async Task<string> Token()
        {
            if (_tokenExpiration > DateTime.Now)
            {
                return _token;
            }
            await UpdateToken();
            return _token;
        }

    private async Task UpdateToken()
        {
            string serverResponse = await RetrieveTokenFromServer();

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