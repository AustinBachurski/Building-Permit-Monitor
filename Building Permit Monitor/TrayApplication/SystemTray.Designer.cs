namespace Building_Permit_Monitor
{
    partial class SystemTray
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SystemTray));
            trayIcon = new NotifyIcon(components);
            rightClickMenu = new ContextMenuStrip(components);
            Menu_CheckNow = new ToolStripMenuItem();
            Menu_DisplaySavedSearchIDs = new ToolStripMenuItem();
            Menu_ChangeSavedSearchID = new ToolStripMenuItem();
            Menu_Exit = new ToolStripMenuItem();
            rightClickMenu.SuspendLayout();
            SuspendLayout();
            // 
            // trayIcon
            // 
            trayIcon.ContextMenuStrip = rightClickMenu;
            trayIcon.Icon = (Icon)resources.GetObject("trayIcon.Icon");
            trayIcon.Text = "Building Permit Monitor";
            trayIcon.Visible = true;
            // 
            // rightClickMenu
            // 
            rightClickMenu.ImageScalingSize = new Size(24, 24);
            rightClickMenu.Items.AddRange(new ToolStripItem[] { Menu_CheckNow, Menu_ChangeSavedSearchID, Menu_DisplaySavedSearchIDs, Menu_Exit });
            rightClickMenu.Name = "rightClickMenu";
            rightClickMenu.Size = new Size(284, 165);
            // 
            // Menu_CheckNow
            // 
            Menu_CheckNow.Name = "Menu_CheckNow";
            Menu_CheckNow.Size = new Size(283, 32);
            Menu_CheckNow.Text = "Check Now";
            Menu_CheckNow.Click += MenuItemClicked_CheckNow;
            // 
            // Menu_DisplaySavedSearchIDs
            // 
            Menu_DisplaySavedSearchIDs.Name = "Menu_DisplaySavedSearchIDs";
            Menu_DisplaySavedSearchIDs.Size = new Size(283, 32);
            Menu_DisplaySavedSearchIDs.Text = "Display Saved Search IDs";
            Menu_DisplaySavedSearchIDs.Click += MenuItemClicked_DisplaySavedSearchID;
            // 
            // Menu_ChangeSavedSearchID
            // 
            Menu_ChangeSavedSearchID.Name = "Menu_ChangeSavedSearchID";
            Menu_ChangeSavedSearchID.Size = new Size(283, 32);
            Menu_ChangeSavedSearchID.Text = "Change Search ID";
            Menu_ChangeSavedSearchID.Click += MenuItemClicked_ChangeSearchID;
            // 
            // Menu_Exit
            // 
            Menu_Exit.Name = "Menu_Exit";
            Menu_Exit.Size = new Size(283, 32);
            Menu_Exit.Text = "Exit";
            Menu_Exit.Click += MenuItemClicked_Exit;
            // 
            // SystemTray
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Enabled = false;
            Name = "SystemTray";
            ShowInTaskbar = false;
            Text = "Form1";
            rightClickMenu.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private NotifyIcon trayIcon;
        private ContextMenuStrip rightClickMenu;
        private ToolStripMenuItem Menu_CheckNow;
        private ToolStripMenuItem Menu_DisplaySavedSearchIDs;
        private ToolStripMenuItem Menu_ChangeSavedSearchID;
        private ToolStripMenuItem Menu_Exit;
    }
}