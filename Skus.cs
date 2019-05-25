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

        public List<Skus> GetListSkus(int product_id)
        {
            var list = new List<Skus>();
            if (product_id < 0) return list;
            MySqlConnection conn = new MySqlConnection(Settings.ConStr);
            conn.Open();
            string sql = string.Format("SELECT id, product_id, name, price FROM shop_product_skus Where product_id = {0}", product_id);
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Skus()
                {
                    Id = int.Parse(reader[0].ToString()),
                    Product_id = int.Parse(reader[1].ToString()),
                    Name = reader[2].ToString(),
                    Price = double.Parse(reader[3].ToString())
                });
            }
            reader.Close();
            conn.Close();
            return list;
        }
    }

}

