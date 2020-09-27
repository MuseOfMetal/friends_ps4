using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Models
{
    class Product
    {
        public static int FreeId { get; set; }
        public static int FindFreeId(List<Product> products)
        {
            int max = 0;
            for (int i = 0; i < products.Count; i++)
            {
                if (max < products[i].Id)
                    max = products[i].Id;
            }
            return max;
        }

        public static int FindProduct(List<Product> products, int id)
        {
            for (int i = 0; i < products.Count; i++)
            {
                if (id == products[i].Id)
                    return i;
            }
            return -1;
        }


        public static void Save()
        {
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter("products.json"))
            {
                writer.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(Program.Products));
            }
        }

        public static void Load()
        {
            if (System.IO.File.Exists("products.json"))
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader("products.json"))
                {
                    var a = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Product>>(reader.ReadToEnd());
                    if (a != null)
                    {
                        Program.Products = a;
                        FreeId = FindFreeId(Program.Products) + 1;
                    }
                }

                
            }

        }

        public string Name { get; set; }
        public string Description { get; set; }
        public bool isVisible { get; set; }
        public int Id { get; set; }
        public void Visible()
        {
            isVisible = true;
        }

        public void NotVisible()
        {
            isVisible = false;
        }
    }
}
