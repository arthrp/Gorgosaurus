using Gorgosaurus.BO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorgosaurus.DA.Repositories
{
    interface IBaseRepository<T> where T : BaseEntity
    {
        T Get(long id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(long id);
    }
}
