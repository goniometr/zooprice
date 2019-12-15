using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using zoocool.OldPrice;
using zoocool.Xml2CSharp;

namespace zoocool
{
    public partial class fmOldPrice : Form
    {
        public fmOldPrice()
        {
            InitializeComponent();
        }

        private void fmOldPrice_Load(object sender, EventArgs e)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(Yml_catalog));

            Yml_catalog ob;

            using (FileStream fs = new FileStream("price.xml", FileMode.Open))
            {
                ob = (Yml_catalog)formatter.Deserialize(fs);
            }

            var listCategory = ob.Shop.Categories.Category;
            var ds = ob.Shop.Offers.Offer.Select(x => new BaseOldPrice
            {
                CategoryName = listCategory.Where(y => y.Id == x.CategoryId).Select(y => y.Text).FirstOrDefault(),
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Price_Old = x.Price_old
            }).Where(x => x.Price_Old != null).OrderBy(x=>x.CategoryName).OrderBy(x=>x.Name).ToList();
            grData.DataSource = ds;
        }

       
    }
}
