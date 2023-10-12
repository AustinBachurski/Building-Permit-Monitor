using Building_Permit_Monitor.ExcelEnums;
using System.Runtime.InteropServices;
using XL = Microsoft.Office.Interop.Excel;


namespace Building_Permit_Monitor.ExcelAccess
{
    public class Excel
    {
        public Excel(string filePath)
        {
            _filePath = filePath;
            _sheetName = DateTime.Now.Year.ToString();
        }

        private string _filePath;
        private string _sheetName;

        public string[] GetCompletedPermitNumbers()
        {
            XL.Application xlApp = new XL.Application();
            XL.Workbook xlWorkbook = xlApp.Workbooks.Open(_filePath);
            XL.Worksheet xlWorksheet = xlWorkbook.Worksheets[SheetIndex(xlWorkbook, _sheetName)];
            XL.Range xlRange = xlWorksheet.UsedRange;

            string[] permitNumbers = ReadPermitNumbers(xlWorksheet, xlRange);

            GC.Collect();
            GC.WaitForPendingFinalizers();
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);

            return permitNumbers;
        }
        
        public void PushRowsToSpreadsheet(List<SpreadsheetRow> rows)
        {
            XL.Application xlApp = new XL.Application();
            XL.Workbook xlWorkbook = xlApp.Workbooks.Open(_filePath);
            XL.Worksheet xlWorksheet = xlWorkbook.Worksheets[SheetIndex(xlWorkbook, _sheetName)];
            XL.Range xlRange = xlWorksheet.UsedRange;

            int insertionRow = xlRange.Rows.Count + 1;

            // Sort by DateIssued, newest first.
            rows.Sort((a, b) => b.DateIssued.CompareTo(a.DateIssued));

            foreach (SpreadsheetRow row in rows)
            {
                xlWorksheet.Cells[insertionRow, Column.Permit_Number] = row.PermitNumber;
                xlWorksheet.Cells[insertionRow, Column.Building_Use] = row.BuildingUse;
                xlWorksheet.Cells[insertionRow, Column.Number_of_Units] = int.Parse(row.NumberOfUnits);
                xlWorksheet.Cells[insertionRow, Column.Class_of_Work] = row.ClassOfWork;
                xlWorksheet.Cells[insertionRow, Column.Date_Issued] = row.DateIssued;
                xlWorksheet.Cells[insertionRow, Column.Project_Value] = row.ProjectValue;
                xlWorksheet.Cells[insertionRow, Column.Address] = row.Address;
                xlWorksheet.Cells[insertionRow, Column.Notes] = row.Notes;
                xlWorksheet.Cells[insertionRow, Column.CoordinateX] = row.CoordinateX;
                xlWorksheet.Cells[insertionRow, Column.CoordinateY] = row.CoordinateY;

                ++insertionRow;
            }

            xlWorkbook.Save();

            GC.Collect();
            GC.WaitForPendingFinalizers();
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
        }

        private void PrintWorksheet(XL.Worksheet worksheet, XL.Range range)
        {
            int populatedRows = range.Rows.Count;
            int populatedColumns = range.Columns.Count;

            for (int i = 1; i <= populatedRows; ++i)
            {
                for (int j = 1; j <= populatedColumns; ++j)
                {
                    if (j == 1)
                    {
                        Console.WriteLine();
                    }
                    if (range.Cells[i, j] != null && range.Cells[i, j].Value != null)
                    {
                        Console.Write(range.Cells[i, j].Value.ToString() + '\t');
                    }
                }
            }
        }

        private string[] ReadPermitNumbers(XL.Worksheet worksheet, XL.Range range)
        {
            int populatedRows = range.Rows.Count;
            int insertionIndex = 0;
            string[] permitNumbers = new string[populatedRows - (int)ExcelData.Header];

            for (int i = (int)ExcelData.Start; i <= range.Rows.Count; ++i)
            {
                if (range.Cells[i, Column.Permit_Number] != null
                    && range.Cells[i, Column.Permit_Number].Value != null)
                {
                    permitNumbers[insertionIndex] = range.Cells[i, Column.Permit_Number].Value.ToString();
                    ++insertionIndex;
                }
            }
            return permitNumbers;
        }

        private int SheetIndex(XL.Workbook workbook, string sheetName)
        {
            int sheetIndex = 1; // Excel is not zero indexed.
            
            foreach (XL.Worksheet wSheet in workbook.Worksheets)
            {
                if (wSheet.Name == sheetName)
                {
                    return sheetIndex;
                }
                ++sheetIndex;
            }
            throw new KeyNotFoundException($"{sheetName} was not found in the workbook.");
        }
    }
}
