using map360.Data;
using map360.Dto;
using map360.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace map360.Service.Implementations
{
    public class UserAppService : IUserAppService
    {

        private readonly IUserDbContext _context;

        public UserAppService(IUserDbContext context)
        {
            _context = context;
        }

        public User Add(User entity)
        {
            return _context.AddHybrid(entity);
        }

        public bool Delete(int id)
        {
            return _context.DeleteHybrid(id);
        }

        public List<User> GetAll()
        {
            return _context.GetAllHybrid();
        }

        public User GetById(int id)
        {
            return _context.GetByIdHybrid(id);
        }

        public User Update(User entity)
        {
            return _context.UpdateHybrid(entity);
        }

        //public UserToReturn CustomJoinById(int id)
        //{
        //    return _context.SelectWithJoinById(id);
        //}
    }
}