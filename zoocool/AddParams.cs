using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace zoocool
{
    public class AddParam
    {
        public int GroupId { get; set; }
        public string ParamName { get; set; }
        public string ParamValue { get; set; }


        public void SerializeList()
        {
            var ob = new AddParam() { GroupId = 5, ParamName = "Доставка/Оплата", ParamValue = "Предварительная доставка" };
            var ob1 = new AddParam() { GroupId = 6, ParamName = "Доставка/Оплата1", ParamValue = "Предварительная доставка1" };
            var ob2 = new AddParam() { GroupId = 7, ParamName = "Доставка/Оплата2", ParamValue = "Предварительная доставка2" };

            var list = new List<AddParam>() { ob, ob1, ob2 };

            XmlSerializer formatter = new XmlSerializer(typeof(List<AddParam>));
            // получаем поток, куда будем записывать сериализованный объект
            using (FileStream fs = new FileStream("AddParams.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, list);               
            }
        }


        public List<AddParam> GetListAddParam()
        {
            var list = new List<AddParam>();
            XmlSerializer formatter = new XmlSerializer(typeof(List<AddParam>));
            using (FileStream fs = new FileStream("AddParams.xml", FileMode.OpenOrCreate))
            {
                list = (List<AddParam>)formatter.Deserialize(fs);                
            }
            return list;
        }
    }

   


}
