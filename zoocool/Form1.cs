using MySql.Data.MySqlClient;
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
using zoocool.Xml2CSharp;

// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

namespace zoocool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {          
            Settings.ConStr = File.ReadAllLines("settings.txt").FirstOrDefault();
          
            //var listCategory = new CategoryB().GetListCategory();
            //var listParentCategory = listCategory.Where(x => x.Parent_Id == 0);
            //foreach (var item in listParentCategory)
            //{
            //    var listChieldrenItem = listCategory.Where(x => x.Parent_Id == item.Id);
            //    var listChieldNode = new List<TreeNode>();
            //    foreach (var itemChield in listChieldrenItem)
            //    {
            //        listChieldNode.Add(new TreeNode()
            //        {
            //            Text = itemChield.Name,
            //            Tag = itemChield.Id
            //        });
            //    }
            //    trCategory.Nodes.Add(new TreeNode(item.Name, listChieldNode.ToArray()) { Tag = item.Id });
            //}
        }

        private void trCategory_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var category_id = (int)(e.Node.Tag);
            if (category_id == 0)
            {
                grProduct.DataSource = new List<Product>();
                return;
            }
            var listProduct = new Product().GetListProduct(category_id);
            grProduct.DataSource = listProduct;


            var listProductId = listProduct.Select(x=>x.Id).Distinct();
            if (listProductId.Count() == 0) return;
            var listStrId = string.Join(",", listProductId);
            grFeature.DataSource = new Feature().GetListFetures(listStrId);
        }



        private void grProduct_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 1) return;
            var pr_id = int.Parse(grProduct.Rows[e.RowIndex].Cells["idDataGridViewTextBoxColumn"].Value.ToString());
            grSkus.DataSource = new Skus().GetListSkus(pr_id);
        }

        private void grFeature_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            var list = new FeatureValue().GetValues(int.Parse(grFeature.Rows[e.RowIndex].Cells["idDataGridViewTextBoxColumn2"].Value.ToString()));
            grFeatureValue.DataSource = list;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            new Yml_catalog().hhh();
            File.WriteAllLines("erroresUrls.txt", Settings.Urls.Distinct());
            MessageBox.Show("OK \n Errores: \n " + string.Join(",\n ", Settings.Errores.Distinct()) +
                "\n Dublicate: \n " + string.Join(",\n ", Settings.Dublicates.Distinct()) +
                "\n Urls: \n " + string.Join(",\n ", Settings.Urls.Distinct()));
        }
    }
}
