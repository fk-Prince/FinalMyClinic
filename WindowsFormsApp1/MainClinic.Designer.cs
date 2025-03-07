namespace WindowsFormsApp1
{
    partial class Sidebar
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.mainpanel = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Clock = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.ViewPatientS = new System.Windows.Forms.Button();
            this.AddPatientS = new System.Windows.Forms.Button();
            this.DashboardS = new System.Windows.Forms.Button();
            this.ClockTimer = new System.Windows.Forms.Timer(this.components);
            this.Date = new System.Windows.Forms.Label();
            this.DateTimer = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.mainpanel);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1300, 720);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1278, -4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "X";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // mainpanel
            // 
            this.mainpanel.Location = new System.Drawing.Point(226, 25);
            this.mainpanel.Name = "mainpanel";
            this.mainpanel.Size = new System.Drawing.Size(1061, 667);
            this.mainpanel.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(136)))), ((int)(((byte)(152)))));
            this.panel2.Controls.Add(this.Date);
            this.panel2.Controls.Add(this.Clock);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(220, 720);
            this.panel2.TabIndex = 0;
            // 
            // Clock
            // 
            this.Clock.AutoSize = true;
            this.Clock.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Clock.ForeColor = System.Drawing.Color.White;
            this.Clock.Location = new System.Drawing.Point(40, 8);
            this.Clock.Name = "Clock";
            this.Clock.Size = new System.Drawing.Size(0, 45);
            this.Clock.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.button8);
            this.panel3.Controls.Add(this.button7);
            this.panel3.Controls.Add(this.button6);
            this.panel3.Controls.Add(this.button5);
            this.panel3.Controls.Add(this.button4);
            this.panel3.Controls.Add(this.ViewPatientS);
            this.panel3.Controls.Add(this.AddPatientS);
            this.panel3.Controls.Add(this.DashboardS);
            this.panel3.Location = new System.Drawing.Point(20, 119);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(180, 437);
            this.panel3.TabIndex = 0;
            // 
            // button8
            // 
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button8.Location = new System.Drawing.Point(19, 358);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(139, 46);
            this.button8.TabIndex = 10000;
            this.button8.TabStop = false;
            this.button8.Text = "button8";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Location = new System.Drawing.Point(19, 312);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(139, 46);
            this.button7.TabIndex = 10000;
            this.button7.TabStop = false;
            this.button7.Text = "button7";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Location = new System.Drawing.Point(19, 266);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(139, 46);
            this.button6.TabIndex = 10000;
            this.button6.TabStop = false;
            this.button6.Text = "button6";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Location = new System.Drawing.Point(19, 220);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(139, 46);
            this.button5.TabIndex = 10000;
            this.button5.TabStop = false;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Location = new System.Drawing.Point(19, 174);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(139, 46);
            this.button4.TabIndex = 10000;
            this.button4.TabStop = false;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // ViewPatientS
            // 
            this.ViewPatientS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ViewPatientS.Location = new System.Drawing.Point(19, 128);
            this.ViewPatientS.Name = "ViewPatientS";
            this.ViewPatientS.Size = new System.Drawing.Size(139, 46);
            this.ViewPatientS.TabIndex = 10000;
            this.ViewPatientS.TabStop = false;
            this.ViewPatientS.Text = "View Patient";
            this.ViewPatientS.UseVisualStyleBackColor = true;
            this.ViewPatientS.Click += new System.EventHandler(this.ViewPatientSide_Click);
            this.ViewPatientS.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mouseClicked);
            // 
            // AddPatientS
            // 
            this.AddPatientS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddPatientS.Location = new System.Drawing.Point(19, 82);
            this.AddPatientS.Name = "AddPatientS";
            this.AddPatientS.Size = new System.Drawing.Size(139, 46);
            this.AddPatientS.TabIndex = 10000;
            this.AddPatientS.TabStop = false;
            this.AddPatientS.Text = "Add Patient";
            this.AddPatientS.UseVisualStyleBackColor = true;
            this.AddPatientS.Click += new System.EventHandler(this.AddPatientS_Click);
            this.AddPatientS.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mouseClicked);
            // 
            // DashboardS
            // 
            this.DashboardS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(136)))), ((int)(((byte)(152)))));
            this.DashboardS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DashboardS.Location = new System.Drawing.Point(19, 36);
            this.DashboardS.Name = "DashboardS";
            this.DashboardS.Size = new System.Drawing.Size(139, 46);
            this.DashboardS.TabIndex = 10000;
            this.DashboardS.TabStop = false;
            this.DashboardS.Text = "Dashboard";
            this.DashboardS.UseVisualStyleBackColor = false;
            this.DashboardS.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mouseClicked);
            // 
            // ClockTimer
            // 
            this.ClockTimer.Enabled = true;
            this.ClockTimer.Interval = 1000;
            this.ClockTimer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Date
            // 
            this.Date.AutoSize = true;
            this.Date.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Date.ForeColor = System.Drawing.Color.White;
            this.Date.Location = new System.Drawing.Point(54, 46);
            this.Date.Name = "Date";
            this.Date.Size = new System.Drawing.Size(0, 25);
            this.Date.TabIndex = 2;
            // 
            // DateTimer
            // 
            this.DateTimer.Enabled = true;
            this.DateTimer.Interval = 72000000;
            this.DateTimer.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // Sidebar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1300, 720);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Sidebar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sidebar";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button ViewPatientS;
        private System.Windows.Forms.Button AddPatientS;
        private System.Windows.Forms.Button DashboardS;
        private System.Windows.Forms.Panel mainpanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Clock;
        private System.Windows.Forms.Timer ClockTimer;
        private System.Windows.Forms.Label Date;
        private System.Windows.Forms.Timer DateTimer;
    }
}