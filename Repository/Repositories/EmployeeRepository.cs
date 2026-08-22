using Domain.Models;
using Repository.Data;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Repository.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context) : base(context)
        {

        }

        public int GetAllCount()
        {
            return _context.Employees.Count();
        }

        public List<Employee> GetEmployeesByAge(int age)
        {
            return _context.Employees.Where(m => m.Age == age).ToList();
        }

        public List<Employee> GetEmployeesByDepartmentId(int departmentId)
        {
            return _context.Employees.Where(m => m.Department.Id == departmentId).ToList();
        }

        public List<Employee> GetEmployeesByDepartmentName(string departmentName)
        {
            return _context.Employees.Where(m => m.Department.Name.Contains(departmentName)).ToList();
        }

        public List<Employee> SearchByNameOrSurname(string searchText)
        {
            return _context.Employees.Where(m => m.Name.Contains(searchText) || m.Surname.Contains(searchText)).ToList();
        }
    }
}

