using System;
using JqueryDT.Enums;

namespace JqueryDT.Models
{
    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }

        public PositionType? Position { get; set; }


        public DateTime? Hired { get; set; }

        public bool IsAdmin { get; set; }

        public decimal Salary { get; set; }
    }
}