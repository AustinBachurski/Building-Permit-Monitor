namespace Building_Permit_Monitor.DataValidation
{
    public enum BuildingUse
    {
        Commercial = 0,
        SingleFamilyResidence = 1,
        Townhouse = 2,
        Duplex = 3,
        Apartments = 4,
        Quasi = 5,
        HealthCare = 6,
    }

    public enum ClassOfWork
    {
        SFRNEW = 0,
        SFRMOD = 1,
        COMNEW = 2,
        COMMOD = 3,
        DUPNEW = 4,
        DUPMOD = 5,
        THNEW = 6,
    }
}