namespace HTML_Crawler.UI
{
    partial class LoadDocument
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoadDocument));
            this.TextBoxInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LoadButton = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TextBoxInput
            // 
            this.TextBoxInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.TextBoxInput, "TextBoxInput");
            this.TextBoxInput.ForeColor = System.Drawing.Color.White;
            this.TextBoxInput.Name = "TextBoxInput";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Name = "label1";
            // 
            // LoadButton
            // 
            resources.ApplyResources(this.LoadButton, "LoadButton");
            this.LoadButton.BackColor = System.Drawing.Color.Transparent;
            this.LoadButton.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // LoadDocument
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Controls.Add(this.LoadButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TextBoxInput);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "LoadDocument";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox TextBoxInput;
        private Label label1;
        private Label LoadButton;
    }
}