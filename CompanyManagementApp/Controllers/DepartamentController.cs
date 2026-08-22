using Domain.Models;
using Services.Services;
using Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyManagementApp.Controllers
{
    public class DepartmentController
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }


        public void Create(Departament department)
        {
            _departmentService.Create(department);
        }


        public void Update(Departament department)
        {
            _departmentService.Update(department);
        }

        public void Delete(int id)
        {
            _departmentService.Delete(id);
        }

        public Departament GetById(int id)
        {
            return _departmentService.GetById(id);
        }

        public List<Departament> GetAll()
        {
            return _departmentService.GetAll();
        }

        public List<Departament> Search(string searchText)
        {
            return _departmentService.Search(searchText);
        }

        public List<Departament> SortByCapacity()
        {
            return _departmentService.SortByCapacity();
        }
    }
}
