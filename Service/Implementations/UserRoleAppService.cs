using map360.Data;
using map360.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace map360.Service.Implementations
{
    public class UserRoleAppService : IUserRoleAppService
    {
        private readonly IUserRoleDbContext _context;
        public UserRoleAppService(IUserRoleDbContext context)
        {
            _context = context;
        }
        
        public UserRole Add(UserRole entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<UserRole> GetAll()
        {
           return _context.GetAll();
        }

        public UserRole GetById(int id)
        {
            throw new NotImplementedException();
        }

        public UserRole Update(UserRole entity)
        {
            throw new NotImplementedException();
        }
    }
}