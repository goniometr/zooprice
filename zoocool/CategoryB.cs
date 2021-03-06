﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoocool
{
    class CategoryB
    {
        public int Id { get; set; }
        public int Parent_Id { get; set; }
        public string Name { get; set; }

        public List<CategoryB> GetListCategory()
        {
            var list = new List<CategoryB>();
            MySqlConnection conn = new MySqlConnection(Settings.PriceSettings.ConStr); 
            conn.Open();            
            string sql = "SELECT id, parent_id, name FROM shop_category";
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader = command.ExecuteReader();            
            while (reader.Read())
            {
                list.Add(new CategoryB()
                {
                    Id = int.Parse(reader[0].ToString()),
                    Parent_Id = int.Parse(reader[1].ToString()),
                    Name = reader[2].ToString()
                });
            }
            reader.Close();
            conn.Close();
            return list;
        }

        public List<CategoryB> GetListcategoryToPrice(int id)
        {
            var list = new List<CategoryB>();
            MySqlConnection conn = new MySqlConnection(Settings.PriceSettings.ConStr);
            conn.Open();
            string sql = string.Format("SELECT id, parent_id, name from shop_category where id = {0} union select id, parent_id, name from shop_category where id in (select parent_id from shop_category where id = {0})", id);
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new CategoryB()
                {
                    Id = int.Parse(reader[0].ToString()),
                    Parent_Id = int.Parse(reader[1].ToString()),
                    Name = reader[2].ToString()
                });
            }
            reader.Close();
            conn.Close();

            var listCategoryReplace = new CategoryReplace().GetCategoryReplace();
            foreach (var item in list)            
                if (listCategoryReplace.Select(x => x.Id).Contains(item.Id))
                    item.Name = listCategoryReplace.Where(x => x.Id == item.Id)?.FirstOrDefault()?.NewName;
            
            return list;
        }
    }


    class CategoryReplace
    {
        public int Id { get; set; }
        public string NewName { get; set; }

        public List<CategoryReplace> GetCategoryReplace()
        {
            var list = new List<CategoryReplace>();
            if (!File.Exists("listCategory.csv")) 
            {
                Settings.Errores.Add("Файл с категориями отсутствует");
                return list;
            }
            using (var csvParser = new StreamReader("listCategory.csv"))
            {
                while (!csvParser.EndOfStream)
                {
                    var lineIn = csvParser.ReadLine().Split(';');
                    
                    var res = false;
                    var id = 0;
                    res = int.TryParse(lineIn[0], out id);                                        
                    list.Add(new CategoryReplace() { Id = id, NewName = lineIn[1] });
                }
            }
            return list;
        }
    }
}
