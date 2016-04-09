using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Dominio.EntidadesNegocio;
using Dominio.InterfacesRepositorio;
using System.Data.SqlClient;
using Utilidades;

namespace Repositorios
{
    public class RepositorioServiciosSQL : IRepositorioServicios
    {
        public List<Servicio> FindAll()
        {
            string cadenaFindAll = "SELECT nombre, descripcion, activo FROM Servicio";
            List<Servicio> listaServicios = new List<Servicio>();
            using (SqlConnection cn = BdSQL.Conectar())
            {
                using (SqlCommand cmd = new SqlCommand(cadenaFindAll, cn))
                {
                    cn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            Servicio unS = new Servicio();
                            unS.Load(reader);
                            if (unS.Validar())
                                listaServicios.Add(unS);
                        }
                    }
                }
            }
            return listaServicios;
        }

        public Servicio FindById(int id)
        {
            string cadenaFind = "SELECT nombre, descripcion, activo FROM Servicio WHERE id = @id";
            Servicio unS = null;
            using (SqlConnection cn = BdSQL.Conectar())
            {
                using (SqlCommand cmd = new SqlCommand(cadenaFind, cn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader != null && reader.Read())
                    {
                        unS = new Servicio();
                        unS.Load(reader);
                    }
                }
            }
            return unS;
        }

        public bool Add(Servicio obj)
        {
            return obj != null && obj.Add();
        }

        public bool Delete(int id)
        {
            Servicio s = this.FindById(id);
            return (s != null && s.Delete());

        }

        public bool Update(Servicio obj)
        {
            return obj != null && obj.Update();
        }
    }
}
