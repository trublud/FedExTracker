using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebScaperConsole;
using HtmlAgilityPack;

namespace WebScaperConsole
{
    class Program
    {
        static void Main(String[] args)
        {
            // Program starts by launching form
            Application.Run(new MellingDepartInterface());
        }
    }

}