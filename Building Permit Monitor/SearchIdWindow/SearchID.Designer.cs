namespace Building_Permit_Monitor.SearchIdWindow
{
    partial class SearchID
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchID));
            textBox_newID = new TextBox();
            label_title_CurrentSearchID = new Label();
            button_Submit = new Button();
            button_Cancel = new Button();
            label_title_NewSearchID = new Label();
            label_CurrentSearchValue = new Label();
            SuspendLayout();
            // 
            // textBox_newID
            // 
            textBox_newID.Location = new Point(216, 59);
            textBox_newID.Name = "textBox_newID";
            textBox_newID.Size = new Size(150, 31);
            textBox_newID.TabIndex = 0;
            textBox_newID.KeyPress += textBox_KeyPressed;
            // 
            // label_title_CurrentSearchID
            // 
            label_title_CurrentSearchID.AutoSize = true;
            label_title_CurrentSearchID.Location = new Point(61, 21);
            label_title_CurrentSearchID.Name = "label_title_CurrentSearchID";
            label_title_CurrentSearchID.Size = new Size(149, 25);
            label_title_CurrentSearchID.TabIndex = 1;
            label_title_CurrentSearchID.Text = "Current SearchID:";
            // 
            // button_Submit
            // 
            button_Submit.Location = new Point(89, 107);
            button_Submit.Name = "button_Submit";
            button_Submit.Size = new Size(112, 34);
            button_Submit.TabIndex = 2;
            button_Submit.Text = "Submit";
            button_Submit.UseVisualStyleBackColor = true;
            button_Submit.Click += button_Submit_Click;
            // 
            // button_Cancel
            // 
            button_Cancel.Location = new Point(245, 107);
            button_Cancel.Name = "button_Cancel";
            button_Cancel.Size = new Size(112, 34);
            button_Cancel.TabIndex = 3;
            button_Cancel.Text = "Cancel";
            button_Cancel.UseVisualStyleBackColor = true;
            button_Cancel.Click += button_Cancel_Click;
            // 
            // label_title_NewSearchID
            // 
            label_title_NewSearchID.AutoSize = true;
            label_title_NewSearchID.Location = new Point(79, 59);
            label_title_NewSearchID.Name = "label_title_NewSearchID";
            label_title_NewSearchID.Size = new Size(131, 25);
            label_title_NewSearchID.TabIndex = 4;
            label_title_NewSearchID.Text = "New Search ID:";
            // 
            // label_CurrentSearchValue
            // 
            label_CurrentSearchValue.AutoSize = true;
            label_CurrentSearchValue.Location = new Point(216, 21);
            label_CurrentSearchValue.Name = "label_CurrentSearchValue";
            label_CurrentSearchValue.Size = new Size(0, 25);
            label_CurrentSearchValue.TabIndex = 5;
            // 
            // SearchID
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(435, 160);
            Controls.Add(label_CurrentSearchValue);
            Controls.Add(label_title_NewSearchID);
            Controls.Add(button_Cancel);
            Controls.Add(button_Submit);
            Controls.Add(label_title_CurrentSearchID);
            Controls.Add(textBox_newID);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Location = new Point(3200, 1700);
            Name = "SearchID";
            StartPosition = FormStartPosition.Manual;
            Text = "Change SearchID";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox_newID;
        private Label label_title_CurrentSearchID;
        private Button button_Submit;
        private Button button_Cancel;
        private Label label_title_NewSearchID;
        private Label label_CurrentSearchValue;
    }
}