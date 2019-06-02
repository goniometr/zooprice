using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoocool
{
    public static class Settings
    {
        public static string  ConStr{ get; set; }

        public static List<string> Errores { get; set; } = new List<string>();

        public static List<string> Dublicates { get; set; } = new List<string>();

        public static List<string> Urls { get; set; } = new List<string>();

    }
}
