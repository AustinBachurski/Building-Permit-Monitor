using Building_Permit_Monitor.Permit_Monitor;
using Building_Permit_Monitor.TrayApplication;
using Microsoft.Toolkit.Uwp.Notifications;

namespace Building_Permit_Monitor.SearchIdWindow
{
    public partial class SearchID : Form
    {
        PermitMonitor _monitor;

        public SearchID(PermitMonitor monitor, int currentSearchID)
        {
            InitializeComponent();

            _monitor = monitor;
            label_CurrentSearchValue.Text = currentSearchID.ToString();
        }

        private void button_Submit_Click(object sender, EventArgs e)
        {
            int newID = -1;

            if (int.TryParse(textBox_newID.Text, out newID))
            {
                _monitor.ChangeSearchID(newID);
                Close();
                return;
            }
            SendToast(ToastClicked.NoAction, $"Invalid Entry: {textBox_newID.Text}");
            return;
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SendToast(string argument, string message)
        {
            ToastContentBuilder toast = new ToastContentBuilder();
            toast.AddArgument(argument);
            toast.AddText(message);
            toast.Show();
        }

        private void textBox_KeyPressed(object sender, KeyPressEventArgs key)
        {
            if (key.KeyChar == '\r')
            {
                int newID = -1;

                if (int.TryParse(textBox_newID.Text, out newID))
                {
                    _monitor.ChangeSearchID(newID);
                    Close();
                    return;
                }
                SendToast(ToastClicked.NoAction, $"Invalid Entry: {textBox_newID.Text}");
                return;
            }
        }
    }
}
