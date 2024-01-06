
using FinalOutput.ProductRelatedClasses;
using System.Collections.Generic;
using System.Linq;
using System;

namespace FinalOutput
{
    public class Menu
    {

        public const int PriceXLoc = 37;
        public const int QuantityXLoc = 57;
        public static IEnumerable<Account> accounts;
        public static List<Product> mainProducts;
        const int width = 78;
        const int height = 44;
        const int asciiHeaderX = 17;
        const int asciiHeaderY = 2;
        public const int posX = 15;
        public const int posY = 40;
        public static void MyCartMenu()
        {

            Stack<int> pageStack = new Stack<int>(new int[] { 0 });

            while (true)
            {
                Console.Clear();
                Console.CursorVisible = false;

                var cartProducts = MyCart.GetMyCartProducts().ToList();
                var mainProducts = Product.GetProducts().ToList();

                string[] myCartMenuItems = { "[1] Search Product", "[2] Remove Product", "[3] Checkout" };

                Menu.DisplayProducts(pageStack = new Stack<int>(new int[] { 0 }), cartProducts, myCartMenuItems);


                //Create a RemovingProduct function
                var myCartUserInput = Console.ReadKey(true);

                switch (myCartUserInput.KeyChar)
                {
                    case '1':
                        //Search
                        break;

                    case '2':
                        //Remove product from cart

                        Console.WriteLine("Remove from cart");

                        Console.Write("Enter the product: ");
                        string productName = Console.ReadLine().Trim();
                        Console.Write("Enter the quantity: ");
                        int inputproductQuantity = int.Parse(Console.ReadLine());

                        var selectedProduct = cartProducts.Find(product => product.ProductName.ToLower() == productName.ToLower());

                        Console.Write("Remove from cart?: ");

                        string yesOrNo = Console.ReadLine().Trim().ToLower();


                        //Make a function out of these...

                        bool productValidator = ProductValidator.CheckIfProductIsInStock(selectedProduct, inputproductQuantity);

                        if (yesOrNo == "y")
                        {
                            var mainProduct = mainProducts.Find(product => product.ProductName.ToLower() == productName.ToLower());

                            if (mainProduct == null)
                            {
                                selectedProduct.ProductQuantity = inputproductQuantity;
                                InventorySystem.CreateStock(new Product(selectedProduct.ProductName, selectedProduct.ProductPrice, inputproductQuantity), InventorySystem.mainStockFolder);

                                selectedProduct.ProductQuantity -= inputproductQuantity;

                                //Checking if product quantity is 0
                                if (ProductValidator.CheckIfProductQuantityIsZero(selectedProduct))
                                {
                                    MyCart.RemoveProductFromCart(selectedProduct);
                                    InventorySystem.DeleteStock(selectedProduct, MyCart.myCartStockFolder);
                                }

                                Product.UpdateProduct(selectedProduct, MyCart.myCartStockFolder);
                            }
                            else if (File.Exists(GetFilePath.TextFilePath(InventorySystem.mainStockFolder, mainProduct.ProductName)))
                            {
                                //Adding the items to the Main Product path
                                mainProduct.ProductQuantity += inputproductQuantity;
                                Product.UpdateProduct(mainProduct, InventorySystem.mainStockFolder);

                                //Reducing the items in this cart
                                selectedProduct.ProductQuantity -= inputproductQuantity;

                                //Checking if product quantity is 0
                                if (ProductValidator.CheckIfProductQuantityIsZero(selectedProduct))
                                {
                                    MyCart.RemoveProductFromCart(selectedProduct);
                                    InventorySystem.DeleteStock(selectedProduct, MyCart.myCartStockFolder);
                                }

                                Product.UpdateProduct(selectedProduct, MyCart.myCartStockFolder);

                            }


                        }



                        break;

                    case '3':
                        //Checkout

                        break;

                    default:

                        if (myCartUserInput.Key == ConsoleKey.RightArrow)
                        {
                            //Call previous function
                            PageHandler.NextPage(pageStack, cartProducts);
                        }
                        else if (myCartUserInput.Key == ConsoleKey.LeftArrow)
                        {
                            //Call next function
                            PageHandler.PreviousPage(pageStack);
                        }
                        else if (myCartUserInput.Key == ConsoleKey.Escape)
                        {
                            return;
                        }

                        break;
                }


            }




        }


