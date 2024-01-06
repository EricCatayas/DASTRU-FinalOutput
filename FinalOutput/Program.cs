using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalOutput
{
    internal class Program
    {
        static void Main(string[] args)
        {

            const int WINDOW_HEIGHT = 40;
            const int WINDOW_WIDTH = 98;
            static void Main(string[] args)
            {
                while (true)
                {
                    Console.SetWindowSize(WINDOW_WIDTH, WINDOW_HEIGHT);
                    Console.OutputEncoding = System.Text.Encoding.UTF8;

                    Console.Clear();


                    Console.ForegroundColor = ConsoleColor.Cyan;

                    Menu.StartAuthorization();
                }


            }

        }
    }


}
