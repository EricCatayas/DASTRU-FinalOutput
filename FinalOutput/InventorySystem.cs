using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using Microsoft.SqlServer.Server;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Text;

namespace FinalOutput
{
    public class InventorySystem
    {

        public readonly static string mainStockFolder = @"StockFile";

        public static void AddProductToInventory(Stack<int> pageStack, List<Product> mainProducts)
        {
            Console.CursorVisible = true;

            Product tempProd = Product.CreateProduct(mainProducts);

            if (tempProd != null)
            {
                mainProducts.Add(tempProd);
                CreateStock(tempProd, mainStockFolder);
            }

            if (PageHandler.CheckIfPageIsFullOrEmpty(pageStack, mainProducts))
            {
                PageHandler.NextPage(pageStack, mainProducts);
            }
        }


        public static void RemoveProductFromInventory(Stack<int> pageStack, List<Product> mainProducts)
        {
            Console.Write("Type a product you would like to delete: ");
            string prod = Console.ReadLine().Trim().ToLower();

            Product temp = mainProducts.Find(product => product.ProductName.ToLower() == prod);
            mainProducts.Remove(temp);
            DeleteStock(temp, mainStockFolder);

            if (PageHandler.CheckIfPageIsFullOrEmpty(pageStack, mainProducts))
            {
                PageHandler.PreviousPage(pageStack);
            }
        }



        /// <summary>
        /// Creates the stock in the StockFile folder
        /// </summary>
        /// <param name="product"></param>
        /// 
        public static void CreateStock(Product product, string folderName)
        {
            string path = GetFilePath.TextFilePath(folderName, product.ProductName);

            FileManager.CreateTextFile(path);
            FileManager.WriteTextFile(path, $"{product.ProductName},{product.ProductPrice},{product.ProductQuantity}");

        }


        /// <summary>
        /// Deletes the stock in the StockFile folder
        /// </summary>
        /// <param name="product"></param>
        public static void DeleteStock(Product product, string folderName)
        {

            string path = GetFilePath.TextFilePath(folderName, product.ProductName);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }




    }


}
