using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoocool
{
    public class Errores
    {
        public List<string> duplicates { get; set; }
        public List<string> error { get; set; }

        public Errores() 
        {
            duplicates = Settings.Dublicates.Distinct().ToList();

            error = Settings.Errores.Distinct().ToList();
        }

    }
}
