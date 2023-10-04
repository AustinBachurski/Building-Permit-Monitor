namespace Building_Permit_Monitor.JSON_Objects
{
    public class SavedSearches
    {
        public List<Search> Value { private get; set; }
        public int Status { private get; set; }
        public string Message { private get; set; }

        public List<Search> Searches { get { return Value; } }
        public int StatusCode { get { return Status; } }
        public string StatusMessage { get { return Message; } }


    }

    public class Search
    {
        public int SearchId { get; set; }
        public string SearchName { get; set; }
        public string Description { get; set; }
        public string EmployeeName { get; set; }
    }
}
