using System.Collections.ObjectModel;
using MasterDetailWPF.Models;

namespace MasterDetailWPF.Controllers
{
    public class MasterDetailController
    {
        public ObservableCollection<Customer> Customers { get; set; }
        public Customer SelectedCustomer { get; set; }

        public MasterDetailController()
        {
            Customers = new ObservableCollection<Customer>
            {
                new Customer { Name = "Alberto", Email = "alberto@ejemplo.com", Phone = "111222333" },
                new Customer { Name = "Bob", Email = "maría@ejemplo.com", Phone = "222333444" },
                new Customer { Name = "Charlie", Email = "carla@ejemplo.com", Phone = "333444555" }
            };
        }
    }
}
