using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoocool
{
    public static class Settings
    {

        public static PriceSettings PriceSettings;

        public static List<string> Errores  = new List<string>();
        
        public static List<string> Dublicates = new List<string>();
        
        public static List<string> Urls = new List<string>();

    }

    public class PriceSettings
    {
        public string ConStr;

        public string BaseURL;

        public string URL;

        public string Name;

        public string CopanyName;
    }
}
