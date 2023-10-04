namespace Building_Permit_Monitor.InfoWindow
{
    public partial class InfoDisplay : Form
    {
        public InfoDisplay(string title, string content)
        {
            InitializeComponent();

            Text = title;
            richTextBox.Text = content;
        }
    }
}
