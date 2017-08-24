namespace ErrorHandling
{
    partial class ErrorPrompt
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
            this.exceptionNameTb = new System.Windows.Forms.TextBox();
            this.exceptionContentRtb = new System.Windows.Forms.RichTextBox();
            this.sendBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // exceptionNameTb
            // 
            this.exceptionNameTb.Location = new System.Drawing.Point(12, 12);
            this.exceptionNameTb.Name = "exceptionNameTb";
            this.exceptionNameTb.Size = new System.Drawing.Size(646, 20);
            this.exceptionNameTb.TabIndex = 0;
            // 
            // exceptionContentRtb
            // 
            this.exceptionContentRtb.Location = new System.Drawing.Point(12, 39);
            this.exceptionContentRtb.Name = "exceptionContentRtb";
            this.exceptionContentRtb.Size = new System.Drawing.Size(646, 172);
            this.exceptionContentRtb.TabIndex = 1;
            this.exceptionContentRtb.Text = "";
            // 
            // sendBtn
            // 
            this.sendBtn.Location = new System.Drawing.Point(298, 218);
            this.sendBtn.Name = "sendBtn";
            this.sendBtn.Size = new System.Drawing.Size(75, 23);
            this.sendBtn.TabIndex = 2;
            this.sendBtn.Text = "Send";
            this.sendBtn.UseVisualStyleBackColor = true;
            // 
            // ErrorPrompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 261);
            this.Controls.Add(this.sendBtn);
            this.Controls.Add(this.exceptionContentRtb);
            this.Controls.Add(this.exceptionNameTb);
            this.Name = "ErrorPrompt";
            this.Text = "ErrorPrompt";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox exceptionNameTb;
        private System.Windows.Forms.RichTextBox exceptionContentRtb;
        private System.Windows.Forms.Button sendBtn;
    }
}