        public static void InventoryMenu()
        {
            Console.Title = "Inventory Menu";


            Console.SetWindowSize(width, height);

            //Admin and Costumer hard coded


            string[] mainMenuItems = { "[1] Add Product", "[2] Remove Product", "[3] Edit Product", "[4] Search", "[esc] exit" };


            accounts = FileManager.GetCostumerAccounts();

            Stack<int> pageStack = new Stack<int>(new int[] { 0 });


            while (true)
            {

                Console.CursorVisible = false;

                Console.Clear();
                mainProducts = Product.GetProducts().ToList();
                var cartProducts = MyCart.GetMyCartProducts().ToList();
                Menu.DisplayProducts(pageStack, mainProducts, mainMenuItems);

                AsciiArt.PrintInventoryAsciiArt(17, asciiHeaderY);
                Console.SetCursorPosition(15, 40);

                var inputKey = Console.ReadKey(true);

                switch (inputKey.KeyChar)
                {
                    case '1':

                        Console.WriteLine("Type a product you would like to add: ");

                        Product tempProd = Product.CreateProduct(mainProducts);

                        if (tempProd != null)
                        {
                            mainProducts.Add(tempProd);
                            InventorySystem.CreateStock(tempProd, InventorySystem.mainStockFolder);
                        }

                        if (PageHandler.CheckIfPageIsFullOrEmpty(pageStack, mainProducts))
                        {
                            PageHandler.NextPage(pageStack, mainProducts);
                        }

                        break;

                    case '2':
                        InventorySystem.RemoveProductFromInventory(pageStack, mainProducts);



                        break;

                    case '3':

                        Product.EditProduct(mainProducts);

                        break;
                    case '5':


                        break;

                    case '6':



                        break;
                    default:

                        if (inputKey.Key == ConsoleKey.RightArrow)
                        {
                            //Call previous function
                            PageHandler.NextPage(pageStack, mainProducts);
                        }
                        else if (inputKey.Key == ConsoleKey.LeftArrow)
                        {
                            //Call next function
                            PageHandler.PreviousPage(pageStack);
                        }
                        else if (inputKey.Key == ConsoleKey.Escape)
                        {
                            return;
                        }


                        break;

                }
            }


        }

