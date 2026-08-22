using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
using Domain.Models;


namespace Domain.Models
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public int? DepartmentId { get; set; }
        public Departament Department { get; set; }
    }
}
