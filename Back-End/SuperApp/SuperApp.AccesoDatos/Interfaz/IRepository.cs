using SupperApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperApp.AccesoDatos.Interfaz
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<Response<TEntity>> Find(int id);
        Task<Response> Create(TEntity data);
        Task<Response<IEnumerable<TEntity>>> GetAll();
        Task<Response> Update(TEntity data);
        Task<Response> Delete(int id);
    }
}
