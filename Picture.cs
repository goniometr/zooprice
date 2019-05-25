using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoocool
{
    public class Picture
    {
        public string link;
        public List<string> images;
        public int category;
        public int count;
        public string name;
        public int addparam;

        public List<Picture> GetListPicture()
        {
            var list = new List<Picture>();

            var fullfiles = Directory.GetFiles("Source").ToList();
            List<string> files = new List<string>();
            foreach (var item in fullfiles)
            {
                var extention = Path.GetExtension(item);
                if (extention.ToLower() == ".csv")
                    files.Add(Path.GetFileName(item));
                else Settings.Errores.Add(string.Format("File {0} has not extention csv", item));

            }
            foreach (var item in files)
            {
                var t = item.Split('_');

                if (t.Count() < 4)
                {
                    Settings.Errores.Add(item + " have not parameters");
                    continue;
                }

                var catId = 0;
                var res = int.TryParse(t[0], out catId);

                if (catId == 0)
                {
                    Settings.Errores.Add(string.Format("файл {0} не содержит категорию в правильном формате", item));
                    continue;
                }

                var count = 0;               
                res = int.TryParse(t[1], out count);                
                if (count == 0) count = 10;

                var addparam = 0;
                res = int.TryParse(t[2], out addparam);
               
                using (var csvParser = new StreamReader("Source\\"+ item))
                {
                    //csvParser.ReadLine();
                    while (!csvParser.EndOfStream)
                    {
                        var lineIn = csvParser.ReadLine().Split(';');
                        var pic = new Picture();
                        pic.link = lineIn[1];
                        pic.category = catId;
                        pic.count = count;
                        pic.addparam = addparam;
                        pic.name = lineIn[0];
                        pic.images = new List<string>();
                        for (var i = 2; i < lineIn.Count(); i++)
                            if (!string.IsNullOrEmpty(lineIn[2].Trim()))
                                if (!string.IsNullOrEmpty(lineIn[i].Trim()))
                                    pic.images.Add(lineIn[i].Trim());
                        list.Add(pic);
                    }
                }
            }
            return list;
        }
    }
}
