using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoocool
{
    class Skus
    {
        public int Id { get; set; }
        public int Product_id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public double OldPrice { get; set; }

        public List<Skus> GetListSkus(int product_id)
        {
            var list = new List<Skus>();
            if (product_id < 0) return list;
            MySqlConnection conn = new MySqlConnection(Settings.ConStr);
            conn.Open();
            string sql = string.Format("SELECT  t1.id, t1.product_id, t1.name, t1.price,  t18.value FROM shop_product_skus t1 left join shop_product_features t16 on t1.product_id = t16.product_id  and(t1.id = t16.sku_id or t16.sku_id is null) and t16.feature_id = 188 left join shop_feature t17 on t16.feature_id = t17.id and t16.feature_id = 188 left join shop_feature_values_double t18 on t16.feature_value_id = t18.id Where t1.product_id = {0}", product_id);
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var ob = new Skus();

                ob.Id = int.Parse(reader[0].ToString());
                ob.Product_id = int.Parse(reader[1].ToString());
                ob.Name = reader[2].ToString();
                ob.Price = double.Parse(reader[3].ToString());
                double oldPrice = 0;
                var res = double.TryParse(reader[4].ToString(), out oldPrice);                
                ob.OldPrice = oldPrice;

                if(ob.OldPrice <= ob.Price || ob.OldPrice < 0) 
                {
                    ob.OldPrice = 0;
                    Settings.Errores.Add(string.Format("Старая цена не актуальна {0}, {1}", product_id, ob.Id));
                }

                list.Add(ob);
            }
            reader.Close();
            conn.Close();
            return list;
        }
    }

}

