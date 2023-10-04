namespace Building_Permit_Monitor.JSON_Objects
{
    public class CaseDataGroups
    {
        public List<CaseDataGroupId> Value { get; set; }
        public int Status{ private get; set; }
        public string? Message {  private get; set; }

        public int StatusCode { get { return Status; } }
        public string? StatusMessage { get { return Message; } }

        public int BuildingUseID
        {
            get
            {
                CaseDataGroupId? entry = Value.FirstOrDefault(entry => entry.GroupDesc == GroupDesc.UseOfBuilding);

                if (entry != null)
                {
                    return entry.CaDataGroupId;
                }
                return 0;
            }
        }
        public int NumberOfUnitsID
        {
            get
            {
                CaseDataGroupId? entry = Value.FirstOrDefault(entry => entry.GroupDesc == GroupDesc.NumberOfUnits);

                if (entry != null)
                {
                    return entry.CaDataGroupId;
                }
                return 0;
            }
        }
        public int ClassOfWorkID
        {
            get
            {
                CaseDataGroupId? entry = Value.FirstOrDefault(entry => entry.GroupDesc == GroupDesc.ClassOfWork);

                if (entry != null)
                {
                    return entry.CaDataGroupId;
                }
                return 0;
            }
        }
        public int ProjectValueID
        {
            get
            {
                CaseDataGroupId? entry = Value.FirstOrDefault(entry => entry.GroupDesc == GroupDesc.ProjectValuation);

                if (entry != null)
                {
                    return entry.CaDataGroupId;
                }
                return 0;
            }
        }
    }
    
    public class CaseDataGroupId
    {
        public int CaDataGroupId { get; set; }
        public string GroupDesc { get; set; }
    }
}
