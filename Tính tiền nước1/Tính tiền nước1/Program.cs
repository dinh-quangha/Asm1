using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tính_tiền_nước1
{
    internal class Program
    {
        static List<Customer> customers = new List<Customer>();

        static void Main(string[] args)
        {
            while (true)
            {
                Menu();
                string choice = GetChoice();
                if (choice == "5")
                {
                    break;
                }

                string name = Name();

                int num1 = Number("Enter water usage last month: ");
                Console.WriteLine("Water usage last month: " + num1 + "m³");
                int num2 = Number("Enter water usage this month: ");
                Console.WriteLine("Water usage this month: " + num2 + "m³");

                if (num1 >= num2)
                {
                    Console.WriteLine("Invalid input. Last month's water usage should be less than this month's. Please try again.");
                    continue;
                }

                int waterConsumption = num2 - num1;
                double m3 = 0;
                if (choice == "4")
                {
                    int numpeople = Number("Enter number of people: ");
                    m3 = (double)waterConsumption / numpeople;
                    Console.WriteLine("The average amount of water used by each person is: " + m3 + "m³");
                }
                Console.WriteLine("Water consumption is: " + waterConsumption + "m³");

                double waterPrice = GetWaterPrice(choice, waterConsumption);
                Console.WriteLine("Total water bill: " + waterPrice.ToString("0.000") + " VND");
                double environmentFee = waterPrice * 0.1;
                Console.WriteLine("Environment Fee is: " + environmentFee.ToString("0.000") + " VND");

                double VAT = waterPrice * 0.1;
                Console.WriteLine("VAT is: " + VAT.ToString("0.000") + " VND");

                double totalBill = waterPrice + VAT + environmentFee;
                Console.WriteLine("Total bill is: " + totalBill.ToString("0.000") + " VND");


                Customer customer = new Customer(name, totalBill, choice);
                customers.Add(customer);

                Console.WriteLine("Please press Enter key to continue");
                Console.ReadLine();
                Console.Clear();
            }


            DisplayCustomers();
        }

        static void Menu()
        {
            Console.WriteLine("----- Menu -----");
            Console.WriteLine("1. Business services");
            Console.WriteLine("2. Administrative agency, public services");
            Console.WriteLine("3. Production units");
            Console.WriteLine("4. Household");
            Console.WriteLine("5. Exit");
        }

        static string GetChoice()
        {
            Console.WriteLine("Enter customer type: ");
            string choice = Console.ReadLine();

            while (choice != "1" && choice != "2" && choice != "3" && choice != "4" && choice != "5")
            {
                Console.WriteLine("Invalid choice. Please enter a valid option.");
                choice = Console.ReadLine();
            }

            return choice;
        }

        static string Name()
        {
            Console.WriteLine("Enter customer name: ");
            string name = Console.ReadLine();
            return name;
        }

        static int Number(string message)
        {
            Console.Write(message);
            int num = int.Parse(Console.ReadLine());
            return num;
        }

        static double GetWaterPrice(string choice, int waterConsumption)
        {
            double waterPrice = 0;
            switch (choice)
            {
                case "1":
                    waterPrice = waterConsumption * 22.068;
                    break;
                case "2":
                    waterPrice = waterConsumption * 9.955;
                    break;
                case "3":
                    waterPrice = waterConsumption * 11.615;
                    break;
                case "4":
                    if (waterConsumption <= 10)
                    {
                        waterPrice = 5.973 * waterConsumption;
                    }
                    else if (waterConsumption <= 20)
                    {
                        waterPrice = (10 * 5.973) + (waterConsumption - 10) * 7.052;
                    }
                    else if (waterConsumption <= 30)
                    {
                        waterPrice = (10 * 5.973) + (10 * 7.052) + (waterConsumption - 20) * 8.699;
                    }
                    else
                    {
                        waterPrice = (10 * 5.973) + (10 * 7.052) + (10 * 8.699) + (waterConsumption - 30) * 15.929;
                    }
                    break;
                case "5":
                    Console.WriteLine("Exiting...");
                    Environment.Exit(0);
                    break;
            }
            return waterPrice;
        }

        static void DisplayCustomers()
        {
            Console.WriteLine("----- Customer Details -----");
            foreach (var customer in customers)
            {
                Console.WriteLine(customer);
            }
        }
    }

    class Customer
    {
        public string Name { get; set; }

        public double TotalBill { get; set; }
        public string CustomerType { get; set; }


        public Customer(string name, double totalBill, string customerType)
        {
            Name = name;
            TotalBill = totalBill;
            CustomerType = customerType;

        }

        public override string ToString()
        {
            return $"Name: {Name} " +
                   $"Total Bill: {TotalBill} VND, " +
                   $"Customer Type: {CustomerType}";
        }
    }
}
