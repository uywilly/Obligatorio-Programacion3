using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.EntidadesNegocio;

namespace Dominio.InterfacesRepositorio
{
    public interface IRepositorio<T> where T :IEntity
    {
        T FindById(int id);
        List<T> FindAll();
    }
}
