using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WebScaperConsole
{
    public partial class MellingDepartInterface : Form
    {
        public string SelectedStation { get; set;}
        public DateTime SelectedDepartDate { get; set; }
        public string SelectedWeatherCity { get; set; }
        public string SelectedWeatherCountry { get; set; }

        MellingDeparts md;
        public MellingDepartInterface()
        {
            InitializeComponent();
            md = new MellingDeparts();

            // Giving default values incase is not updated 
            SelectedStation = "Melling";
            SelectedDepartDate = DateTime.Now.Date;
            SelectedWeatherCity = "Lower Hutt";
            SelectedWeatherCountry = "nz";
        }

        private void MellingDepartInterface_Load(object sender, EventArgs e)
        {

        }

        private void LstDepartStation_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Getting the abrviation for the train station when one is selected
            SelectedStation = md.StationAbrev(lstDepartStation.GetItemText(lstDepartStation.SelectedItem));
        }

        private void DtpDepartDate_ValueChanged(object sender, EventArgs e)
        {
            SelectedDepartDate = dtpDepartDate.Value.Date;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            // Getting and displaying the train times
            md.DepartTimes(SelectedStation, SelectedDepartDate);
        }

        private void TbWeatherCity_TextChanged(object sender, EventArgs e)
        {
            SelectedWeatherCity = tbWeatherCity.Text;
        }

        private void TbWeatherCountry_TextChanged(object sender, EventArgs e)
        {
            SelectedWeatherCountry = tbWeatherCountry.Text;
        }

        private void BtnGetWeather_Click(object sender, EventArgs e)
        {
            // Getting and displaying the weather info
            md.DaysWeather(SelectedWeatherCity, SelectedWeatherCountry);
        }
    }
}
