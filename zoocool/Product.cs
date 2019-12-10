using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoocool
{
    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public int Category_Id { get; set; }
        public string Url { get; set; }
        public string Vendor { get; set; }
        public List<string> Images { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }

        [NonSerialized]
        public int addparam;


        public List<Product> GetListProduct(int category_id)
        {
            var list = new List<Product>();
            if (category_id < 0) return list;
            var listPicture = new Picture().GetListPicture();

            //if (addParam < 1) return list;

            //var addlist = GetNAmeReplace();
           // if (string.IsNullOrEmpty(addlist[addParam])) Settings.Errores.Add("Not addParam with key " + addParam.ToString());


            MySqlConnection conn = new MySqlConnection(Settings.ConStr);
            conn.Open();
            string sql = string.Format("SELECT t1.id, t1.name, t1.summary, t1.description, t1.category_id, t1.url, t1.Price " +
"FROM shop_product t1 " +
"Where t1.category_id = {0}", category_id);
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (reader.FieldCount == 0) continue;
                
                //var addParam = 

                var category = int.Parse(reader[4].ToString());
                //if (listPicture.Where(x => x.link == reader[5].ToString()).FirstOrDefault()?.category != category) continue;
                var url1 = reader[5].ToString();
                var url = listPicture.Where(x => x.link == url1).FirstOrDefault()?.link;
                if (url == null) continue;
                var product = new Product();
                product.Id = int.Parse(reader[0].ToString());
                product.Name = reader[1].ToString();
                product.addparam = listPicture.Where(x => x.link == reader[5].ToString()).FirstOrDefault()?.addparam ?? 0;


                product.Summary = reader[2].ToString();
                product.Description = reader[3].ToString();
                product.Category_Id = category;
                product.Url = reader[5].ToString();               
                product.Images = listPicture.Where(x => x.link == reader[5].ToString()).FirstOrDefault()?.images;
                product.Count = (int)(listPicture.Where(x => x.link == reader[5].ToString()).FirstOrDefault()?.count);
                //product.Name = listPicture.Where(x => x.link == reader[5].ToString()).FirstOrDefault()?.name;
                product.Price = double.Parse(reader[6].ToString());
                list.Add(product);
            }
            reader.Close();
            conn.Close();
            return list;
        }

        public Dictionary<int, string> GetNameReplace()
        {
            var list = new Dictionary<int, string>();
            if (File.Exists("addname.csv"))
            {
                using (var csvParser = new StreamReader("addname.csv"))
                {
                    while (!csvParser.EndOfStream)
                    {
                        var lineIn = csvParser.ReadLine().Split(';');
                        if (lineIn.Count() < 2) continue;
                        list.Add(int.Parse(lineIn[0].ToString()), lineIn[1].ToString());
                    }
                }
            }
            else Settings.Errores.Add("Файл с дополнениями к названию addname.csv отсутствует");
            return list;
        }

    }


}
