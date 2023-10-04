using Building_Permit_Monitor.ExcelAccess;
using Building_Permit_Monitor.JSON_Objects;

namespace Building_Permit_Monitor.Interfaces
{
    public interface ICityworksData
    {
        Task<CaseDataDetails> GetCaseDataDetails(int CaDataGroupId, string expectedGroupDesc);
        Task<CaseDataGroups> GetCaseDataGroups(int CaObjectId);
        Task<List<Search>> GetSavedSearchesForUser(string employeeName);
        Task<SearchResults> GetSearchResults(int searchIdNumber);
        Task PopulateSpreadsheetRows(List<SpreadsheetRow> rows);
    }
}
