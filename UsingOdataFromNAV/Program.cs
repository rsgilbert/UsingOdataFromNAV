using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAV;

namespace UsingOdataFromNAV
{
    class Program
    {

        static void Main(string[] args)
        {
            string urlWithCompany = "http://localhost:7068/DynamicsNAV90/OData/Company('CRONUS%20International%20Ltd.')";
            NAV.NAV nav = new NAV.NAV(new Uri(urlWithCompany));
            // Authenticate users on their Windows Credentials
            nav.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;

            print("** Printing list of current customers **");
            PrintCustomers(nav, "C");

            // Adding new customer
            PageWithCapitalization cust = new PageWithCapitalization();
            cust.Name = "Clean Gilbo Estates";
            nav.AddToPageWithCapitalization(cust);
            nav.SaveChanges();

            print("** Printing list after creating a customer **");
            PrintCustomers(nav, "C");

            // Modify customer
            cust.Name += " Changed";
            cust.Address = "Salaama";
            nav.UpdateObject(cust);
            nav.SaveChanges();

            print("Printing list after updating a customer **");
            PrintCustomers(nav, "C");

            // Pause console
            Console.ReadLine();

        }

        /**
         * Method to print customers whose name starts with Cust
         */
        private static void PrintCustomers(NAV.NAV nav, string namePrefix)
        {
            // PageWithCustomization is linked to the Customer Card page
            var customers = from c in nav.PageWithCapitalization
                            where c.Name.StartsWith(namePrefix)
                            select c;
            Boolean customerFound = false;
            foreach(PageWithCapitalization customer in customers)
            {
                customerFound = true;
                Console.WriteLine("No: {0}, Name: {1}", customer.No, customer.Name);
            }
            if(!customerFound)
            {
                print("Failed to find customers with name starting with " + namePrefix);
            }
        }

        private static void print(String s)
        {
            Console.WriteLine(s);
        }
    }
}
