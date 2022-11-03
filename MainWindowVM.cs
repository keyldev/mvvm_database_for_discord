using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace mvvm_database
{
    internal class MainWindowVM : INotifyPropertyChanged
    {
        // INotifyPropertyChanged implementation
        #region INPC_Region
        public event PropertyChangedEventHandler? PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] string name = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        #endregion

        #region Properties

        private ObservableCollection<Customer>? customers;

        public ObservableCollection<Customer> CustomersList
        {
            get { return customers ?? new ObservableCollection<Customer>(); } // if null -> new OC();
            set { customers = value; NotifyPropertyChanged(); }
        }

        // Binding for Button's click
        public RelayCommand? ShowSomething { get; set; }

        #endregion

        //private SqlConnection _connection = new SqlConnection("HERE YOU USE YOUR SQL CONN STRING");
        public MainWindowVM()
        {
            CustomersList = new ObservableCollection<Customer>();

            // open conn
            #region SQLConnection
            /* _connection.Open();
             string sqlQuery = "SELECT * FROM db"; // for example
             using(SqlCommand selectAllFrom = new SqlCommand(sqlQuery, _connection))
             {
                 using(SqlDataReader reader = selectAllFrom.ExecuteReader())
                 {
                     while (reader.Read())
                     {
                         Customer customer = new Customer();
                         customer.cID = (int)reader["cID"];
                         customer.cName = (string)reader["cName"];
                         CustomersList.Add(customer);
                     }
                 }
             }
             _connection.Close();*/
            #endregion
            CustomersList.Add(new Customer() { cID = 0, cName = "Example name" });

            ShowSomething = new RelayCommand(o =>
            {
                 // it's realization of button's click
            });
        }
    }
    // for binding buttons
    public class RelayCommand : ICommand
    {
        private Action<object> _execute;
        private Func<object, bool> _canExecute;

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            _execute(parameter);
        }
    }
}
