namespace Building_Permit_Monitor.DataValidationWindow
{
    public static class Values
    {
        private static readonly string[] _validBuildingUse = {
            "Commercial",
            "Single Family Residence",
            "Townhouse",
            "Duplex",
            "Apartments",
            "Quasi",
            "Health Care" };

        private static readonly string[] _validClassOfWork = {
            "SFR-NEW",
            "SFR-MOD",
            "COM-NEW",
            "COM-MOD",
            "DUP-NEW",
            "DUP-MOD",
            "TH-NEW" };

        public static bool IsValidBuildingUse(string value)
        {
            return _validBuildingUse.Contains(value);
        }

        public static bool IsValidClassOfWork(string value)
        {
            return _validClassOfWork.Contains(value);
        }
    }
}
