using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoocool
{
    class Paramses
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public bool Status { get; set; }

        public List<Paramses> GetListParams(int product_id, int skus_id)
        {
            var list = new List<Paramses>();
            if (product_id < 0) return list;
            MySqlConnection conn = new MySqlConnection(Settings.PriceSettings.ConStr);
            conn.Open();
            string sql = string.Format("select t2.name, t3.value, 'true' " +
                "from shop_product_features t1 "
+" inner join shop_feature t2 on t1.feature_id = t2.id "
+" inner join shop_feature_values_varchar t3 on t1.feature_value_id = t3.id"
+ " where t1.product_id = {0} and(t1.sku_id is null or t1.sku_id = {1}) and t2.type = 'varchar' and t1.feature_id = t3.feature_id" +
" and  t2.status = 'public'"
+ " union "
+" select t5.name, CONCAT(t6.value, ' ', case  "
+" when t6.unit = 'cm' then 'см'"
+" when t6.unit = 'm' then 'м'"
+" when t6.unit = 'mm' then 'мм'"
+" when t6.unit = 'g' then 'г'"
+" when t6.unit = 'kg' then 'кг'"
+" when t6.unit = 'l' then 'л'"
+" when t6.unit = 'ml' then 'мл'"
+" when t6.unit = 'W' then 'Вт'"
+" when t6.unit = 'cm3' then 'см3'"
+" end), 'true' " +
"from shop_product_features t4"
+" inner join shop_feature t5 on t4.feature_id = t5.id"
+" inner join shop_feature_values_dimension t6 on t4.feature_value_id = t6.id"
+ " where t4.product_id = {0} and(t4.sku_id is null or t4.sku_id = {1}) and t5.type like 'dimension%' and t4.feature_id = t6.feature_id" +
" and  t5.status = 'public'"
+ " UNION"
+" select t11.name, t12.value, 'true' " +
"from shop_product_features t10"
+" inner  join shop_feature t11 on t10.feature_id = t11.id"
+" inner  join shop_feature_values_color t12 on t10.feature_value_id = t12.id"
+ " where t10.product_id = {0} and(t10.sku_id is null or t10.sku_id = {1}) and t11.type = 'color' and t10.feature_id = t12.feature_id" +
" and  t11.status = 'public'"
+ " union"
+" select t14.name, t15.value, 'true' " +
"from shop_product_features t13"
+" inner  join shop_feature t14 on t13.feature_id = t14.id"
+" inner  join shop_feature_values_double t15 on t13.feature_value_id = t15.id"
+ " where t13.product_id = {0} and(t13.sku_id is null or t13.sku_id = {1}) and t14.type = 'dimension.weight' " +
" and t15.feature_id = t13.feature_id and  t14.status = 'public'" +
"union " +
"select t17.name, t18.value, 'false' " +
"from shop_product_features t16 " +
"inner join shop_feature t17 on t16.feature_id = t17.id " +
"inner join shop_feature_values_double t18 on t16.feature_value_id = t18.id " +
"where t16.product_id = {0} and (t16.sku_id is null or t16.sku_id = {1}) " +
"and t17.type = 'double' and t16.feature_id = 183 and t18.feature_id = t16.feature_id and t17.status = 'private' and t17.id = 183", product_id, skus_id); //ИД 183 - наш параметр
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {

                list.Add(new Paramses()
                {
                    Name = reader[0].ToString(),
                    Text = reader[1].ToString(),
                    Status = bool.Parse(reader[2].ToString()) 

                });
            }
            reader.Close();
            conn.Close();
            return list;
        }
    }
}
