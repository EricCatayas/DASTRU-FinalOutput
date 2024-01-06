using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FinalOutput
{
    public static class MyCart
    {
        public static LinkedList<Product> CartProducts { get; private set; } = new LinkedList<Product>();

        public static decimal TotalCost { get; private set; } = 0;

        public readonly static string myCartStockFolder = @"MyCart";
        #region My Cart

        //Create MyCart class

        //Add My Cart display (resuse the DisplayFunction)

        //Check out function

        //When user checks out make a prompt for finalization

        //User can also remove items from the cart (IMPORTANT)

        //User can select which item to checkout?

        //When user gets the product ma minusan ang physical inventory and ang displayed products sa terminal

        #endregion


        //Outside this function there should be a validator to check if the user really wants to buy the items
        public static void CheckOut(Costumer costumer)
        {
            if (BalanceValidator(costumer))
            {
                //Read from file, use the iterate file function then write text function to deduct the stock of the product
                //And also deduct the balance of the costumer


                foreach (var product in Product.GetProducts())
                {
                    //if (product.ProductName == )
                }
                

            }
        }

        public static void MyCartOptions(ConsoleKeyInfo input)
        {
            switch(input.KeyChar)
            {
                case '1':
                    //Search function
                    break;

                case '2':
                    //RemoveFunction
                    break;
            }
        }


        public static IEnumerable<Product> GetMyCartProducts()
        {
            string path = GetFilePath.FilePath(myCartStockFolder);

            var txtFiles = Directory.EnumerateFiles(path, "*.txt");

            var line = FileManager.IterateTextFiles(txtFiles);
            foreach (var property in line)
            {
                yield return new Product(property[0], decimal.Parse(property[1]), int.Parse(property[2]));
            }
        }


        public static bool BalanceValidator(Costumer costumer)
        {
            return costumer.Balance > TotalCost;

        }

        public static void AddToCart(Product product)
        {
            CartProducts.AddLast(product);
            InventorySystem.CreateStock(product, myCartStockFolder);
        }

        public static void SubtractQuantity()
        {

        }


        /// <summary>
        /// 
        /// </summary>
        public static void RemoveProductFromCart(Product product)
        {
            CartProducts.Remove(product);
        }



    }
}
