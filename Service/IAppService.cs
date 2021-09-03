using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace map360.Service
{
    public interface IAppService<T>
    {
        List<T> GetAll();
        T GetById(int id);
        T Add(T entity);
        bool Delete(int id);
        T Update(T entity);
    }
}
