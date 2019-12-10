using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoocool
{
    class Vendor
    {
        public int Product_id { get; set; }
        public string Name { get; set; }

        public List<Vendor> GetListVendor()
        {
            var listVendorReplace = new VendorReplace().GetVendorReplace();

            var list = new List<Vendor>();
            MySqlConnection conn = new MySqlConnection(Settings.ConStr);
            conn.Open();
            string sql = "select t1.product_id, t3.value from shop_product_features t1 "
+ " inner join shop_feature t2 on t1.feature_id = t2.id "
+ " inner join shop_feature_values_varchar t3 on t1.feature_value_id = t3.id"
+ " where t2.id = 4 and t2.type = 'varchar'";
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var vendor = new Vendor()
                {
                    Product_id = int.Parse(reader[0].ToString()),
                    Name = reader[1].ToString()
                };

                if (listVendorReplace.Select(x => x.OldName).Contains(vendor.Name))
                    vendor.Name = listVendorReplace.Where(x => x.OldName == vendor.Name).FirstOrDefault().NewName;
                list.Add(vendor);
            }
            reader.Close();
            conn.Close();
            return list;
        }



    }

    class VendorReplace
    {
       public string OldName { get; set; }
       public string NewName { get; set; }

        public List<VendorReplace> GetVendorReplace()
        {
            var list = new List<VendorReplace>();
            if (File.Exists("tm.csv"))
            {
                using (var csvParser = new StreamReader("tm.csv"))
                {
                    while (!csvParser.EndOfStream)
                    {
                        var lineIn = csvParser.ReadLine().Split(';');
                        list.Add(new VendorReplace() { OldName = lineIn[0], NewName = lineIn[1] });
                    }
                }
            }
            else  Settings.Errores.Add("Отсутствует файл соответствия торговых марок tm.csv");
            
            return list;
        }
    }


}
