﻿using Microsoft.Toolkit.Uwp.Notifications;
using Building_Permit_Monitor.ExcelAccess;
using Building_Permit_Monitor.Cityworks;
using Building_Permit_Monitor.JSON_Objects;
using Building_Permit_Monitor.DataValidation;
using Building_Permit_Monitor.TrayApplication;
using Building_Permit_Monitor.InfoWindow;
using Building_Permit_Monitor.CityworksSearchData;
using Windows.Media.AppBroadcasting;

namespace Building_Permit_Monitor.Permit_Monitor
{
    public class PermitMonitor
    {
        bool _abort = false;
        CityworksData _cityworks;
        string _dataFile;
        string _employeeName;
        Excel _excel;
        int _savedSearchID;
        List<SpreadsheetRow> _unmappedPermits;
        
        public PermitMonitor(CityworksData cityworks, Excel excel, SearchData data)
        {
            _cityworks = cityworks;
            _dataFile = data.DataFile;
            _employeeName = data.Employee;
            _excel = excel;
            _savedSearchID = data.SearchID;
            _unmappedPermits = new List<SpreadsheetRow>();
        }

        public string EmployeeName { get { return _employeeName; } set { _employeeName = value; } }
        public int SearchID { get { return _savedSearchID; } set { _savedSearchID = value; } }
        public string DataFile { get { return _dataFile; } }

        public void Abort()
        {
            _abort = true;
        }

        public void ChangeSearchID(int newID)
        {
            _savedSearchID = newID;
        }

        public async Task<int> CheckForWorkToDoAsync()
        {
            _unmappedPermits =  await CheckForUnmappedPermitsAsync();

            return _unmappedPermits.Count;
        }

        public async Task DisplaySavedSearchesForUserAsync()
        {
            List<Search> searches = await _cityworks.GetSavedSearchesForUserAsync(_employeeName);

            string searchText = "";

            foreach (Search search in searches)
            {
                searchText += $"Search ID: {search.SearchId}\n"
                            + $"Search Name: {search.SearchName}\n"
                            + $"Description: {search.Description}";
            }

            Thread showSearch = new Thread(() => 
            { Application.Run(new InfoDisplay($"Saved Searches for {_employeeName}", searchText)); });
            showSearch.Start();
        }

        public void DisplayRowsForUserValidation()
        {
            _abort = false;
            int current = 1;
            int total = _unmappedPermits.Count;

            foreach (SpreadsheetRow row in _unmappedPermits)
            {
                DisplayAndValidate(row, current, total);
                ++current;

                if (_abort) { return; }
            }
        }

        public async Task DoWorkAsync()
        {
            await _cityworks.PopulateSpreadsheetRowsAsync(_unmappedPermits);
            await Console.Out.WriteLineAsync("Data gathered, pushing to sheet.");

            DisplayRowsForUserValidation(); // May set _abort to true.

            if (!_abort)
            {
                _excel.PushRowsToSpreadsheet(_unmappedPermits);
                SendToast(OnToastClick.NoAction, "Spreadsheet updated.");
                return;
            }
            SendToast(OnToastClick.NoAction, "Update aborted.");
        }

        private async Task<List<SpreadsheetRow>> CheckForUnmappedPermitsAsync()
        {
            SearchResults issuedPermits = await _cityworks.GetSearchResultsAsync(_savedSearchID);
            string[] completedPermits = _excel.GetCompletedPermitNumbers();

            List<SpreadsheetRow> _unmappedPermits = new List<SpreadsheetRow>();

            foreach (SearchResult issuedPermit in issuedPermits.Results)
            {
                if (!completedPermits.Contains(issuedPermit.CaseNumber))
                {
                    _unmappedPermits.Add(new SpreadsheetRow(issuedPermit.CaseNumber,
                        issuedPermit.DateIssued, issuedPermit.Address, issuedPermit.Notes,
                        issuedPermit.CoordinateX, issuedPermit.CoordinateY, issuedPermit.CaObjectId));
                }
            }
            return _unmappedPermits;
        }

        private void SendToast(string argument, string message)
        {
            ToastContentBuilder toast = new ToastContentBuilder();
            toast.AddArgument(argument);
            toast.AddText(message);
            toast.Show();
        }

        private void DisplayAndValidate (SpreadsheetRow row, int current,  int total)
        {
            Application.Run(new ValidationWindow(row, current, total, this));
        }
    }
}
