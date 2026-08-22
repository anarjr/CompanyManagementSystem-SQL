using Domain.Models;
using Repository.Data;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repositories
{
    public class DepartmentRepository : BaseRepository<Departament>, IDepartmentRepository
    {
        public DepartmentRepository(AppDbContext context) : base(context)
        {

        }

        public List<Departament> Search(string searchText)
        {
            return _context.Departments.Where(m => m.Name.Contains(searchText)).ToList();

        }

        public List<Departament> SortByCapacity()
        {
            return _context.Departments.OrderBy(d => d.Capacity).ToList();


        }
    }
}


