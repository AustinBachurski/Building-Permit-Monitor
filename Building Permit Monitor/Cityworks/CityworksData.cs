using Newtonsoft.Json;
using Building_Permit_Monitor.Interfaces;
using Building_Permit_Monitor.JSON_Objects;
using Building_Permit_Monitor.ExcelAccess;
using Building_Permit_Monitor.InfoWindow;

namespace Building_Permit_Monitor.Cityworks
{
    public class CityworksData
    {
        ICityworksAPI _cityworks;

        public CityworksData(ICityworksAPI cityworksAPI)
        {
            _cityworks = cityworksAPI; 
        }

       public async Task<CaseDataDetails> GetCaseDataDetails(int CaDataGroupId, string expectedGroupDesc)
        {
            string jsonData = await _cityworks.RetrieveCaseDataDetailFromServer(CaDataGroupId);

            try
            {
                CaseDataDetails? caseDetails = JsonConvert.DeserializeObject<CaseDataDetails>(jsonData);

                if (caseDetails != null && caseDetails.StatusCode == (int)StatusCode.SUCCESS)
                {
                    if (caseDetails.CaseData != null)
                    {
                        return caseDetails;
                    }

                    caseDetails.CaseData = new List<CaseData> { DefaultValues(expectedGroupDesc) };
                    return caseDetails;
                }

                Thread error = new Thread(() =>
                { Application.Run(new InfoDisplay("Server Error", $"Error retrieving case data details from server:\n\n{caseDetails.StatusMessage}")); });
                error.Start();
                error.Join();
                Application.Exit();
                throw new InvalidOperationException("Application should have closed gracefully by this point, is the world on fire?");
            }
            catch (JsonException jsonError)
            {
                Thread error = new Thread(() =>
                { Application.Run(new InfoDisplay("Exception Encountered", $"Exception encountered deserializing JSON:\n\n{jsonError.Message}")); });
                error.Start();
                error.Join();
                Application.Exit();
                throw new InvalidOperationException("Application should have closed gracefully by this point, is the world on fire?");
            }
        }

        public async Task<CaseDataGroups> GetCaseDataGroups(int CaObjId)
        {
            string jsonData = await _cityworks.RetrieveCaseDataGroupFromServer(CaObjId);

            try
            {
                CaseDataGroups? dataGroups = JsonConvert.DeserializeObject<CaseDataGroups>(jsonData);

                if (dataGroups != null && dataGroups.StatusCode == (int)StatusCode.SUCCESS)
                {
                    return dataGroups;
                }

                Thread error = new Thread(() =>
                { Application.Run(new InfoDisplay("Server Error", $"Error retrieving case data groups from server:\n\n{dataGroups.StatusMessage}")); });
                error.Start();
                error.Join();
                Application.Exit();
                throw new InvalidOperationException("Application should have closed gracefully by this point, is the world on fire?");
            }
            catch (JsonException jsonError)
            {
                Thread error = new Thread(() =>
                { Application.Run(new InfoDisplay("Exception Encountered", $"Exception encountered deserializing JSON:\n\n{jsonError.Message}")); });
                error.Start();
                error.Join();
                Application.Exit();
                throw new InvalidOperationException("Application should have closed gracefully by this point, is the world on fire?");
            }
        }

        public async Task<List<Search>> GetSavedSearchesForUser(string employeeName)
        {
            string jsonData = await _cityworks.RetrieveSavedSearchesFromServer();

            try
            {
                SavedSearches? savedSearches = JsonConvert.DeserializeObject<SavedSearches>(jsonData);

                if (savedSearches != null && savedSearches.StatusCode == (int)StatusCode.SUCCESS)
                {
                    return SearchesByName(savedSearches, employeeName);
                }

                Thread error = new Thread(() =>
                { Application.Run(new InfoDisplay("Server Error", $"Error retrieving saved searches from server:\n\n{savedSearches.StatusMessage}")); });
                error.Start();
                error.Join();
                Application.Exit();
                throw new InvalidOperationException("Application should have closed gracefully by this point, is the world on fire?");
            }
            catch (JsonException jsonError)
            {
                Thread error = new Thread(() =>
                { Application.Run(new InfoDisplay("Exception Encountered", $"Exception encountered deserializing JSON:\n\n{jsonError.Message}")); });
                error.Start();
                error.Join();
                Application.Exit();
                throw new InvalidOperationException("Application should have closed gracefully by this point, is the world on fire?");
            }
        }

