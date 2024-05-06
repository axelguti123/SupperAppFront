using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperApp.AccesoDatos.Interfaz
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Find(int id);
        TEntity Create(TEntity data);
        IEnumerable<TEntity> GetAll();
        void Update(TEntity data);
        void Delete(int id);
    }
}
