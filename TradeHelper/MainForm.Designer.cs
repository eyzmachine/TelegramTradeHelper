namespace TradeHelper
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.CheckConnectionBtn = new System.Windows.Forms.Button();
            this.StartWorkingBtn = new System.Windows.Forms.Button();
            this.ConnectionIndicator = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.SuspendLayout();
            // 
            // CheckConnectionBtn
            // 
            this.CheckConnectionBtn.Location = new System.Drawing.Point(12, 61);
            this.CheckConnectionBtn.Name = "CheckConnectionBtn";
            this.CheckConnectionBtn.Size = new System.Drawing.Size(339, 25);
            this.CheckConnectionBtn.TabIndex = 0;
            this.CheckConnectionBtn.Text = global::TradeHelper.Resources.FormStrings.CheckConnection;
            this.CheckConnectionBtn.UseVisualStyleBackColor = true;
            this.CheckConnectionBtn.Click += new System.EventHandler(this.CheckConnectionBtn_Click);
            // 
            // StartWorkingBtn
            // 
            this.StartWorkingBtn.Location = new System.Drawing.Point(12, 91);
            this.StartWorkingBtn.Name = "StartWorkingBtn";
            this.StartWorkingBtn.Size = new System.Drawing.Size(339, 25);
            this.StartWorkingBtn.TabIndex = 1;
            this.StartWorkingBtn.Text = global::TradeHelper.Resources.FormStrings.StartWorking;
            this.StartWorkingBtn.UseVisualStyleBackColor = true;
            this.StartWorkingBtn.Click += new System.EventHandler(this.StartWorkingBtn_Click);
            // 
            // ConnectionIndicator
            // 
            this.ConnectionIndicator.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConnectionIndicator.Location = new System.Drawing.Point(12, 9);
            this.ConnectionIndicator.Name = "ConnectionIndicator";
            this.ConnectionIndicator.Size = new System.Drawing.Size(339, 49);
            this.ConnectionIndicator.TabIndex = 2;
            this.ConnectionIndicator.Text = global::TradeHelper.Resources.FormStrings.PressAnyButton;
            this.ConnectionIndicator.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Telegram Trade Helper";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 127);
            this.Controls.Add(this.ConnectionIndicator);
            this.Controls.Add(this.StartWorkingBtn);
            this.Controls.Add(this.CheckConnectionBtn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Telegram Trade Helper";
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_OnClose);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button CheckConnectionBtn;
        private System.Windows.Forms.Button StartWorkingBtn;
        private System.Windows.Forms.Label ConnectionIndicator;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}

