using map360.Data;
using map360.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace map360.Service.Implementations
{
    public class CompanyAppService : ICompanyAppService
    {
        private readonly ICompanyDbContext _context;

        public CompanyAppService(ICompanyDbContext context)
        {
            _context = context;
        }

        public Company Add(Company entity)
        {
            return _context.AddHybrid(entity);
        }

        public bool Delete(int id)
        {
            return _context.DeleteHybrid(id);
        }

        public List<Company> GetAll()
        {
            return _context.GetAllHybrid();
        }

        public Company GetById(int id)
        {
            return _context.GetByIdHybrid(id);
        }

        public Company Update(Company entity)
        {
            return _context.UpdateHybrid(entity);
        }
    }
}