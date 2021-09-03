using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using map360.Models;

namespace map360.Data
{
    public interface ICompanyDbContext : IDbContext<Company>
    {
        List<Company> GetAllHybrid();
        Company GetByIdHybrid(int id);
        Company AddHybrid(Company company);
        bool DeleteHybrid(int id);
        Company UpdateHybrid(Company Company);
    }
}
