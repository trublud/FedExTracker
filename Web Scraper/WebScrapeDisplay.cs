using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using System.Net.Http;
using System.IO; 
using AngleSharp.Dom;
using AngleSharp.Text;
using System.Windows.Forms;

using System.Collections.Specialized;
using System.Timers;
using System.Media;
using System.Drawing;
using FedExTracker.Properties;

namespace Web_Scraper
{
    public partial class WebScrapeDisplay : MetroFramework.Forms.MetroForm
    {
        public WebScrapeDisplay()
        {
            InitializeComponent();
        }
        int segundo = 0;
        DateTime dt = new DateTime();
        private string Title { get; set; }
        private string Url { get; set; }
        string startsiteUrl = "https://www.bing.com/packagetrackingv2?packNum=";
        string endsiteUrl = "&carrier=Fedex&FORM=PCKTR1";
        string siteUrl = "https://www.bing.com/packagetrackingv2?packNum=771195477820&carrier=Fedex&FORM=PCKTR1";
        public string[] QueryTerms { get; } = { "DATE" };

        private void Form1_Load(object sender, EventArgs e)
        {
            Settings mySettings = new Settings();

            foreach (string val in mySettings.tnumbers)
                comboBox1.Items.Add(val);
        //    System.Timers.Timer aTimer = new System.Timers.Timer(6000); //one hour in milliseconds

               System.Timers.Timer aTimer = new System.Timers.Timer(60 * 60 * 1000); //one hour in milliseconds
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Start();
            timer2.Start();
        }
        private async void OnTimedEvent(object source, ElapsedEventArgs e)
        {

            try
            {
                if (checkBox1.Checked)
                {

                    if (dataGridView1.InvokeRequired)
                    {
                        dataGridView1.Invoke(new MethodInvoker(delegate
                        {
                            while (dataGridView1.Rows.Count > 1)
                            {
                                dataGridView1.Rows.RemoveAt(0);
                            }
                            GetWebsite();
                        }));
                    }
                    else
                    {
                        // dataGridView1.DataContext = employeesView;
                    }






                }
            }
            catch (Exception err) { MessageBox.Show(err.Message.ToString()); }


        }

        private void StartCodeButton_Click(object sender, EventArgs e)
        {

            try
            {
                while (dataGridView1.Rows.Count > 1)
                {
                    dataGridView1.Rows.RemoveAt(0);
                }


                richTextBox.Text = "";
                GetWebsite2();
                // ScrapeWebsite();
            }
            catch (Exception err) { richTextBox.Text = err.Message.ToString(); }
        }

        internal async void ScrapeWebsite()
        {
            CancellationTokenSource cancellationToken = new CancellationTokenSource();

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage request = await httpClient.GetAsync(startsiteUrl + "771195477820" + endsiteUrl);

            cancellationToken.Token.ThrowIfCancellationRequested();

            Stream response = await request.Content.ReadAsStreamAsync();
            cancellationToken.Token.ThrowIfCancellationRequested();

            HtmlParser parser = new HtmlParser();
            IHtmlDocument document = parser.ParseDocument(response);

            GetScrapeResults(document);
        }
        internal async void Getit()
        {

            CancellationTokenSource cancellationToken = new CancellationTokenSource();

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage request = await httpClient.GetAsync(startsiteUrl + comboBox1.Text + endsiteUrl);

            cancellationToken.Token.ThrowIfCancellationRequested();

            Stream response = await request.Content.ReadAsStreamAsync();
            cancellationToken.Token.ThrowIfCancellationRequested();

            HtmlParser parser = new HtmlParser();
            IHtmlDocument document = parser.ParseDocument(response);
            IEnumerable<IElement> articleLink, statusLink, locLink, dateLink = null;
            articleLink = document.All.Where(x => (x.ClassName == "pr-tr-currentState" || x.ClassName == "b_focusTextSmall" || x.ClassName == "  pt_location_cell pt_Cell" || x.ClassName == "  rpt_se_rm pt_Cell" || x.ClassName == "b_focusTextSmall"));

            if (articleLink.Any())
            {
                PrintResults(articleLink);
            }
            // GetResults(document, comboBox1.Text);



        }
        internal async void GetWebsite2()
        {
            try
            {
                foreach (var term in comboBox1.Items)
                {
                    char[] delimiters = new char[] { ',' };
                    string[] parts = term.ToString().Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                    Console.WriteLine(":::SPLIT, CHAR ARRAY:::");
                    for (int i = 0; i < parts.Length; i++)
                    {
                        //      MessageBox.Show(parts[i]);
                        //      Console.WriteLine(parts[i]);
                    }

                    CancellationTokenSource cancellationToken = new CancellationTokenSource();

                    HttpClient httpClient = new HttpClient();
                    HttpResponseMessage request = await httpClient.GetAsync(startsiteUrl + parts[0] + endsiteUrl);

                    cancellationToken.Token.ThrowIfCancellationRequested();

                    Stream response = await request.Content.ReadAsStreamAsync();
                    cancellationToken.Token.ThrowIfCancellationRequested();

                    HtmlParser parser = new HtmlParser();
                    IHtmlDocument document = parser.ParseDocument(response);

                    GetResults2(document, (string)parts[0], (string)parts[1], (string)parts[2]);
                }
            }
            catch { }

        }
        internal async void GetWebsite()
        {
            foreach (var term in comboBox1.Items)
            {
                CancellationTokenSource cancellationToken = new CancellationTokenSource();

                HttpClient httpClient = new HttpClient();
                HttpResponseMessage request = await httpClient.GetAsync(startsiteUrl + term + endsiteUrl);

                cancellationToken.Token.ThrowIfCancellationRequested();

                Stream response = await request.Content.ReadAsStreamAsync();
                cancellationToken.Token.ThrowIfCancellationRequested();

                HtmlParser parser = new HtmlParser();
                IHtmlDocument document = parser.ParseDocument(response);

                GetResults(document, (string)term);
            }


        }

