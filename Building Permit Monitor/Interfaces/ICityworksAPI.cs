namespace Building_Permit_Monitor.Interfaces
{
    public interface ICityworksAPI
    {
        public Task<string> RetrieveCaseDataDetailFromServer(int CaDataGroupId);
        public Task<string> RetrieveCaseDataGroupFromServer(int CaObjId);
        public Task<string> RetrieveSavedSearchesFromServer();
        public Task<string> RetrieveSearchResultsFromServer(int searchIdNumber);
    }
}
