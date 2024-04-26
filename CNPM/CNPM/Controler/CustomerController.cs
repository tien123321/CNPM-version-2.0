using CNPM.Model;
using CNPM.Model.ClassData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNPM.Controler
{
    internal class CustomerController
    {
        private string username, address, phone;
        private CustomerModel _customerModel;
        public CustomerController()
        {
            this._customerModel = new CustomerModel();
        }
        public List<Custemer> searchCustomers(Custemer filterCustomer)
        {
            return this._customerModel.find(filterCustomer);
        }
        public List<Custemer> allCustomers()
        {
            return this._customerModel.findAll();
        }
        public bool isExistCustomer(Custemer customer)
        {
            return this._customerModel.exist(customer);
        }
        public Custemer createCustomer(Custemer customer)
        {
            bool isValid = customer.requiredFields();
            if (isValid)
            {
                return this._customerModel.create(customer);
            }
            return new Custemer(1, "", "", "");
        }
        public Custemer updateCustomer(Custemer customer)
        {
            bool isValid = customer.requiredFields();
            if (isValid)
            {
                return this._customerModel.update(customer);
            }
            return new Custemer(1, "", "", "");
        }
        public Custemer deleteCustomer(Custemer customer)
        {
            return this._customerModel.delete(customer);
        }
    }
   
}
