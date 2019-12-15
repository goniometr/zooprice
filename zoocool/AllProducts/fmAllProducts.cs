using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using zoocool.OldPrice;
using zoocool.Xml2CSharp;

namespace zoocool.AllProducts
{
    public partial class fmAllProducts : Form
    {
        Yml_catalog catalog;
        public fmAllProducts()
        {
            InitializeComponent();
        }


        private Yml_catalog GetCatalog() 
        {
            XmlSerializer formatter = new XmlSerializer(typeof(Yml_catalog));

            //Yml_catalog ob;

            using (FileStream fs = new FileStream("price.xml", FileMode.Open))
            {
                catalog = (Yml_catalog)formatter.Deserialize(fs);
            }
            return catalog;
        }


        private void fmAllProducts_Load(object sender, EventArgs e)
        {

            catalog = GetCatalog();

            var listCategory = catalog.Shop.Categories.Category;

            /*

            var list = new List<CategoryB>();
            var listCategory = new CategoryB().GetListCategory();


            foreach (var item in listCategory) 
            {
                var obList = new CategoryB().GetListcategoryToPrice(item.Id);
                foreach (var ob in obList) 
                {
                    if (!list.Any(x=>x.Id == ob.Id)) 
                    {
                        o
                        list.Add(ob);
                    }
                }
            }
            


    */
            GetListNodes(null, listCategory);
            
        }


        private void GetListNodes(TreeNode parentNode, List<Category> listCategory)
        {


            //var listNodes = new List<TreeNode>();
            if (parentNode == null)
            {
                var listParentCategory = listCategory.Where(x => x.ParentId == null).ToList();

                foreach (var item in listParentCategory)
                {

                    var node = new TreeNode()
                    {
                        Text = item.Text,
                        Tag = item.Id
                        
                    };
                    trCategory.Nodes.Add(node);
                    GetListNodes(node, listCategory);
                    //listNodes.Add(node);
                }
            }
            else 
            {
                var listParentCategory = listCategory.Where(x => x.ParentId == parentNode.Tag.ToString()).ToList();

               // if (listParentCategory.Count == 0) return 
                foreach (var item in listParentCategory)
                {

                    var node = new TreeNode()
                    {
                        Text = item.Text,
                        Tag = item.Id
                    };
                    parentNode.Nodes.Add(node);
                    GetListNodes(node, listCategory);
                }
            }

            
            


            //return listNodes;
        }

        private void trCategory_AfterSelect(object sender, TreeViewEventArgs e)
        {

            grProduct.DataSource = catalog.Shop.Offers.Offer
                .Where(x => x.CategoryId == e.Node.Tag.ToString())
                .Select(x => new BaseOldPrice()
                {
                    //CategoryName = listCategory.Where(y => y.Id == x.CategoryId).Select(y => y.Text).FirstOrDefault(),
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Price_Old = x.Price_old
                }).ToList(); 
        }
    }
}
