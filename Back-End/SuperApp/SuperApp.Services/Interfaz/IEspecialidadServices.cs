using SuperApp.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperApp.Services.Interfaz
{
    public interface IEspecialidadServices
    {
        IEnumerable<MostrarEspecialidadDTO> GetAll();
    }
}
