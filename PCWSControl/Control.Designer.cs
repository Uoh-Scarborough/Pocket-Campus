namespace PCWSControl
{
    partial class Control
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LastUpdatelbl = new System.Windows.Forms.Label();
            this.NextUpdatelbl = new System.Windows.Forms.Label();
            this.Updatecmd = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Last Update:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Next Update:";
            // 
            // LastUpdatelbl
            // 
            this.LastUpdatelbl.AutoSize = true;
            this.LastUpdatelbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LastUpdatelbl.Location = new System.Drawing.Point(98, 9);
            this.LastUpdatelbl.Name = "LastUpdatelbl";
            this.LastUpdatelbl.Size = new System.Drawing.Size(65, 13);
            this.LastUpdatelbl.TabIndex = 2;
            this.LastUpdatelbl.Text = "Last Update";
            // 
            // NextUpdatelbl
            // 
            this.NextUpdatelbl.AutoSize = true;
            this.NextUpdatelbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NextUpdatelbl.Location = new System.Drawing.Point(98, 42);
            this.NextUpdatelbl.Name = "NextUpdatelbl";
            this.NextUpdatelbl.Size = new System.Drawing.Size(67, 13);
            this.NextUpdatelbl.TabIndex = 3;
            this.NextUpdatelbl.Text = "Next Update";
            // 
            // Updatecmd
            // 
            this.Updatecmd.Location = new System.Drawing.Point(158, 71);
            this.Updatecmd.Name = "Updatecmd";
            this.Updatecmd.Size = new System.Drawing.Size(75, 23);
            this.Updatecmd.TabIndex = 4;
            this.Updatecmd.Text = "Update Now";
            this.Updatecmd.UseVisualStyleBackColor = true;
            this.Updatecmd.Click += new System.EventHandler(this.Updatecmd_Click);
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 3600000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(245, 106);
            this.Controls.Add(this.Updatecmd);
            this.Controls.Add(this.NextUpdatelbl);
            this.Controls.Add(this.LastUpdatelbl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Control";
            this.Text = "PCWS Control";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label LastUpdatelbl;
        private System.Windows.Forms.Label NextUpdatelbl;
        private System.Windows.Forms.Button Updatecmd;
        private System.Windows.Forms.Timer timer;
    }
}