        public async Task<SearchResults> GetSearchResults(int searchIdNumber)
        {
            string jsonData = await _cityworks.RetrieveSearchResultsFromServer(searchIdNumber);

            try
            {
                SearchResults? searchResults = JsonConvert.DeserializeObject<SearchResults>(jsonData);

                if (searchResults != null && searchResults.StatusCode == (int)StatusCode.SUCCESS)
                {
                    return searchResults;
                }

                Thread error = new Thread(() =>
                { Application.Run(new InfoDisplay("Server Error", $"Error retrieving search results from server:\n\n{searchResults.StatusMessage}")); });
                error.Start();
                error.Join();
                Application.Exit();
                throw new InvalidOperationException("Application should have closed gracefully by this point, is the world on fire?");
            }
            catch (JsonException jsonError)
            {
                Thread error = new Thread(() =>
                { Application.Run(new InfoDisplay("Exception Encountered", $"Exception encountered deserializing JSON:\n\n{jsonError.Message}")); });
                error.Start();
                error.Join();
                Application.Exit();
                throw new InvalidOperationException("Application should have closed gracefully by this point, is the world on fire?");
            }
        }

        public async Task PopulateSpreadsheetRows(List<SpreadsheetRow> rows)
        {
            foreach (SpreadsheetRow row in rows)
            {
                await PopulateSpreadsheetRow(row);
            }
            return;
        }

        private CaseData DefaultValues(string groupDesc)
        {
            switch(groupDesc)
            {
                case GroupDesc.NumberOfUnits:
                    return new CaseData(groupDesc, null, null, "0");

                case GroupDesc.ProjectValuation:
                    return new CaseData(groupDesc, -1, null, null);

                case GroupDesc.UseOfBuilding:
                case GroupDesc.ClassOfWork:
                    return new CaseData(groupDesc, null, "Data missing in Cityworks.", null);

                default:
                    throw new InvalidOperationException("groupDesc was invalid");
            }
        }

        private List<Search> SearchesByName(SavedSearches searches, string employeeName)
        {
            List<Search> employeeSearches = new List<Search>();

            foreach (Search search in searches.Searches)
            {
                if (search.EmployeeName == employeeName)
                {
                    employeeSearches.Add(search);
                }
            }
            return employeeSearches;
        }

        private async Task PopulateSpreadsheetRow(SpreadsheetRow row)
        {
            // Holds data group id numbers for each detail group.
            CaseDataGroups dataGroups = await GetCaseDataGroups(row.CaObjectId);

            // Each group is accessed via it's group id number;
            CaseDataDetails buildingUseDetails = await GetCaseDataDetails(dataGroups.BuildingUseID, GroupDesc.UseOfBuilding);
            CaseDataDetails numbeOfUnitsDetails = await GetCaseDataDetails(dataGroups.NumberOfUnitsID, GroupDesc.NumberOfUnits);
            CaseDataDetails classOfWorkDetails = await GetCaseDataDetails(dataGroups.ClassOfWorkID, GroupDesc.ClassOfWork);
            CaseDataDetails projectValueDetails = await GetCaseDataDetails(dataGroups.ProjectValueID, GroupDesc.ProjectValuation);

            // The data is then shoved into the row object.
            row.BuildingUse = buildingUseDetails.BuildingUse;
            row.NumberOfUnits = numbeOfUnitsDetails.NumberOfUnits;
            row.ClassOfWork = classOfWorkDetails.ClassOfWork;
            row.ProjectValue = projectValueDetails.ProjectValue;

            return;
        }
    }
}
