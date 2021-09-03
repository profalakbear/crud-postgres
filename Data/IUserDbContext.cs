using map360.Dto;
using map360.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace map360.Data
{
    public interface IUserDbContext : IDbContext<User>
    {
        List<User> GetAllHybrid(); 
        User GetByIdHybrid(int id);
        User AddHybrid(User user);
        bool DeleteHybrid(int id);
        User UpdateHybrid(User user);
        //UserToReturn SelectWithJoinById(int id);
    }
}