        private void GetResults2(IHtmlDocument document, string fedexid, string notes, string dated)
        {
            IEnumerable<IElement> articleLink, statusLink, locLink, dateLink = null;
            DataGridViewRow row = (DataGridViewRow)this.dataGridView1.Rows[0].Clone();
            try
            {

                foreach (var term in QueryTerms)
                {

                    //   articleLink = document.All.Where(x => ( x.ClassName == "  pt_location_cell pt_Cell" || x.ClassName == "pt_header pt_header_time") && (x.ParentElement.InnerHtml.Contains(term) || x.ParentElement.InnerHtml.Contains(term.ToLower())));
                    articleLink = document.All.Where(x => (x.ClassName == "pr-tr-currentState" || x.ClassName == "b_focusTextSmall" || x.ClassName == "  pt_location_cell pt_Cell" || x.ClassName == "  rpt_se_rm pt_Cell" || x.ClassName == "b_focusTextSmall"));
                    statusLink = document.All.Where(x => x.ClassName == "b_focusTextSmall" || x.ClassName == "pr-tr-currentState");
                    locLink = document.All.Where(x => x.ClassName == "  pt_location_cell pt_Cell");
                    dateLink = document.All.Where(x => x.ClassName == "  rpt_se_rm pt_Cell");
                    //        dateLink = document.All.Where(x => x.ClassName == "Tracking");
                    var characters = statusLink.ToArray();

                    dataGridView1.AutoGenerateColumns = false;

                    dataGridView1.Columns[1].DataPropertyName = "InnerHtml";
                    dataGridView1.Rows.Insert(0, fedexid, characters.ElementAt(1).InnerHtml, notes, dated);

                    DateTime futurDate = Convert.ToDateTime(dated);
                    DateTime TodayDate = DateTime.Now;
                    var numberOfDays = (TodayDate - futurDate ).TotalDays;



                  
                    if (numberOfDays >= 8)
                    {
                        dataGridView1.Rows[0].DefaultCellStyle.BackColor = Color.LightYellow;
                    }
                    else
                    {
                        if (numberOfDays >= 10)
                        {
                            dataGridView1.Rows[0].DefaultCellStyle.BackColor = Color.Yellow;
                        }
                        if (numberOfDays >= 14)
                        {
                            dataGridView1.Rows[0].DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                        }
                    }
                   
                    //        if (articleLink.Any())
                    {
                        //   PrintResults(articleLink);
                    }
                }

            }

            catch (Exception err) { if (err.Message.ToString().Contains("Index was out of range. Must be non-negative and less than the size of the collection")) { richTextBox.Text = "Not Found"; dataGridView1.Rows.Insert(0, fedexid, "Not Found", notes, dated); } }

        }

