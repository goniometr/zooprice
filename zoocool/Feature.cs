using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoocool
{
    class Feature
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Feature> GetListFetures(string whereString)
        {
            var list = new List<Feature>();
            //if (product_id < 0) return list;
            MySqlConnection conn = new MySqlConnection(Settings.PriceSettings.ConStr);
            conn.Open();
            string sql = string.Format("select DISTINCT t2.id, t2.name from shop_product_features t1" +
" inner join shop_feature t2 on t1.feature_id = t2.id" +
" Where t1.product_id in ({0})", whereString);
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (reader[1] == null) continue;
                list.Add(new Feature()
                {
                    Id = int.Parse(reader[0].ToString()),
                    Name = reader[1].ToString()
                });
            }
            reader.Close();
            conn.Close();
            return list;
        }
    }
}