        public static void StartPOSForAdmin()
        {
            Console.Title = "POS(ADMIN)";
            Console.SetWindowSize(width, height);

            //Admin and Costumer hard coded


            string[] mainMenuItems = { "[1] Add to cart", "[2] My Cart", "[3] Search", "[4] Checkout", "[5] Inventory", "[6] Add Product", "[esc] Logout" };


            accounts = FileManager.GetCostumerAccounts();

            Stack<int> pageStack = new Stack<int>(new int[] { 0 });


            while (true)
            {

                Console.CursorVisible = false;

                Console.Clear();
                mainProducts = Product.GetProducts().ToList();
                var cartProducts = MyCart.GetMyCartProducts().ToList();
                Menu.DisplayProducts(pageStack, mainProducts, mainMenuItems);

                AsciiArt.PrintPOSAsciiArt(30, asciiHeaderY);
                Console.SetCursorPosition(15, 40);

                var inputKey = Console.ReadKey(true);

                switch (inputKey.KeyChar)
                {
                    case '1':

                        Console.CursorVisible = true;

                        Console.SetCursorPosition(15, 40);

                        Console.Write("Enter the product: ");
                        string productName = Console.ReadLine().Trim();
                        Console.SetCursorPosition(15, 41);

                        Console.Write("Enter the quantity: ");


                        int inputproductQuantity = int.Parse(Console.ReadLine());


                        var selectedProduct = mainProducts.Find(product => product.ProductName.ToLower() == productName.ToLower());
                        Console.SetCursorPosition(15, 42);

                        Console.Write("Add to cart? (Y/N): ");

                        string yesOrNo = Console.ReadLine().Trim().ToLower();


                        //Make a function out of these...
                        bool productValidator = ProductValidator.CheckIfProductIsInStock(selectedProduct, inputproductQuantity);

                        if (yesOrNo == "y" && productValidator == true)
                        {
                            var cartProduct = cartProducts.Find(product => product.ProductName.ToLower() == productName.ToLower());

                            if (cartProduct == null)
                            {

                                InventorySystem.CreateStock(new Product(selectedProduct.ProductName, selectedProduct.ProductPrice, inputproductQuantity), MyCart.myCartStockFolder);

                                selectedProduct.ProductQuantity -= inputproductQuantity;

                                //Checking if product quantity is 0
                                if (ProductValidator.CheckIfProductQuantityIsZero(selectedProduct))
                                {
                                    MyCart.RemoveProductFromCart(selectedProduct);
                                    InventorySystem.DeleteStock(selectedProduct, InventorySystem.mainStockFolder);
                                }

                                Product.UpdateProduct(selectedProduct, InventorySystem.mainStockFolder);
                            }
                            else if (File.Exists(GetFilePath.TextFilePath(InventorySystem.mainStockFolder, cartProduct.ProductName)))
                            {
                                //Adding the items to the MyCart Product path
                                cartProduct.ProductQuantity += inputproductQuantity;
                                Product.UpdateProduct(cartProduct, MyCart.myCartStockFolder);

                                //Reducing the items in this product
                                selectedProduct.ProductQuantity -= inputproductQuantity;

                                //Checking if product quantity is 0
                                if (ProductValidator.CheckIfProductQuantityIsZero(selectedProduct))
                                {

                                    InventorySystem.DeleteStock(selectedProduct, InventorySystem.mainStockFolder);
                                }

                                Product.UpdateProduct(selectedProduct, InventorySystem.mainStockFolder);

                            }

                        }

                        break;

                    case '2':
                        Console.Clear();

                        Console.CursorVisible = true;


                        Menu.MyCartMenu();



                        break;

                    case '3':
                        Console.WriteLine("Type a product you would like to add: ");

                        Product tempProd = Product.CreateProduct(mainProducts);

                        if (tempProd != null)
                        {
                            mainProducts.Add(tempProd);
                            InventorySystem.CreateStock(tempProd, InventorySystem.mainStockFolder);
                        }

                        if (PageHandler.CheckIfPageIsFullOrEmpty(pageStack, mainProducts))
                        {
                            PageHandler.NextPage(pageStack, mainProducts);
                        }


                        break;
                    case '5':

                        InventoryMenu();
                        break;

                    case '6':

                        InventorySystem.AddProductToInventory(pageStack, mainProducts);

                        break;
                    default:

                        if (inputKey.Key == ConsoleKey.RightArrow)
                        {
                            //Call previous function
                            PageHandler.NextPage(pageStack, mainProducts);
                        }
                        else if (inputKey.Key == ConsoleKey.LeftArrow)
                        {
                            //Call next function
                            PageHandler.PreviousPage(pageStack);
                        }
                        else if (inputKey.Key == ConsoleKey.Escape)
                        {
                            return;
                        }


                        break;

                }
            }

        }


        public static void StartAuthorization()
        {
            Console.CursorVisible = false;
            Console.Clear();
            string[] options = { "Admin", "Cashier", "Costumer", "Exit" };

            AsciiArt.PrintAuthorizationAsciiArt(16, 4);


            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($"[{i + 1}] {options[i]}");
            }


            ConsoleKeyInfo key = Console.ReadKey(true);
            string repeatProcess = String.Empty;
            switch (key.KeyChar)
            {
                case '1':

                    do
                    {
                        if (Login.LoginSystemForAdmin())
                        {
                            StartPOSForAdmin();
                        }
                        else
                        {
                            Console.WriteLine("Wrong username or password!");
                            Console.Write("Do you want to try again? (Y:N): ");
                            repeatProcess = Console.ReadLine().ToLower();
                        }

                    } while (repeatProcess == "y");

                    break;

                case '4':
                    Environment.Exit(0);
                    break;



            }



        }



