namespace Building_Permit_Monitor.JSON_Objects
{
    public class CaseDataDetails
    {
        public List<CaseData> Value {  private get; set; }
        public int Status { private get; set; }
        public string Message { private get; set; }

        public List<CaseData> CaseData { get { return Value; } set { Value = value; } }
        public int StatusCode { get { return Status; } }
        public string StatusMessage { get { return Message; } }

        public string BuildingUse
        {
            get
            {
                CaseData? entry = Value.FirstOrDefault(entry => entry.GroupDesc == GroupDesc.UseOfBuilding);
                
                if (entry != null && entry.ListValue != null)
                {
                    if (!string.IsNullOrEmpty(entry.ListValue))
                    {
                        return entry.ListValue;
                    }

                    return "No data entered in Cityworks.";
                }
                return "Invalid Detail Group";
            }
        }

        public string NumberOfUnits
        {
            get
            {
                CaseData? entry = Value.FirstOrDefault(entry => entry.GroupDesc == GroupDesc.NumberOfUnits);

                if (entry != null && entry.TextValue != null)
                {
                    if (!string.IsNullOrEmpty(entry.TextValue))
                    {
                        return entry.TextValue;
                    }

                    return "0";
                }
                return "Invalid Detail Group";
            }
        }

        public string ClassOfWork
        {
            get
            {
                CaseData? entry = Value.FirstOrDefault(entry => entry.GroupDesc == GroupDesc.ClassOfWork);

                if (entry != null && entry.ListValue != null)
                {
                    if (!string.IsNullOrEmpty(entry.ListValue))
                    {
                        return entry.ListValue;
                    }

                    return "No data entered in Cityworks.";
                }
                return "Invalid Detail Group";
            }
        }

        public double ProjectValue
        {
            get
            {
                CaseData? entry = Value.FirstOrDefault(entry => entry.GroupDesc == GroupDesc.ProjectValuation);

                if (entry != null && entry.Value != null)
                {
                    return (double)entry.Value;
                }
                return -1;
            }
        }
    }

    public class CaseData
    {
        public CaseData(string GroupDescription, double? dataValue, string? dataListValue, string? dataTextValue)
        {
            GroupDesc = GroupDescription;
            Value = dataValue;
            ListValue = dataListValue;
            TextValue = dataTextValue;
        }
        public string GroupDesc { get; set; }
        public double? Value { get; set; }
        public string? ListValue { get; set; }
        public string? TextValue { get; set; }
    }
}
