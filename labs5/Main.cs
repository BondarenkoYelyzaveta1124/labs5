using System;
using System.Text; 
namespace labs5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            bool stayInMenu = true;

            while (stayInMenu)
            {
                Console.Clear(); 
                Console.WriteLine("Меню Завдань:");
                Console.WriteLine("1. Виконати Block 1");
                Console.WriteLine("2. Виконати Block 2");
                Console.WriteLine("0. Вихід");
                Console.Write("Ваш вибір: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Некоректне введення. Введіть число.");
                }
                else
                {
                    switch (choice)
                    {
                        case 1:
                            Block1.Block11();
                            break; 

                        case 2:
                            Block2.Block22(); 
                            break; 

                        case 0:
                            stayInMenu = false; 
                            Console.WriteLine("Завершення роботи програми.");
                            break; 

                        default:
                            Console.WriteLine("Невірний пункт меню. Спробуйте ще раз.");
                            break; 
                    }
                }

                if (stayInMenu)
                {
                    Console.WriteLine("\nНатисніть Enter для повернення в меню...");
                    Console.ReadLine();
                }
            }
        }
    }
}