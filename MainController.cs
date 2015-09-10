using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Konex.AutoEmailer;
using Konex.AutoEmailer.Library;
using Konex.AutoEmailer.Properties;

namespace PageableDataGrid
{
    public class MainController
    {
        public void SaveSettings(Settings settings)
        {
            var excelReader = new ExcelReader();

        }

        public void SendEmail(Customer customer)
        {
            var emailer = new Emailer();
            emailer.SendEmail(customer.Email, customer.Name, customer.NextServiceDate);
        }

        public SortablePageableCollection<Customer> GetCustomers()
        {
            return new SortablePageableCollection<Customer>(CreateInitialContacts());
        }

        private IEnumerable<Customer> CreateInitialContacts()
        {
            var list = new List<Customer>();

            for (int i = 1; i < 90; i++)
            {
                var newContact = new Customer() { Name = "ContactName " + i.ToString("000"), Email = "address" + i.ToString("000") + "@test.com" };
                list.Add(newContact);
            }
            return list;
        }

        public void AddNewCustomer(Customer customer)
        {
            if (customer != null && !string.IsNullOrEmpty(customer.Name) && !string.IsNullOrEmpty(customer.Email))
            {
                //customer.Add(customer);
                //NewCustomer = new Customer();
            }
        }
    }
}
