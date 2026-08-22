using Domain.Models;
using Services.Services;
using Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyManagementApp.Controllers
{
    public class EmployeeController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public void Create(Employee employee)
        {
            _employeeService.Create(employee);
        }

        public void Update(Employee employee)
        {
            _employeeService.Update(employee);
        }

        public void Delete(int id)
        {
            _employeeService.Delete(id);
        }

        public Employee GetById(int id)
        {
            return _employeeService.GetById(id);
        }

        public List<Employee> GetAll()
        {
            return _employeeService.GetAll();
        }

        public List<Employee> GetEmployeesByAge(int age)
        {
            return _employeeService.GetEmployeesByAge(age);
        }

        public List<Employee> GetEmployeesByDepartmentId(int departmentId)
        {
            return _employeeService.GetEmployeesByDepartmentId(departmentId);
        }

        public List<Employee> GetEmployeesByDepartmentName(string departmentName)
        {
            return _employeeService.GetEmployeesByDepartmentName(departmentName);
        }

        public List<Employee> SearchByNameOrSurname(string searchText)
        {
            return _employeeService.SearchByNameOrSurname(searchText);
        }

        public int GetAllCount()
        {
            return _employeeService.GetAllCount();
        }
    }
}