using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_1_1
{
    public enum Currencies
    {
        TWD,
        USD,
        CNY,
        JPY
    }

    public class Product
    {
        public string name;
        public float priceTWD;

        public Product(string name, float priceTWD)
        {
            this.name = name;
            this.priceTWD = priceTWD;
        }

        public float GetPrice(Currencies currency)
        {
            switch (currency)
            {
                case Currencies.TWD:
                    return priceTWD;
                case Currencies.USD:
                    return priceTWD * 0.031f;
                case Currencies.CNY:
                    return priceTWD * 0.23f;
                case Currencies.JPY:
                    return priceTWD * 4.59f;
                default:
                    throw new ArgumentException(string.Format("Currency '{0}' not supported.", currency));
            }
        }

        public string GetCurrencyTaggedPrice(Currencies currency)
        {
            string priceString = string.Format("({0}) {1}", currency.ToString(), GetPrice(currency));
            return priceString;
        }
    }

    public class Order
    {
        public Product product;
        public int amount;

        public Order(Product product, int amount)
        {
            this.product = product;
            this.amount = amount;
        }

        public float GetSubtotal(Currencies currency)
        {
            float subtotal = product.GetPrice(currency) * amount;
            return subtotal;
        }
    }

    public class ShoppingCart
    {
        public List<Order> items = new List<Order>();

        public ShoppingCart() { }

        public bool Add(Product product, int amount)
        {
            if (items.Count <= 0)
                return false;
            else
            {
                foreach (Order item in items)
                {
                    if (item.product == product)
                    {
                        item.amount += amount;
                        return true;
                    }
                }
                Order newOrder = new Order(product, amount);
                items.Add(newOrder);
                return true;
            }
        }

        public bool Remove(Product product, int amount)
        {
            if (items.Count <= 0)
                return false;
            else
            {
                foreach (Order item in items)
                {
                    if (item.product == product)
                    {
                        if (item.amount < amount)
                            return false;
                        item.amount -= amount;
                        return true;
                    }
                }
                return false;
            }
        }

        public float CalculateOrderTotal(Currencies currency)
        {
            float total = 0;
            foreach (Order item in items)
            {
                total += item.product.GetPrice(currency) * item.amount;
            }
            return total;
        }

        public void DisplayShoppingCart(Currencies currency)
        {
            Console.WriteLine();
            Console.WriteLine("Contents in the Shopping Cart: ");
            Console.WriteLine("========================================================================================");
            Console.WriteLine($"| {"Product Names",40} | {"Products Price",15} | {"Amount",10} | {"Subtotal",10} |");
            Console.WriteLine("========================================================================================");
            foreach (Order order in items)
            {
                Console.WriteLine($"| {order.product.name,40} | {order.product.GetCurrencyTaggedPrice(currency),15} | {order.amount,10} | {order.GetSubtotal(currency),10} |");
            }
            Console.WriteLine("========================================================================================");
            Console.WriteLine();
        }

        public void DisplayTotalOrder(Currencies currency)
        {
            Console.WriteLine();
            Console.WriteLine("Total Order: ");
            Console.WriteLine("========================================================================================");
            Console.WriteLine($"| {"Product Names",40} | {"Products Price",15} | {"Amount",10} | {"Subtotal",10} |");
            Console.WriteLine("========================================================================================");
            foreach (Order order in items)
            {
                if (order.amount == 0) continue;
                Console.WriteLine($"| {order.product.name,40} | {order.product.GetCurrencyTaggedPrice(currency),15} | {order.amount,10} | {order.GetSubtotal(currency),10} |");
            }
            Console.WriteLine("========================================================================================");
            Console.WriteLine();
            Console.WriteLine($"Total: ({currency}) {CalculateOrderTotal(currency)}");
            Console.WriteLine();
        }
    }

    internal class Program
    {
        static private List<Product> productList = new List<Product>();
        static private ShoppingCart shoppingCart = new ShoppingCart();

        static private Currencies activeCurrency = Currencies.TWD;

        static private bool programEnded = false;

        static void Main(string[] args)
        {
            Initialize();

            int userAction;

            while (!programEnded)
            {
                Console.WriteLine("(1) Product List (2) Add to Cart (3) Remove from Cart (4) View Cart (5) Calculate Total Amount (6) Leave Website");
                Console.Write("Select action by entering number: ");

                try
                {
                    userAction = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine();
                    Console.WriteLine("Input action invalid! Please re-enter your action number!");
                    Console.WriteLine();
                    continue;
                }

                switch (userAction)
                {
                    case 1:
                        ProductList();
                        break;
                    case 2:
                        AddToCart();
                        break;
                    case 3:
                        RemoveFromCart();
                        break;
                    case 4:
                        ViewCart();
                        break;
                    case 5:
                        CalculateTotalAmount();
                        break;
                    case 6:
                        LeaveWebsite();
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Input action invalid! Please re-enter your action number!");
                        Console.WriteLine();
                        continue;
                }

            }
        }

        static private void Initialize()
        {
            Product prod1 = new Product("Underwater Camera Anti-loss Lanyard", 199.0f);
            Product prod2 = new Product("Diving Weight Belt", 460.0f);
            Product prod3 = new Product("Diving Operations Compass", 1100.0f);

            productList.Add(prod1);
            productList.Add(prod2);
            productList.Add(prod3);

            shoppingCart.items.Add(new Order(prod1, 0));
            shoppingCart.items.Add(new Order(prod2, 0));
            shoppingCart.items.Add(new Order(prod3, 0));
        }

        static private void ProductList()
        {
            Console.WriteLine("");
            Console.WriteLine("Product List:");
            Console.WriteLine("==============================================================");
            Console.WriteLine($"| {"Product Names",40} | {"Product Price",15} |");
            Console.WriteLine("==============================================================");
            for (int i = 0; i < productList.Count; i++)
            {
                Console.WriteLine($"| {productList[i].name,40} | {productList[i].GetCurrencyTaggedPrice(activeCurrency),15} |");
            }
            Console.WriteLine("==============================================================");
            Console.WriteLine("");
        }

        static private void AddToCart()
        {
            Product product;
            int productIndex;
            int productAmount;
            bool actionFinished = false;

            Console.WriteLine();
            for (int i = 0; i < productList.Count; i++)
            {
                Console.Write($"({i + 1}) {productList[i].name} ");
            }
            Console.WriteLine();

            do
            {
                Console.Write("Enter Product Number: ");
                try
                {
                    productIndex = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Invalid input! Please re-enter!");
                    Console.WriteLine();
                    continue;
                }

                try
                {
                    product = productList[productIndex - 1];
                }
                catch
                {
                    Console.WriteLine("Invalid input! Please re-enter!");
                    Console.WriteLine();
                    continue;
                }

                Console.Write("Enter Amount to Add: ");
                try
                {
                    productAmount = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Invalid input! Please re-enter!");
                    Console.WriteLine();
                    continue;
                }

                actionFinished = shoppingCart.Add(product, productAmount);
                if (!actionFinished)
                {
                    Console.WriteLine("Invalid input! Please re-enter!");
                    Console.WriteLine();
                    continue;
                }

            } while (!actionFinished);
            Console.WriteLine();
            Console.WriteLine("Item successfully added!");
            Console.WriteLine();
        }

        static private void RemoveFromCart()
        {
            Product product;
            int productIndex;
            int productAmount;
            bool actionFinished = false;

            Console.WriteLine();
            shoppingCart.DisplayShoppingCart(activeCurrency);
            Console.WriteLine();
            for (int i = 0; i < productList.Count; i++)
            {
                Console.Write($"({i + 1}) {productList[i].name} ");
            }
            Console.WriteLine();

            do
            {
                Console.Write("Enter Product Number: ");
                try
                {
                    productIndex = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Invalid input! Please re-enter!");
                    Console.WriteLine();
                    continue;
                }

                try
                {
                    product = productList[productIndex - 1];
                }
                catch
                {
                    Console.WriteLine("Invalid input! Please re-enter!");
                    Console.WriteLine();
                    continue;
                }

                Console.Write("Enter Amount to Add: ");
                try
                {
                    productAmount = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Invalid input! Please re-enter!");
                    Console.WriteLine();
                    continue;
                }

                actionFinished = shoppingCart.Remove(product, productAmount);
                if (!actionFinished)
                {
                    Console.WriteLine("Invalid input! Please re-enter!");
                    Console.WriteLine();
                    continue;
                }

            } while (!actionFinished);
            Console.WriteLine();
            Console.WriteLine("Item successfully removed!");
            Console.WriteLine();
        }

        static private void ViewCart()
        {
            shoppingCart.DisplayShoppingCart(activeCurrency);
        }

        static private void CalculateTotalAmount()
        {
            shoppingCart.DisplayTotalOrder(activeCurrency);
        }

        static private void LeaveWebsite()
        {
            programEnded = true;
        }
    }

}

