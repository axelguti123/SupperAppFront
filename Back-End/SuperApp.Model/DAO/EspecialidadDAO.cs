using Microsoft.EntityFrameworkCore;
using Model.Interfaz;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    internal class EspecialidadDAO : IEspecialidad
    {
        private readonly DbObraContext _context;

        public EspecialidadDAO(DbObraContext context)
        {
            _context = context;
        }
        public void Create(TblEspecialidad entity)
        {
            _context.Database.ExecuteSqlRaw("")
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public TblEspecialidad FindById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TblEspecialidad> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(TblEspecialidad entity)
        {
            throw new NotImplementedException();
        }
    }
}
