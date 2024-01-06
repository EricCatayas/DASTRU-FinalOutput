using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalOutput
{
    public class PageHandler
    {
        public static int GetPage(List<Product> products)
        {
            if (products.Count % 10 == 0)
            {
                return products.Count / 10;
            }
            else
            {
                decimal count = Convert.ToDecimal(products.Count);
                return (int)Math.Ceiling(count / 10m);
            }

        }

        public static bool CheckIfPageIsFullOrEmpty(Stack<int> currentPage, List<Product> mainProducts)
        {

            return (mainProducts.Count % 10 == 0 && mainProducts.Count != 0) ? true : false;

        }


        public static void NextPage(Stack<int> pageStack, List<Product> products)
        {

            int count = products.Count;
            int currentPage = pageStack.Peek();
            int nextPage = currentPage + 10;

            if (nextPage < count)
            {
                pageStack.Push(nextPage);
            }
        }

        public static void PreviousPage(Stack<int> pageStack)
        {
            if (pageStack.Count > 1)
            {
                pageStack.Pop();
            }
        }


    }
}
