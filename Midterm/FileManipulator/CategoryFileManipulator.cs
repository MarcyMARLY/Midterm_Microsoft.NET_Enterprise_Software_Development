using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Midterm.Models;

namespace Midterm.FileManipulator
{
    public class CategoryFileManipulator : IFileManipulator<Models.Category>
    {
        public string Path { get; set; }
        private List<Category> CategoryCollection;

        public Category ConvertItem(string item)
        {
            var itemList = item.Split(";");
            return new Category
            {
                id = itemList[0],
                productName = itemList[1],
                pricePerKg = Convert.ToInt32(itemList[2]),
                totalAmount = Convert.ToInt32(itemList[3])
            };
        }

        public List<Category> GetCollection()
        {
            if(CategoryCollection == null)
            {
                var data = File.ReadAllLines(Path);
                CategoryCollection = data
                    .Select(x => ConvertItem(x))
                    .ToList();
            }
            return CategoryCollection;
        }

        public void WriteToFile(Category t)
        {
            string CategoryString  = t.id + ";"+ t.productName + ";" + t.pricePerKg + ";" + t.totalAmount + ";";

            using (StreamWriter sw = File.AppendText(Path))
            {
                sw.WriteLine(CategoryString);
            }
        }
    }
}