        private void GetResults(IHtmlDocument document, string fedexid)
        {
            IEnumerable<IElement> articleLink, statusLink, locLink, dateLink = null;
            DataGridViewRow row = (DataGridViewRow)this.dataGridView1.Rows[0].Clone();
            try
            {

                foreach (var term in QueryTerms)
                {

                    //   articleLink = document.All.Where(x => ( x.ClassName == "  pt_location_cell pt_Cell" || x.ClassName == "pt_header pt_header_time") && (x.ParentElement.InnerHtml.Contains(term) || x.ParentElement.InnerHtml.Contains(term.ToLower())));
                    articleLink = document.All.Where(x => (x.ClassName == "pr-tr-currentState" || x.ClassName == "b_focusTextSmall" || x.ClassName == "  pt_location_cell pt_Cell" || x.ClassName == "  rpt_se_rm pt_Cell" || x.ClassName == "b_focusTextSmall"));
                    statusLink = document.All.Where(x => x.ClassName == "b_focusTextSmall" || x.ClassName == "pr-tr-currentState");
                    locLink = document.All.Where(x => x.ClassName == "  pt_location_cell pt_Cell");
                    dateLink = document.All.Where(x => x.ClassName == "  rpt_se_rm pt_Cell");
                    //        dateLink = document.All.Where(x => x.ClassName == "Tracking");
                    var characters = statusLink.ToArray();

                    dataGridView1.AutoGenerateColumns = false;

                    dataGridView1.Columns[1].DataPropertyName = "InnerHtml";
                    dataGridView1.Rows.Insert(0, fedexid, characters.ElementAt(1).InnerHtml);

                    //        if (articleLink.Any())
                    {
                        //   PrintResults(articleLink);
                    }
                }

            }

            catch (Exception err) { if (err.Message.ToString().Contains("Index was out of range. Must be non-negative and less than the size of the collection")) { richTextBox.Text = "Not Found"; dataGridView1.Rows.Insert(0, fedexid, "Not Found"); } }

        }
        private void GetScrapeResults(IHtmlDocument document)
        {
            IEnumerable<IElement> articleLink, statusLink, locLink, dateLink = null;

            foreach (var term in QueryTerms)
            {
                //   articleLink = document.All.Where(x => ( x.ClassName == "  pt_location_cell pt_Cell" || x.ClassName == "pt_header pt_header_time") && (x.ParentElement.InnerHtml.Contains(term) || x.ParentElement.InnerHtml.Contains(term.ToLower())));
                articleLink = document.All.Where(x => (x.ClassName == "pr-tr-currentState" || x.ClassName == "b_focusTextSmall" || x.ClassName == "  pt_location_cell pt_Cell" || x.ClassName == "  rpt_se_rm pt_Cell"));
                statusLink = document.All.Where(x => x.ClassName == "pr-tr-currentState");
                locLink = document.All.Where(x => x.ClassName == "  pt_location_cell pt_Cell");
                dateLink = document.All.Where(x => x.ClassName == "  rpt_se_rm pt_Cell");
                if (articleLink.Any())
                {
                    PrintResults(articleLink);
                }
            }
        }

        public void PrintResults(IEnumerable<IElement> articleLink)
        {
            foreach (var element in articleLink)
            {
                CleanResults(element);
            }
        }

        private void CleanResults(IElement result)
        {
            string htmlResult = result.InnerHtml;
            ////     htmlResult = htmlResult.ReplaceFirst("\">", "*");

            //     htmlResult = htmlResult.ReplaceFirst("</a></div>\n<div class=\"article-title-top\">", "-");
            //    htmlResult = htmlResult.ReplaceFirst("</div>\n<hr></span>  ", "");

            ResultValidation(htmlResult);

        }

        private void ResultValidation(string htmlResult)
        {
            //   if ((htmlResult.Contains("<") || htmlResult.Contains(">")) == false)
            //   {
            SplitResults(htmlResult);
            //   }
        }

        private void SplitResults(string htmlResult)
        {

            richTextBox.AppendText($"{htmlResult}" + Environment.NewLine);
            //    string[] splitResults = htmlResult.Split(' ');
            //        Url = splitResults[0];
            //       Title = splitResults[1];
            //       AppendResults();
        }

        private void AppendResults()
        {
            richTextBox.AppendText($"{Title} - {Url}{Environment.NewLine}");
        }
        private void SaveComboBoxValues()
        {
            Settings mySettings = new Settings();
            StringCollection myValues = new StringCollection();

            foreach (var cVal in comboBox1.Items)
                myValues.Add(cVal.ToString());

            mySettings.tnumbers = myValues;
            mySettings.Save();

        }





        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (comboBox1.Items.Contains(comboBox1.Text))
                {
                }
                else
                {
                    string toadd = "";
                    if (radioButton1.Checked)
                    {
                        toadd = comboBox1.Text + " From: ";

                    }
                    else
                    {
                        toadd = comboBox1.Text + " To: ";
                        radioButton1.Checked = true;
                        radioButton2.Checked = false;
                    }
                    comboBox1.Items.Add(toadd + textBox1.Text);

                }

                SaveComboBoxValues();
            }
            if (e.KeyCode == Keys.Delete)
            {
                if (comboBox1.Items.Contains(comboBox1.Text))
                {
                    comboBox1.Items.Remove(comboBox1.Text);
                }
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            Getit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult timerb = MessageBox.Show(" Are you sure? ", "This will clear all SAVED FedEx numbers too", MessageBoxButtons.YesNo);

            if (timerb == System.Windows.Forms.DialogResult.Yes)
            {
                comboBox1.Items.Clear();
                SaveComboBoxValues();
            }

            }

