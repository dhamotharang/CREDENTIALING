namespace DBMigrationApplication
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.lblProfilesCount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCurrentProfile = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblDone = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(24, 31);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(225, 32);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start Migration";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(24, 134);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(1054, 564);
            this.listBox1.TabIndex = 1;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(255, 31);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(823, 31);
            this.progressBar1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(252, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Profiles Count :";
            // 
            // lblProfilesCount
            // 
            this.lblProfilesCount.AutoSize = true;
            this.lblProfilesCount.Location = new System.Drawing.Point(362, 85);
            this.lblProfilesCount.Name = "lblProfilesCount";
            this.lblProfilesCount.Size = new System.Drawing.Size(0, 17);
            this.lblProfilesCount.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(642, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Migrating Profile:";
            // 
            // lblCurrentProfile
            // 
            this.lblCurrentProfile.AutoSize = true;
            this.lblCurrentProfile.Location = new System.Drawing.Point(771, 85);
            this.lblCurrentProfile.Name = "lblCurrentProfile";
            this.lblCurrentProfile.Size = new System.Drawing.Size(0, 17);
            this.lblCurrentProfile.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(440, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "Done:";
            // 
            // lblDone
            // 
            this.lblDone.AutoSize = true;
            this.lblDone.Location = new System.Drawing.Point(499, 85);
            this.lblDone.Name = "lblDone";
            this.lblDone.Size = new System.Drawing.Size(42, 17);
            this.lblDone.TabIndex = 8;
            this.lblDone.Text = "Done";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1090, 735);
            this.Controls.Add(this.lblDone);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblCurrentProfile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblProfilesCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "DB Migration";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblProfilesCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCurrentProfile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblDone;
    }
}

