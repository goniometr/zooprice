﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoocool
{
    using System;
    using System.Xml.Serialization;
    using System.Collections.Generic;
    using System.IO;

    namespace Xml2CSharp
    {
        [XmlRoot(ElementName = "currency")]
        public class Currency
        {
            [XmlAttribute(AttributeName = "id")]
            public string Id { get; set; }
            [XmlAttribute(AttributeName = "rate")]
            public string Rate { get; set; }
        }

        [XmlRoot(ElementName = "currencies")]
        public class Currencies
        {
            [XmlElement(ElementName = "currency")]
            public Currency Currency { get; set; }
        }

        [XmlRoot(ElementName = "category")]
        public class Category
        {
            [XmlAttribute(AttributeName = "id")]
            public string Id { get; set; }
            [XmlText]
            public string Text { get; set; }
            [XmlAttribute(AttributeName = "parentId")]
            public string ParentId { get; set; }
        }

        [XmlRoot(ElementName = "categories")]
        public class Categories
        {
            [XmlElement(ElementName = "category")]
            public List<Category> Category { get; set; }
        }

        [XmlRoot(ElementName = "param")]
        public class Param
        {
            [XmlAttribute(AttributeName = "name")]
            public string Name { get; set; }
            [XmlText]
            public string Text { get; set; }
        }

        [XmlRoot(ElementName = "offer")]
        public class Offer
        {
            [XmlElement(ElementName = "url")]
            public string Url { get; set; }
            [XmlElement(ElementName = "price")]
            public string Price { get; set; }
            [XmlElement(ElementName = "currencyId")]
            public string CurrencyId { get; set; }
            [XmlElement(ElementName = "categoryId")]
            public string CategoryId { get; set; }
            [XmlElement(ElementName = "picture")]
            public List<string> Picture { get; set; }
            [XmlElement(ElementName = "vendor")]
            public string Vendor { get; set; }
            [XmlElement(ElementName = "stock_quantity")]
            public string Stock_quantity { get; set; }
            [XmlElement(ElementName = "name")]
            public string Name { get; set; }
            [XmlElement(ElementName = "description")]
            public string Description { get; set; }
            [XmlElement(ElementName = "param")]
            public List<Param> Param { get; set; }
            [XmlAttribute(AttributeName = "id")]
            public string Id { get; set; }
            [XmlAttribute(AttributeName = "available")]
            public string Available { get; set; }
        }

        [XmlRoot(ElementName = "offers")]
        public class Offers
        {
            [XmlElement(ElementName = "offer")]
            public List<Offer> Offer { get; set; }
        }

        [XmlRoot(ElementName = "shop")]
        public class Shop
        {
            [XmlElement(ElementName = "name")]
            public string Name { get; set; }
            [XmlElement(ElementName = "company")]
            public string Company { get; set; }
            [XmlElement(ElementName = "url")]
            public string Url { get; set; }
            [XmlElement(ElementName = "currencies")]
            public Currencies Currencies { get; set; }
            [XmlElement(ElementName = "categories")]
            public Categories Categories { get; set; }
            [XmlElement(ElementName = "offers")]
            public Offers Offers { get; set; }

            


            public Shop GetShops()
            {
                var listVendor = new Vendor().GetListVendor();
                var baseUrl = @"https://zoocool.com.ua/";
                var list = new List<Shop>();
                var listCategory = new CategoryB().GetListCategory().Select(x=>x.Id).ToList<int>();
                var shop = new Shop()
                {
                    Name = "NEMO SHOP",
                    Company = "NEMO SHOP",
                    Url = "https://zoocool.com.ua",
                    Currencies = new Currencies() { Currency = new Currency() { Id = "UAH", Rate = "1" } },
                    //Categories = new Categories() { Category = categories },
                    //Offers = new Offers() { Offer = listOffes }
                };


                var listAdd = new Product().GetNAmeReplace();
                var listPictureAdd = new Picture().GetListPicture();
                foreach (var categoryId in listCategory)
                {
                    var ListCat = new CategoryB().GetListcategoryToPrice(categoryId);
                    var categories = new List<Category>();
                    foreach (var cat in ListCat)
                    {
                        if (!listPictureAdd.Select(x => x.category).Contains(cat.Id)) continue;
                        if (cat.Parent_Id == 0)
                        {
                            categories.Add(new Category()
                            {
                                Id = cat.Id.ToString(),
                                Text = cat.Name
                            });
                        }
                        else
                        {
                            categories.Add(new Category()
                            {
                                Id = cat.Id.ToString(),
                                Text = cat.Name,
                                ParentId = cat.Parent_Id.ToString()                              
                            });
                            var parent = new CategoryB().GetListcategoryToPrice(cat.Parent_Id);
                            categories.Add(new Category()
                            {
                                Id = parent.FirstOrDefault().Id.ToString(),
                                //ParentId = parent.FirstOrDefault().Parent_Id.ToString(),
                                Text = parent.FirstOrDefault().Name.ToString()
                            });
                        }
                    }

                    var listOffes = new List<Offer>();

                    var products = new Product().GetListProduct(categoryId);
                    if (products.Count() == 0) continue;
                    foreach(var product in products)
                    {
                        
                        if (!listPictureAdd.Where(x => x.link.Contains(product.Url)).Any()) continue;
                        
                        var skuses = new Skus().GetListSkus(product.Id);

                        if (product.Images.Count() == 0)
                        {
                            continue;
                        }
                        foreach(var skus in skuses)
                        {
                            var parames = new List<Param>();
                            var listParames = new Paramses().GetListParams(product.Id, skus.Id);
                            foreach (var item in listParames)
                            {
                                parames.Add(
                                    new Param()
                                    {
                                        Name = item.Name.ToString(),
                                        Text = item.Text.ToString()
                                    });
                            }

                            var additname = string.Empty;

                            if (product.addparam > 0)
                            {
                                var addName = listAdd[product.addparam];
                                var add = parames.Where(x => x.Name == addName).FirstOrDefault();

                                if (add != null)
                                    additname = " " + add.Text;

                            }
                            if (skus.Price == 0) skus.Price = product.Price;
                            if (skus.Price == 0) continue;
                                
                            

                            listOffes.Add(new Offer()
                            {
                                Id = skus.Id.ToString(),
                                Available = "true",
                                Url = baseUrl + product.Url.ToString(),
                                Price = skus.Price.ToString(),
                                CurrencyId = "UAH",
                                CategoryId = categoryId.ToString(),
                                Picture = product.Images,
                                Vendor = listVendor.Where(x=>x.Product_id == skus.Product_id)?.FirstOrDefault()?.Name,
                                Stock_quantity = product.Count.ToString(),
                                Name = product.Name.ToString() + additname.ToString(),
                                Description = product.Description.ToString(),
                                Param = parames
                            });
                        }
                    }
                    if (listOffes.Count() == 0) continue;
                    //var shop = new Shop()
                    //{
                    //    Name = "NEMO SHOP",
                    //    Company = "NEMO SHOP",
                    //    Url = "https://zoocool.com.ua",
                    //    Currencies = new Currencies() { Currency = new Currency() { Id = "UAH", Rate = "1" } },
                    //    Categories = new Categories() { Category = categories },
                    //    Offers = new Offers() { Offer = listOffes}
                    //};
                    //list.Add(shop);
                    if (shop.Categories == null)
                        shop.Categories = new Categories() { Category = categories };
                    else
                    {
                        shop.Categories.Category.AddRange(categories);
                       // shop.Categories.Category = shop.Categories.Category.ToList().Distinct().ToList();
                    }
                    if (shop.Offers == null)
                        shop.Offers = new Offers() { Offer = listOffes };
                    else shop.Offers.Offer.AddRange(listOffes);
                    shop.Offers.Offer = shop.Offers.Offer.GroupBy(x => x.Id).Select(x => new Offer { Id = x.Key, 
                        Available = x.Select(z => z.Available).FirstOrDefault(),
                        CategoryId = x.Select(z=>z.CategoryId).FirstOrDefault(),
                        CurrencyId = x.Select(z=>z.CurrencyId).FirstOrDefault(),
                        Description = x.Select(z=>z.Description).FirstOrDefault(),
                        Name = x.Select(z=>z.Name).FirstOrDefault(),
                        Param = x.Select(z=>z.Param).FirstOrDefault(),
                        Picture = x.Select(z=>z.Picture).FirstOrDefault(),
                        Price = x.Select(z=>z.Price).FirstOrDefault(),
                        Stock_quantity = x.Select(z=>z.Stock_quantity).FirstOrDefault(),
                        Url = x.Select(z=>z.Url).FirstOrDefault(),
                        Vendor  = x.Select(z=>z.Vendor).FirstOrDefault()
                    }).ToList();

                    var listName = shop.Offers.Offer.GroupBy(x => x.Name);
                    foreach (var item in listName)                    
                        if (item.Count() > 1)                        
                            Settings.Dublicates.Add(string.Concat(item.Select(x=>x.CategoryId).FirstOrDefault(), item.Select(x => x.Url).FirstOrDefault()));

                    foreach (var item in shop.Offers.Offer)                    
                        if (item.Description.Contains(" href"))
                            Settings.Urls.Add(item.Url);
                    
                }
                if (shop.Categories == null) return shop;
                shop.Categories.Category = shop.Categories.Category.GroupBy(x => x.Id).Select(x => new Category { Id = x.Key, ParentId = x.Select(z => z.ParentId).FirstOrDefault(), Text = x.Select(z => z.Text).FirstOrDefault() }).ToList();
                return shop;
            }

            //public override string ToString()
            //{
            //    return ToString().Replace("\"", "&quot;").Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;");
            //}
        }


        [XmlRoot(ElementName = "yml_catalog")]
        public class Yml_catalog
        {
            [XmlElement(ElementName = "shop")]
            public Shop Shop { get; set; }
            [XmlAttribute(AttributeName = "date")]
            public string Date { get; set; }

            public void hhh()
            {
                var yml = new Yml_catalog()
                {
                    Date = DateTime.Now.ToString("yyyy-MM-dd mm:ss"),
                    Shop = new Shop().GetShops()
                };

                XmlSerializer formatter = new XmlSerializer(typeof(Yml_catalog));

             


                using (FileStream fs = new FileStream("price.xml", FileMode.Create))
                {
                    formatter.Serialize(fs, yml);
                }

            }

        }

    }

}