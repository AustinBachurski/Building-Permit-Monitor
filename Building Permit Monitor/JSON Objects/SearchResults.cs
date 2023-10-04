namespace Building_Permit_Monitor.JSON_Objects
{
    public class SearchResults
    {
        public List<SearchResult> Value { private get; set; }
        public int Status { private get; set; }
        public string Message { private get; set; }

        public List<SearchResult> Results { get { return Value; } }
        public int StatusCode { get { return Status; } } 
        public string StatusMessage { get { return Message; } }
    }

    public class SearchResult
    {
        public SearchResult(double CA_OBJECT_ID, string CASE_NUMBER, string CASE_NAME,
                            double? CX, double? CY, string LOCATION, DateTime TASK_COMPLETE_DATE)
        {
            _CaObjectId = CA_OBJECT_ID;
            CaseNumber = CASE_NUMBER;
            Notes = CASE_NAME;
            CoordinateX = CX;
            CoordinateY = CY;
            Address = LOCATION;
            _DateIssued = TASK_COMPLETE_DATE; // "TASK_COMPLETE_DATE" NOT "COMPLETED!
        }

        public int CaObjectId { get { return (int)_CaObjectId; } }
        public string DateIssued { get { return _DateIssued.ToString().Split(' ')[0]; } }
        public string CaseNumber { get; set; }
        public string Notes { get; set; }
        public double? CoordinateX { get; set; }
        public double? CoordinateY { get; set; }
        public string Address { get; set; }
        private double _CaObjectId { get; set; }
        private DateTime _DateIssued { get; set; }
    }
}
