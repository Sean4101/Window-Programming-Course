using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_1_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int userAction;
            bool programEnded = false;

            while (!programEnded)
            {
                Console.WriteLine("(1) Product List (2) Add to Cart (3) Remove from Cart (4) View Cart (5) Calculate Total Amount (6) Leave Website");
                Console.WriteLine("Select action by entering number:");

                userAction = int.Parse(Console.ReadLine());

                switch (userAction)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        programEnded = true;
                        break;
                    default:
                        break;
                }

            }
        }

        private void ProductList()
        {

        }

        private void AddToCart()
        {

        }

        private void RemoveFromCart()
        {

        }

        private void ViewCart()
        {

        }

        private void CalculateTotalAmount()
        {

        }

        private void LeaveWebsite()
        {

        }
    }
}
