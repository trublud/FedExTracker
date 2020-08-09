using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebScaperConsole
{
    class MellingDeparts
    {

        // Reading the env file used to store the hidden variables like api id
        private static readonly string[] ApiIds = System.IO.File.ReadAllLines(@"C:\Users\joe\Documents\github\WebScraper\WebScaperConsole\MellingDepartHiddenVars.env");
        // Getting the index for where the number starts  (NEED TO LOOK INTO HOW TO PROPERLY USE ENV FILES: using this as quick way in current state)
        private static readonly int OwApiIdIndex = ApiIds[0].IndexOf("=");
        // Public as might be needed outside this class
        public static readonly string OwApiID = ApiIds[0].Substring(OwApiIdIndex + 2);

        /// <summary>
        /// Given the full station name will return station abriviation apropriate for the url
        /// </summary>
        /// <param name="stationFullN"></param>
        /// <returns></returns>
        public string StationAbrev(string stationFullN)
        {

            string station;
            switch (stationFullN)
            {
                case "Melling":
                    station = "MELL";
                    break;

                case "Western Hutt":
                    station = "WEST";
                    break;

                case "Petone":
                    station = "PETO";
                    break;

                case "Ngauranga":
                    station = "NGAU";
                    break;

                default:
                    station = "MELL";
                    break;

            }
            return station;
        }
                

        /// <summary>
        /// Prints the departing train times for the given station and date
        /// </summary>
        /// <param name="departStation"></param>
        /// <param name="departD"></param>
        public void DepartTimes(string departStation, DateTime departD)
        {
            
            string url, departDate;
            bool today = false;
            HtmlWeb webTrainPage = new HtmlWeb();

            // Checking if they want todays info 
            if(departD == DateTime.Now.Date)
            {
                today = true;
            }

            // Converting to string and the correct format for the url
            departDate = departD.ToString("yyyy-MM-dd");
            url = $"https://www.metlink.org.nz/timetables/train/MEL?date={departDate}";

            // Loading the webpage source 
            HtmlAgilityPack.HtmlDocument pageTrainDoc = webTrainPage.Load(url);

            HtmlNode[] mellCol;
            // Using try catch as if there are no trains on that day the table doesnt exist and so nodes selected would be null and that caused errors           
            try
            {
                // Storing each cell from the melling row of the timetable for the melling trainline
                mellCol = pageTrainDoc.DocumentNode.SelectNodes($"//table[@id = 'timetableData']//tr[@data-sms = '{departStation}']//span[@class = 'timeValue']").ToArray();
            }
            catch (ArgumentNullException e)
            {
                // If there is no timetable then try get the no trains today message
                try
                {
                    // Storing the messages from the page where the timetable would normaly be 
                    mellCol = pageTrainDoc.DocumentNode.SelectNodes("//div[@id = 'timetable']").ToArray();
                }
                catch (ArgumentNullException a) { Console.WriteLine("Page Reading Faliure " + e + "and " + a); return; }
            }

            bool isTime;
            DateTime departTime;
            
            // Writing each time to the console 
            foreach (HtmlNode time in mellCol)
            {
                isTime = false;
                // Checking if the node is a time
                isTime = DateTime.TryParse(time.InnerText, out departTime);

                // Removing old times for today
                if (isTime && (departTime.TimeOfDay >= DateTime.Now.TimeOfDay) && today)
                {
                    MessageBox.Show(time.InnerText);

                }
                else if (isTime && !today)
                {
                    MessageBox.Show(time.InnerText);
                }
                else if (!isTime)
                {
                    MessageBox.Show(time.InnerText);
                }  
            }
        }


        /// <summary>
        ///  Gets and displays the current temperature and other weather info for the chosen city
        /// </summary>
        /// <param name="setCity"></param>
        /// <param name="setCountry"></param>
        public void DaysWeather(string setCity, string setCountry)
        {

            // Getting the html document here using openweather api the api id is passed in to url along with the city and country selected
            HtmlWeb webWeatherPage = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument pageWeatherDoc = webWeatherPage.Load($"https://api.openweathermap.org/data/2.5/weather?q={setCity},{setCountry}&mode=html&APPID={OwApiID}");


            // Making sure to give error message if cant get the weather info from open weather
            try
            {
                // Selecting the page element that has the temperature and the element for the location
                HtmlNode temperature = pageWeatherDoc.DocumentNode.SelectSingleNode("//div[@title = 'Current Temperature']");
                HtmlNode location = pageWeatherDoc.DocumentNode.SelectSingleNode("//div[@style = 'font-size: medium; font-weight: bold; margin-bottom: 0px;']");
                HtmlNode[] weatherInfo = pageWeatherDoc.DocumentNode.SelectNodes("//div[@style = 'display: block; clear: left; color: gray; font-size: x-small;']").ToArray();
    
                MessageBox.Show("The temperature in " + location.InnerText + " is: " + temperature.InnerText);
               
                // Printing the weather info nodes to all but the last one as that is a link to other website
                int wInfoNodes = weatherInfo.Length;
                for (int i = 0; i < wInfoNodes - 1; i++)
                {
                    MessageBox.Show(weatherInfo[i].InnerText);
                }
            }
            catch (ArgumentNullException e) { Console.WriteLine("Failure getting weather info\n"); }
        }
    }
}
