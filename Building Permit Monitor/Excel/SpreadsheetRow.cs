namespace Building_Permit_Monitor.ExcelAccess
{
    public class SpreadsheetRow
    {
        public SpreadsheetRow(string PermitNumber, string DateIssued, string Address,
            string Notes, double? xCoordinate, double? yCoordinate, int CaObjectId)
        {
            _permitNumber = PermitNumber;
            _buildingUse = BuildingUse;
            _dateIssued = DateIssued;
            _address = Address;
            _notes = Notes;
            _caObjectId = CaObjectId;

            if (xCoordinate != null)
            {
                _x = (double)xCoordinate;
            }
            else
            {
                _x = 0;
            }

            if (yCoordinate != null)
            {
                _y = (double)yCoordinate;
            }
            else
            {
                _y = 0;
            }
        }

        private string _permitNumber;
        private string _buildingUse;
        private string _classOfWork;
        private string _numberOfUnits;
        private string _dateIssued;
        private double _projectValue;
        private string _address;
        private string _notes;
        private double _x;
        private double _y;
        private int _caObjectId;
        
        public string PermitNumber { get { return _permitNumber; } }
        public string BuildingUse { get { return _buildingUse; } set { _buildingUse = value; } }
        public string ClassOfWork { get { return _classOfWork; } set { _classOfWork = value; } }
        public string NumberOfUnits { get { return _numberOfUnits; } set { _numberOfUnits = value; } }
        public string DateIssued { get { return _dateIssued; } }
        public double ProjectValue { get { return _projectValue; } set { _projectValue = value; } }
        public string Address { get { return _address; } }
        public string Notes { get { return _notes; } }
        public double CoordinateX { get { return _x; } set { _x = value; } }
        public double CoordinateY { get { return _y; } set { _y = value; } }
        public int CaObjectId { get { return _caObjectId; } }
    }
}
