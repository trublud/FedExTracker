namespace WebScaperConsole
{
    partial class MellingDepartInterface
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
            this.gbTrainInfo = new System.Windows.Forms.GroupBox();
            this.btnGetDepartTimes = new System.Windows.Forms.Button();
            this.dtpDepartDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.lstDepartStation = new System.Windows.Forms.ListBox();
            this.gbWeather = new System.Windows.Forms.GroupBox();
            this.btnGetWeather = new System.Windows.Forms.Button();
            this.tbWeatherCountry = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbWeatherCity = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.gbTrainInfo.SuspendLayout();
            this.gbWeather.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ENTER DEPARTING STATION:";
            // 
            // gbTrainInfo
            // 
            this.gbTrainInfo.Controls.Add(this.btnGetDepartTimes);
            this.gbTrainInfo.Controls.Add(this.dtpDepartDate);
            this.gbTrainInfo.Controls.Add(this.label2);
            this.gbTrainInfo.Controls.Add(this.lstDepartStation);
            this.gbTrainInfo.Location = new System.Drawing.Point(22, 25);
            this.gbTrainInfo.Name = "gbTrainInfo";
            this.gbTrainInfo.Size = new System.Drawing.Size(394, 212);
            this.gbTrainInfo.TabIndex = 2;
            this.gbTrainInfo.TabStop = false;
            this.gbTrainInfo.Text = "Train Info";
            // 
            // btnGetDepartTimes
            // 
            this.btnGetDepartTimes.Location = new System.Drawing.Point(228, 183);
            this.btnGetDepartTimes.Name = "btnGetDepartTimes";
            this.btnGetDepartTimes.Size = new System.Drawing.Size(144, 23);
            this.btnGetDepartTimes.TabIndex = 3;
            this.btnGetDepartTimes.Text = "GET DEPARTURE TIMES";
            this.btnGetDepartTimes.UseVisualStyleBackColor = true;
            this.btnGetDepartTimes.Click += new System.EventHandler(this.Button1_Click);
            // 
            // dtpDepartDate
            // 
            this.dtpDepartDate.Location = new System.Drawing.Point(228, 143);
            this.dtpDepartDate.MaxDate = new System.DateTime(2250, 12, 27, 0, 0, 0, 0);
            this.dtpDepartDate.MinDate = new System.DateTime(2019, 7, 4, 0, 0, 0, 0);
            this.dtpDepartDate.Name = "dtpDepartDate";
            this.dtpDepartDate.Size = new System.Drawing.Size(144, 20);
            this.dtpDepartDate.TabIndex = 2;
            this.dtpDepartDate.Value = new System.DateTime(2019, 7, 4, 0, 0, 0, 0);
            this.dtpDepartDate.ValueChanged += new System.EventHandler(this.DtpDepartDate_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 143);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "ENTER DEPARTING DATE:";
            // 
            // lstDepartStation
            // 
            this.lstDepartStation.FormattingEnabled = true;
            this.lstDepartStation.Items.AddRange(new object[] {
            "Melling",
            "Western Hutt",
            "Petone",
            "Ngauranga"});
            this.lstDepartStation.Location = new System.Drawing.Point(228, 23);
            this.lstDepartStation.Name = "lstDepartStation";
            this.lstDepartStation.Size = new System.Drawing.Size(144, 95);
            this.lstDepartStation.TabIndex = 0;
            this.lstDepartStation.SelectedIndexChanged += new System.EventHandler(this.LstDepartStation_SelectedIndexChanged);
            // 
            // gbWeather
            // 
            this.gbWeather.Controls.Add(this.btnGetWeather);
            this.gbWeather.Controls.Add(this.tbWeatherCountry);
            this.gbWeather.Controls.Add(this.label4);
            this.gbWeather.Controls.Add(this.tbWeatherCity);
            this.gbWeather.Controls.Add(this.label3);
            this.gbWeather.Location = new System.Drawing.Point(22, 261);
            this.gbWeather.Name = "gbWeather";
            this.gbWeather.Size = new System.Drawing.Size(394, 223);
            this.gbWeather.TabIndex = 3;
            this.gbWeather.TabStop = false;
            this.gbWeather.Text = "Weather Info";
            // 
            // btnGetWeather
            // 
            this.btnGetWeather.Location = new System.Drawing.Point(228, 193);
            this.btnGetWeather.Name = "btnGetWeather";
            this.btnGetWeather.Size = new System.Drawing.Size(144, 23);
            this.btnGetWeather.TabIndex = 4;
            this.btnGetWeather.Text = "GET WEATHER INFO";
            this.btnGetWeather.UseVisualStyleBackColor = true;
            this.btnGetWeather.Click += new System.EventHandler(this.BtnGetWeather_Click);
            // 
            // tbWeatherCountry
            // 
            this.tbWeatherCountry.Location = new System.Drawing.Point(228, 152);
            this.tbWeatherCountry.Name = "tbWeatherCountry";
            this.tbWeatherCountry.Size = new System.Drawing.Size(144, 20);
            this.tbWeatherCountry.TabIndex = 3;
            this.tbWeatherCountry.TextChanged += new System.EventHandler(this.TbWeatherCountry_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 160);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(178, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "ENTER COUNTRY CODE (E.G NZ)";
            // 
            // tbWeatherCity
            // 
            this.tbWeatherCity.Location = new System.Drawing.Point(228, 38);
            this.tbWeatherCity.Name = "tbWeatherCity";
            this.tbWeatherCity.Size = new System.Drawing.Size(144, 20);
            this.tbWeatherCity.TabIndex = 1;
            this.tbWeatherCity.TextChanged += new System.EventHandler(this.TbWeatherCity_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "ENTER CITY NAME:";
            // 
            // MellingDepartInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 489);
            this.Controls.Add(this.gbWeather);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gbTrainInfo);
            this.Name = "MellingDepartInterface";
            this.Text = "MellingDepartInterface";
            this.Load += new System.EventHandler(this.MellingDepartInterface_Load);
            this.gbTrainInfo.ResumeLayout(false);
            this.gbTrainInfo.PerformLayout();
            this.gbWeather.ResumeLayout(false);
            this.gbWeather.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbTrainInfo;
        private System.Windows.Forms.ListBox lstDepartStation;
        private System.Windows.Forms.DateTimePicker dtpDepartDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gbWeather;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbWeatherCountry;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbWeatherCity;
        private System.Windows.Forms.Button btnGetDepartTimes;
        private System.Windows.Forms.Button btnGetWeather;
    }
}