        private void button1_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Remove(comboBox1.Text);
            SaveComboBoxValues();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked) radioButton2.Checked = false;

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked) radioButton1.Checked = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
            radioButton1.Checked = false;
            radioButton2.Checked = true;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (comboBox1.Items.Contains(comboBox1.Text))
                    {
                    }
                    else
                    {
                        string toadd = "";
                        if (radioButton1.Checked)
                        {
                            toadd = comboBox1.Text + ", From: ";
                        
                            radioButton2.Checked = true;

                        }
                        else
                        {
                            toadd = comboBox1.Text + ", To: ";
                            radioButton1.Checked = true;
                            radioButton2.Checked = false;
                        }
                        comboBox1.Items.Add(toadd+ textBox1.Text + ", " +DateTime.Today.ToShortDateString());

                    }

                    SaveComboBoxValues();
                }
            }
            catch { }
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            comboBox1.SelectAll();
        }

        private void buttontimer_Click(object sender, EventArgs e)
        {
            SystemSounds.Beep.Play();
            DialogResult timerb = MessageBox.Show(" If No then 59 minutes will start ", "14 minutes?", MessageBoxButtons.YesNo);
            if (timerb == System.Windows.Forms.DialogResult.No) timerbreak(59);
            if (timerb == System.Windows.Forms.DialogResult.Yes) timerbreak(14);
        }
        private void timerbreak(int howlong)
        {
           
            System.Timers.Timer aTimer = new System.Timers.Timer(howlong * 60 * 1000);
            //   timer1.Tick += (s, ev) => { label1.Text = String.Format("{0:00}", ((DateTime.Now+howlong * 60 * 1000) - DateTime.Now ).Seconds); };
            //aTimer = DateTime.Now;
                timer1.Interval = 1000;       // every 1/10 of a second
              
            timer1.Enabled = true;
 timer1.Start();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedBreak);
            aTimer.Start();
        }
        private async void OnTimedBreak(object source, ElapsedEventArgs e)
        {

            try
            {

                SystemSounds.Asterisk.Play();
                Thread.Sleep(1000);
                SystemSounds.Exclamation.Play();
                Thread.Sleep(1000);
                SystemSounds.Beep.Play();
                Thread.Sleep(1000);
                SystemSounds.Hand.Play();
                Thread.Sleep(1000);
                SystemSounds.Question.Play();
                Thread.Sleep(1000);
                MessageBox.Show("Time is up!");
                timer1.Enabled = false;
                timer1.Start();
            }
            catch (Exception err) { MessageBox.Show(err.Message.ToString()); }


        }

        private void WebScrapeDisplay_FormClosing(object sender, FormClosingEventArgs e)
        {
            SystemSounds.Beep.Play();
            SaveComboBoxValues();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
       

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            segundo++;
            label1.Text = dt.AddSeconds(segundo).ToString("HH:mm:ss");
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {

            DateTime timeUtc = DateTime.UtcNow;
            try
            {
                TimeZoneInfo pstZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
                TimeZoneInfo estZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                TimeZoneInfo aztZone = TimeZoneInfo.FindSystemTimeZoneById("US Mountain Standard Time");
               TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
                DateTime pstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, pstZone);
                DateTime aztTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, aztZone);
                DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);
                DateTime estTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, estZone);
                pstlbl.Text = "PST: " + pstTime.ToShortTimeString();
                aztlbl.Text = "AZT: " + aztTime.ToShortTimeString();
                cstlbl.Text = "CST: " + cstTime.ToShortTimeString();
                estlbl.Text = "EST: " + estTime.ToShortTimeString();
                //  ,
                // cstZone.IsDaylightSavingTime(cstTime) ?
                //       cstZone.DaylightName : cstZone.StandardName);
            }
            catch (TimeZoneNotFoundException)
            {
                MessageBox.Show("The registry does not define the Central Standard Time zone.");
            }
            catch (InvalidTimeZoneException)
            {
                MessageBox.Show("Registry data on the Central Standard Time zone has been corrupted.");
            }





            TimeZone zone = TimeZone.CurrentTimeZone;
            // Demonstrate ToLocalTime and ToUniversalTime.
            DateTime local = zone.ToLocalTime(DateTime.Now);
            DateTime universal = zone.ToUniversalTime(DateTime.Now);
            // pstlbl.Text = "PST: " + zone.e(DateTime.Now));
            Console.WriteLine(universal);
        }
    }
}