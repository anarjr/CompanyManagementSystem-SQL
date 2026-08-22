using Domain.Entities;
using Repository.Repositories.Interfaces;
using Services.Exception;
using Services.Helpers;
using Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Services
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        protected readonly IBaseRepository<T> _context;

        public BaseService(IBaseRepository<T> repository)
        {
            _context = repository;
        }

        public virtual void Create(T entity)
        {
            _context.Create(entity);
        }

        public virtual void Update(T entity)
        {
            T existData = GetById(entity.Id);
            _context.Update(entity);
        }

        public virtual void Delete(int id)
        {
            T existData = GetById(id);
            _context.Delete(existData);
        }

        public virtual T GetById(int id)
        {
            T data = _context.GetById(id);

            if (data == null)
            {
                throw new NotFoundException(ValidationMessages.EmployeeNotFound);
            }

            return data;
        }

        public virtual List<T> GetAll()
        {
            return _context.GetAll();
        }
    }
}
