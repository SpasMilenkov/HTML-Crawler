namespace HTML_Crawler.UI
{
    partial class SaveDocumentForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxInput = new System.Windows.Forms.TextBox();
            this.SaveDocumentLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter File Path:";
            // 
            // textBoxInput
            // 
            this.textBoxInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxInput.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxInput.ForeColor = System.Drawing.Color.White;
            this.textBoxInput.Location = new System.Drawing.Point(12, 56);
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.Size = new System.Drawing.Size(549, 28);
            this.textBoxInput.TabIndex = 1;
            // 
            // SaveDocumentLabel
            // 
            this.SaveDocumentLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SaveDocumentLabel.ForeColor = System.Drawing.Color.White;
            this.SaveDocumentLabel.Location = new System.Drawing.Point(461, 145);
            this.SaveDocumentLabel.Name = "SaveDocumentLabel";
            this.SaveDocumentLabel.Size = new System.Drawing.Size(100, 37);
            this.SaveDocumentLabel.TabIndex = 3;
            this.SaveDocumentLabel.Text = "Save";
            this.SaveDocumentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.SaveDocumentLabel.Click += new System.EventHandler(this.SaveDocumentLabel_Click);
            // 
            // SaveDocumentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 221);
            this.Controls.Add(this.SaveDocumentLabel);
            this.Controls.Add(this.textBoxInput);
            this.Controls.Add(this.label1);
            this.Name = "SaveDocumentForm";
            this.Text = "SaveDocumentForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private TextBox textBoxInput;
        private Label SaveDocumentLabel;
    }
}