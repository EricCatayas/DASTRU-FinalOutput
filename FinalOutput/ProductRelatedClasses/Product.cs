using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FinalOutput
{
    public class Product : IComparable<Product>
    {
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductQuantity { get; set; }

        const string productFolderName = @"StockFile";

        public Product(string productName, decimal productPrice, int productQuantity = 0)
        {
            ProductName = productName;
            ProductPrice = productPrice;
            ProductQuantity = productQuantity;
        }

        public static void UpdateProduct(Product product, string filePath)
        {
            string path = GetFilePath.TextFilePath(filePath, product.ProductName);

            FileManager.WriteTextFile(path, $"{product.ProductName},{product.ProductPrice},{product.ProductQuantity}");

        }


        public static void EditProduct(List<Product> products)
        {
            Console.Write("Product name: ");
            string selectedProduct = Console.ReadLine().Trim();

            Product getProduct = products.Find(product => product.ProductName.ToLower() == selectedProduct.ToLower());
            //Temporary 'til index

            Console.Write("Enter price: ");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.Write("Enter quantity: ");
            int quantity = int.Parse(Console.ReadLine());

            InventorySystem.CreateStock(new Product(getProduct.ProductName, price, quantity), InventorySystem.mainStockFolder);

        }


        public static Product CreateProduct(List<Product> products)
        {
            string enterProdName = "Enter product name: ";
            string enterProdPrice = "Enter product price: ";
            Console.Write(enterProdName);
            Console.SetCursorPosition(Menu.posX + enterProdName.Length, Menu.posY);
            string productName = Console.ReadLine();

            Console.SetCursorPosition(Menu.posX, Menu.posY + 1);

            Console.Write(enterProdPrice);

            Console.SetCursorPosition(Menu.posX + enterProdPrice.Length, Menu.posY + 1);

            decimal productPrice = decimal.Parse(Console.ReadLine());

            //Put in a different module
            if (products.Any(product => product.ProductName.ToLower() == productName.ToLower()))
            {
                return null;
            }
            if (String.IsNullOrEmpty(productName)) { return null; }
            //Get character for the first letter in the product name then add 32 to capitalize it

            return new Product(productName, productPrice);

        }


        /// <summary>
        /// Gets the products in the StockFile folder
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Product> GetProducts()
        {
            string path = GetFilePath.FilePath(productFolderName);

            var txtFiles = Directory.EnumerateFiles(path, "*.txt");

            var line = FileManager.IterateTextFiles(txtFiles);

            foreach (var property in line)
            {
                yield return new Product(property[0], decimal.Parse(property[1]), int.Parse(property[2]));
            }

        }


        //Used for sorting price instead of in alphabetical order (depending on the sorting of the folder).
        public int CompareTo(Product other)
        {
            if (this.ProductPrice > other.ProductPrice)
            {
                return 1;
            }
            else if (this.ProductPrice == other.ProductPrice)
            {
                return 0;
            }
            else
            {
                return -1;
            }

        }

    }


}
