using Microsoft.Toolkit.Uwp.Notifications;
using Building_Permit_Monitor.Permit_Monitor;
using Building_Permit_Monitor.Cityworks;
using Building_Permit_Monitor.CityworksAPI;
using Building_Permit_Monitor.ExcelAccess;
using Building_Permit_Monitor.TrayApplication;
using Building_Permit_Monitor.SearchIdWindow;

namespace Building_Permit_Monitor
{
    public partial class SystemTray : Form
    {
        PermitMonitor _monitor;

        public SystemTray()
        {
            _monitor = new PermitMonitor(
                            new CityworksData(
                                new LiveCityworksAPI("https://cityworks.ci.kalispell.mt.us/Cityworks/")),
                                new Excel("K:\\Building Permit Tracking\\Building Permits.xlsx"));

            InitializeComponent();

            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;

            // Toast notification clicked handler.
            ToastNotificationManagerCompat.OnActivated += toastArgs =>
            {
                HandleToastNotificationAction(toastArgs.Argument);
            };

            BeginMonitoring().ConfigureAwait(false);
        }

        public async Task BeginMonitoring()
        {
            int thirtyMinutes = 1800000;

            while (true)
            {
                int count = await _monitor.CheckForWorkToDo();

                if (count > 0)
                {
                    SendToast(OnToastClick.UpdateSpreadsheet,
                        (count == 1 ? $"Found {count} new permit." : $"Found {count} new permits."));
                }

                await Task.Delay(thirtyMinutes);
            }
        }

        private void SendToast(string argument, string message)
        {
            ToastContentBuilder toast = new ToastContentBuilder();
            toast.AddArgument(argument);
            toast.AddText(message);
            toast.Show();
        }

        private void HandleToastNotificationAction(string toastArgument)
        {
            if (toastArgument == OnToastClick.UpdateSpreadsheet)
            {
                _monitor.DoWork().ConfigureAwait(false);
            }

            ToastNotificationManagerCompat.History.Clear();
        }

        private async void MenuItemClicked_CheckNow(object sender, EventArgs e)
        {
            int count = await _monitor.CheckForWorkToDo();

            if (count > 0)
            {
                SendToast(OnToastClick.UpdateSpreadsheet,
                    (count == 1 ? $"Found {count} new permit." : $"Found {count} new permits."));
            }
            else
            {
                SendToast(OnToastClick.NoAction, "Nothing to do...");
            }
        }

        private async void MenuItemClicked_DisplaySavedSearchID(object sender, EventArgs e)
        {
            await _monitor.DisplaySavedSearchesForUser();
        }

        private void MenuItemClicked_ChangeSearchID(object sender, EventArgs e)
        {
            Thread showSearch = new Thread(() =>
            { Application.Run(new SearchID(_monitor, _monitor.SearchID)); });
            showSearch.Start();
        }

        private void MenuItemClicked_Exit(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}