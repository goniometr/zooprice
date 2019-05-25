using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoocool
{
    class FeatureValue
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<FeatureValue> GetValues(int featureId)
        {
            var list = new List<FeatureValue>();
            MySqlConnection conn = new MySqlConnection(Settings.ConStr);
            conn.Open();
            string sql = string.Format("SELECT id, value from shop_feature_values_varchar where feature_id = {0}", featureId);
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (reader[1] == null) continue;
                list.Add(new FeatureValue()
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
