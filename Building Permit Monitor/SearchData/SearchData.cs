namespace Building_Permit_Monitor.CityworksSearchData
{
    public class SearchData
    {
        public SearchData(string pathToFile)
        {
            _dataFile = pathToFile;
            readFileData(pathToFile);
        }

        string _dataFile;
        string _employeeName;
        int _savedSearchID;

        public string Employee { get { return _employeeName; } set { _employeeName = value; } }
        public int SearchID { get { return _savedSearchID; } set { _savedSearchID = value; } }

        public string DataFile { get { return _dataFile; } }

        void readFileData(string filePath)
        {
            string? line;

            if (!File.Exists(filePath))
            {
                var f = File.Create(filePath);
                f.Close();
            }

            using (StreamReader file = new StreamReader(filePath))
            {
                if ((line = file.ReadLine()) != null)
                {
                    _employeeName = line;
                }
                else
                {
                    _employeeName = "BACHURSKI, AUSTIN ";  // Trailing whitespace intentional.
                }

                if ((line = file.ReadLine()) != null)
                {
                    if(!int.TryParse(line, out _savedSearchID))
                    { }
                }
                else
                {
                    _savedSearchID = 1174;
                }
            }
        }
    }
}
