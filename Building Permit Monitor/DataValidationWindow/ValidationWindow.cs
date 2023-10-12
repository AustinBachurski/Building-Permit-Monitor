using System.Diagnostics;
using Building_Permit_Monitor.TrayApplication;
using Building_Permit_Monitor.CityworksAPI;
using Building_Permit_Monitor.ExcelAccess;
using Building_Permit_Monitor.Permit_Monitor;
using Microsoft.Toolkit.Uwp.Notifications;

namespace Building_Permit_Monitor.DataValidation
{
    public partial class ValidationWindow : Form
    {
        readonly int _CaObjectId;
        SpreadsheetRow _rowData;
        PermitMonitor _process;

        public ValidationWindow(SpreadsheetRow row, int current, int total, PermitMonitor process)
        {
            _CaObjectId = row.CaObjectId;
            _rowData = row;
            _process = process;

            InitializeComponent();

            Text = $"Data Validation for permit {current} of {total}";

            if (row.BuildingUse == "No data entered in Cityworks.") { label_title_BuildingUse.ForeColor = Color.Red; }
            int _unused_;
            if (!int.TryParse(row.NumberOfUnits, out _unused_)) { label_title_NumberOfUnits.ForeColor = Color.Red; }
            if (row.ClassOfWork == "No data entered in Cityworks.") { label_title_ClassOfWork.ForeColor = Color.Red; }
            if (row.CoordinateX == 0 || row.CoordinateY == 0) { label_title_Coordinates.ForeColor = Color.Red; }

            label_PermitNumber.Text = row.PermitNumber;
            label_BuildingUse.Text = row.BuildingUse;
            label_NumberOfUnits.Text = row.NumberOfUnits;
            label_ClassOfWork.Text = row.ClassOfWork;
            label_DateIssued.Text = row.DateIssued;
            label_ProjectValueation.Text = $"{row.ProjectValue:C}";
            label_Address.Text = row.Address;
            label_Notes.Text = row.Notes;
            label_CoordinatesX.Text = $"{row.CoordinateX}";
            label_CoordinatesY.Text = $"{row.CoordinateY}";

            comboBox_BuildingUse.SelectedIndex = SetBuildingUseSelectionIfValid(row.BuildingUse);
            comboBox_ClassOfWork.SelectedIndex = SetClassOfWorkSelectionIfValid(row.ClassOfWork);

            AcceptButton = button_OpenCityworks;
        }

        private int SetClassOfWorkSelectionIfValid(string value)
        {
            switch (value)
            {
                case "SFR-NEW":
                    return (int)ClassOfWork.SFRNEW;

                case "SFR-MOD":
                    return (int)ClassOfWork.SFRMOD;

                case "COM-NEW":
                    return (int)ClassOfWork.COMNEW;

                case "COM-MOD":
                    return (int)ClassOfWork.COMMOD;

                case "DUP-NEW":
                    return (int)ClassOfWork.DUPNEW;

                case "DUP-MOD":
                    return (int)ClassOfWork.DUPMOD;

                case "TH_NEW":
                    return (int)ClassOfWork.THNEW;

                default:
                    return -1;
            }
        }

        private int SetBuildingUseSelectionIfValid(string value)
        {
            switch (value)
            {
                case "Commercial":
                    return (int)BuildingUse.Commercial;

                case "Single Family Residence":
                    return (int)BuildingUse.SingleFamilyResidence;

                case "Towhhouse":
                    return (int)BuildingUse.Townhouse;

                case "Duplex":
                    return (int)BuildingUse.Duplex;

                case "Apartments":
                    return (int)BuildingUse.Apartments;

                case "Quasi":
                    return (int)BuildingUse.Quasi;

                case "Health Care":
                    return (int)BuildingUse.HealthCare;

                default:
                    return -1;
            }
        }

        private void Button_Abort(object sender, EventArgs e)
        {
            _process.Abort();
            Close();
        }

        private void Button_Submit(object sender, EventArgs e)
        {
            double x = -1;
            double y = -1;
            int _unused_;

            if (comboBox_BuildingUse.SelectedItem != null)
            {
                _rowData.BuildingUse = comboBox_BuildingUse.SelectedItem.ToString();
            }

            if (textBox_NumberOfUnits.Text != string.Empty)
            {
                if (int.TryParse(textBox_NumberOfUnits.Text, out _unused_))
                {
                    _rowData.NumberOfUnits = textBox_NumberOfUnits.Text;
                }
                else
                {
                    SendToast(OnToastClick.NoAction, $"Value entered for Number of Units was invalid: {textBox_NumberOfUnits.Text}");
                    return;
                }
            }

            if (comboBox_ClassOfWork.SelectedItem != null)
            {
                _rowData.ClassOfWork = comboBox_ClassOfWork.SelectedItem.ToString();
            }

            if (textBox_CoordinateX.Text != string.Empty)
            {
                if (double.TryParse(textBox_CoordinateX.Text, out x))
                {
                    _rowData.CoordinateX = x;
                }
                else
                {
                    SendToast(OnToastClick.NoAction, $"Value entered for X was invalid: {textBox_CoordinateX.Text}");
                    return;
                }
            }

            if (textBox_CoordinateY.Text != string.Empty)
            {
                if (double.TryParse(textBox_CoordinateY.Text, out x))
                {
                    _rowData.CoordinateY = y;
                }
                else
                {
                    SendToast(OnToastClick.NoAction, $"Value entered for Y was invalid: {textBox_CoordinateX.Text}");
                    return;
                }
            }
            Close();
        }

        private void Button_OpenCityworks(object sender, EventArgs e)
        {
            var openWebPage = new ProcessStartInfo
            {
                FileName = $"{LiveCityworksAPI.baseURL}NavigationRedirect.aspx?MapCaObjectId={_CaObjectId}",
                UseShellExecute = true
            };

            Process.Start(openWebPage);
        }

        private void SendToast(string argument, string message)
        {
            ToastContentBuilder toast = new ToastContentBuilder();
            toast.AddArgument(argument);
            toast.AddText(message);
            toast.Show();
        }
    }
}
