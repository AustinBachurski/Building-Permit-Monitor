namespace Building_Permit_Monitor.InfoWindow
{
    partial class InfoDisplay
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InfoDisplay));
            richTextBox = new RichTextBox();
            SuspendLayout();
            // 
            // richTextBox
            // 
            richTextBox.Dock = DockStyle.Fill;
            richTextBox.Location = new Point(0, 0);
            richTextBox.Margin = new Padding(15);
            richTextBox.Name = "richTextBox";
            richTextBox.ReadOnly = true;
            richTextBox.Size = new Size(644, 358);
            richTextBox.TabIndex = 0;
            richTextBox.Text = "No text was passed to control.";
            // 
            // InfoDisplay
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(644, 358);
            Controls.Add(richTextBox);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Location = new Point(3000, 1600);
            Name = "InfoDisplay";
            StartPosition = FormStartPosition.Manual;
            Text = "InfoDisplay";
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox richTextBox;
    }
}