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


        //public static int GetPage()
        //{
        //    string pagePath = GetFilePath.TextFilePath(@"PageHandler", "pages");

        //    StreamReader sr = new StreamReader(pagePath);
        //    int page = int.Parse(sr.ReadToEnd());
        //    sr.Close();
        //    sr.Dispose();
        //    return page;

        //}

        //public static void UpdatePage(int page)
        //{
        //    string pagePath = GetFilePath.TextFilePath(@"PageHandler", "pages");

        //    FileManager.WriteTextFile(pagePath, page.ToString());
        //}

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