        public static void DisplayProducts(Stack<int> pageStack, IEnumerable<Product> productsParameter, string[] menuItems)
        {

            #region
            /*
            int leftCoordTable = 10;
            int topCoordTable = 1;

            int leftProductItem = 11;
            int topProductItem = 3;

            int leftMenuOptions = 10;
            int topMenuOptions = 26;

            ---
            int leftProductItem = 16;
            int topProductItem = 9;
            ---
             * 
             */

            #endregion

            int leftCoordTable = 15;
            int topCoordTable = 7;

            int leftProductItem = 16;
            int topProductItem = 9;

            int leftMenuOptions = 15;
            int topMenuOptions = 34;

            #region Ruler
            for (int i = 0; i < width; i++)
            {
                if (i % 10 == 0 && i != 0)
                {
                    Console.Write(i.ToString().Substring(0, 1));
                }
                else
                {
                    Console.Write('-');
                }
            }
            #endregion

            List<Product> products = productsParameter.ToList();

            int page = PageHandler.GetPage(products);


            int count = products.Count();
            int currentPage = pageStack.Peek();


            Menu.PrintTable(ref leftCoordTable, ref topCoordTable);
            Menu.PrintHeader(ref leftProductItem, ref topProductItem);
            Menu.PrintProducts(ref leftProductItem, ref topProductItem, currentPage, count, products);

            Console.SetCursorPosition(15, 31);
            Console.WriteLine($"Count: {products.Count}");


            PrintMenuOptions(leftMenuOptions, topMenuOptions, menuItems);


            Console.SetCursorPosition(29, ++topCoordTable);

            Console.Write("<<   ");
            Console.Write($"Page: {currentPage / 10 + 1} / {page}   ");
            Console.WriteLine(">>");


            //28


        }

        static void PrintMenuOptions(int left, int top, params string[] menuItems)
        {
            for (int i = 0; i < menuItems.Length; i++)
            {
                Console.SetCursorPosition(left, top++);
                Console.WriteLine(menuItems[i]);
            }

        }

        public static void PrintHeader(ref int left, ref int top)
        {
            Console.SetCursorPosition(left, top);
            Console.Write("Product");
            Console.SetCursorPosition(PriceXLoc, top);

            Console.Write("Price");
            Console.SetCursorPosition(QuantityXLoc, top);
            Console.Write("Quantity");
            top += 2;

        }


        public static void PrintProducts(ref int left, ref int top, int currentPage, int count, List<Product> products)
        {


            for (int i = currentPage; i < currentPage + 10 && i < count; i++)
            {
                Console.SetCursorPosition(left, top);
                Product product = products[i];
                Console.Write($"{product.ProductName}");

                Console.SetCursorPosition(PriceXLoc, top);
                Console.Write($"{product.ProductPrice:C}");

                Console.SetCursorPosition(QuantityXLoc, top);
                Console.Write($"{product.ProductQuantity:N0}");

                top += 2;
            }


        }


        public static void PrintTable(ref int left, ref int top)
        {


            string line = "--------------------------------------------------";

            Console.SetCursorPosition(left, ++top);

            Console.WriteLine(new string('_', line.Length + 1));

            //First bar
            Console.SetCursorPosition(left, ++top);
            Console.Write('|');

            //Insert the product here


            Console.SetCursorPosition(65, top);
            //end bar
            Console.Write('|');


            for (int i = 0; i < 10; i++)
            {
                Console.SetCursorPosition(left, ++top);

                //below the product

                Console.Write('|' + new string('-', line.Length - 1) + '|');
                //Start new row table
                Console.SetCursorPosition(left, ++top);
                Console.Write('|');

                //Insert product here


                Console.SetCursorPosition(65, top);

                //table end
                Console.Write('|');

            }


            Console.SetCursorPosition(left, top = 30);
            Console.WriteLine(new string('=', line.Length + 1));
        }

    }
}

