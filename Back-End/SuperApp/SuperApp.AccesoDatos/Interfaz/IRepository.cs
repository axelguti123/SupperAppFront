using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperApp.AccesoDatos.Interfaz
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Find(int id);
        Task<string> Create(TEntity data);
        Task<IEnumerable<TEntity>> GetAll();
        Task<string> Update(TEntity data);
        Task<string> Delete(int id);
    }
}
