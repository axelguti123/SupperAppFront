using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Interfaz
{
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        void Create(TEntity entity);
        TEntity FindById(int id);
        void Update(TEntity entity);
        void Delete(int id);

    }
}
