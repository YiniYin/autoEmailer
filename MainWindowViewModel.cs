using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows.Input;
using Konex.AutoEmailer.Library;
using PageableDataGrid;

namespace Konex.AutoEmailer
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private MainController controller;

        private SortablePageableCollection<Customer> _customers;
        public SortablePageableCollection<Customer> Customers
        {
            get
            {
                return _customers;
            }
            set
            {
                if (_customers == value) return;
                _customers = value;
                SendPropertyChanged(() => Customers);
            }
        }

        private Customer _newCustomer;
        public Customer NewCustomer
        {
            get { return _newCustomer; }
            set
            {
                if (_newCustomer == value) return;
                _newCustomer = value;
                SendPropertyChanged(() => NewCustomer);
            }
        }

        private Settings _settings;
        public Settings Settings
        {
            get { return _settings; }
            set
            {
                if (_settings == value) return;
                _settings = value;
                SendPropertyChanged(() => Settings);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void SendPropertyChanged<T>(Expression<Func<T>> expression)
        {
            if (null != PropertyChanged)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(((MemberExpression)expression.Body).Member.Name));
            }
        }

        public ICommand GoToNextPageCommand { get; private set; }
        public ICommand GoToPreviousPageCommand { get; private set; }
        public ICommand RemoveItemCommand { get; private set; }
        public ICommand AddNewCustomerCommand { get; private set; }
        public ICommand SaveSettingsCommand { get; private set; }
        public ICommand SendEmailCommand { get; private set; }

        public List<int> EntriesPerPageList
        {
            get
            {
                return new List<int> { 5, 10, 15 };
            }
        }

        public MainWindowViewModel()
        {  
            controller = new MainController();

            GetSettings();
            GetCustomers();
            GetNewCustomerViewModel();
            SetupCommands();
        }

        private void GetSettings()
        {
            Settings = controller.GetSettings();
        }

        private void GetNewCustomerViewModel()
        {
            NewCustomer = new Customer();
        }

        private void GetCustomers()
        {
            Customers = controller.GetCustomers();
        }

        private void SetupCommands()
        {
            GoToNextPageCommand = new RelayCommand(a => Customers.GoToNextPage());
            GoToPreviousPageCommand = new RelayCommand(a => Customers.GoToPreviousPage());
            RemoveItemCommand = new RelayCommand(item => Customers.Remove(item as Customer));
            AddNewCustomerCommand = new RelayCommand(item => controller.AddNewCustomer(item as Customer));
            SaveSettingsCommand = new RelayCommand(item => controller.SaveSettings(item as Settings));
            SendEmailCommand = new RelayCommand(item => controller.SendEmail(item as Customer));
        }
    }
}
