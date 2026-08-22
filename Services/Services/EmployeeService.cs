using Domain.Models;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        private readonly IEmployeeRepository _context;

        public EmployeeService(IEmployeeRepository employeeRepository) : base(employeeRepository)
        {
            _context = employeeRepository;
        }
        public override void Create(Employee employee)
        {
            if (employee.Age < 18 || employee.Age > 65)
            {
                throw new System.Exception("Employee age 18 ile 65 arasinda olmalidir.");

            }

            base.Create(employee);
        }

        public int GetAllCount()
        {
            return _context.GetAllCount();
        }

        public List<Employee> GetEmployeesByAge(int age)
        {
            return _context.GetEmployeesByAge(age);
        }

        public List<Employee> GetEmployeesByDepartmentId(int departmentId)
        {
            return _context.GetEmployeesByDepartmentId(departmentId);
        }

        public List<Employee> GetEmployeesByDepartmentName(string departmentName)
        {
            return _context.GetEmployeesByDepartmentName(departmentName);
        }
        

        public List<Employee> SearchByNameOrSurname(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return GetAll();
            }

            return _context.SearchByNameOrSurname(searchText.Trim());
        }
    }
}
