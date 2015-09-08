using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows.Input;
using Konex.AutoEmailer.Library;

namespace Konex.AutoEmailer
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private Emailer _emailer;
        private ExcelReader _excelReader;
        private SortablePageableCollection<Customer> _customers;
        public SortablePageableCollection<Customer> Customers
        {
            get
            {
                return _customers;
            }
            set
            {
                if (_customers != value)
                {
                    _customers = value;
                    SendPropertyChanged(() => Customers);
                }
            }
        }

        private Customer _newCustomer;
        public Customer NewCustomer
        {
            get { return _newCustomer; }
            set
            {
                if (_newCustomer != value)
                {
                    _newCustomer = value;
                    SendPropertyChanged(() => NewCustomer);
                }
            }
        }

        public ICommand GoToNextPageCommand { get; private set; }
        public ICommand GoToPreviousPageCommand { get; private set; }
        public ICommand RemoveItemCommand { get; private set; }
        public ICommand AddNewContactCommand { get; private set; }
        public ICommand AddExcelFileCommand { get; private set; }
        public ICommand SendEmailCommand { get; private set; }

        public List<int> EntriesPerPageList
        {
            get
            {
                return new List<int>() { 5, 10, 15 };
            }
        }

        public MainWindowViewModel()
        {       
            _emailer = new Emailer();
            _excelReader = new ExcelReader();
            Customers = new SortablePageableCollection<Customer>(CreateInitialContacts());

            GoToNextPageCommand = new RelayCommand(a => Customers.GoToNextPage());
            GoToPreviousPageCommand = new RelayCommand(a => Customers.GoToPreviousPage());
            RemoveItemCommand = new RelayCommand(item => Customers.Remove(item as Customer));
            AddNewContactCommand = new RelayCommand(item => AddNewContact());
            AddExcelFileCommand = new RelayCommand(item => _excelReader.AddExcelFileCommand());
            SendEmailCommand = new RelayCommand(item => _emailer.SendEmail(item as Customer) );

            NewCustomer = new Customer();
        }

        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler PropertyChanged;
        public void SendPropertyChanged<T>(Expression<Func<T>> expression)
        {
            if (null != PropertyChanged)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(((MemberExpression)expression.Body).Member.Name));
            }
        }

        #endregion

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

        private void AddNewContact()
        {
            if (NewCustomer != null && !string.IsNullOrEmpty(NewCustomer.Name) && !string.IsNullOrEmpty(NewCustomer.Email))
            {
                Customers.Add(NewCustomer);
                NewCustomer = new Customer();
            }
        }
    }
}
