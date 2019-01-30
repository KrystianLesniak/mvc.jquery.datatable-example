using System;
using System.Collections.Generic;
using System.Linq;
using JqueryDT.Enums;
using JqueryDT.Models;

namespace JqueryDT.Repositories
{
    public class CustomerRepository
    {
        private readonly List<Customer> customers = new List<Customer>
        {
            new Customer
            {
                Id = 1,
                Name = "Jan",
                Email = "test@test.com",
                Position = PositionType.Premium,
                Hired = new DateTime(2018, 10, 23),
                IsAdmin = false,
                Salary = 2000
            },
            new Customer
            {
                Id = 2,
                Name = "Bombek",
                Email = "2test@test.com",
                Position = PositionType.Regular,
                Hired = new DateTime(2019, 10, 23),
                IsAdmin = false,
                Salary = 800
            },
            new Customer
            {
                Id = 3,
                Name = "Zbigniew",
                Email = "atest@test.com",
                Position = PositionType.Regular,
                Hired = null,
                IsAdmin = true,
                Salary = 6000
            }
        };

        public IQueryable<Customer> GetCustomers() //Mocked method that is supposed to return IQueryable from DB
        {
            return customers.AsQueryable();
        }
    }
}