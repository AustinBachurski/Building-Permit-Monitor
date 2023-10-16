namespace Building_Permit_Monitor.Interfaces
{
    public interface ICityworksAPI
    {
        public Task<string> RetrieveCaseDataDetailFromServerAsync(int CaDataGroupId);
        public Task<string> RetrieveCaseDataGroupFromServerAsync(int CaObjId);
        public Task<string> RetrieveSavedSearchesFromServerAsync();
        public Task<string> RetrieveSearchResultsFromServerAsync(int searchIdNumber);
    }
}
