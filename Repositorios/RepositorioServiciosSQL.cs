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
        List<Servicio> IRepositorio<Servicio>.FindAll()
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
                                listaServicios.Add(unaU);
                        }
                    }
                }
            }
            return listaServicios;
        }

        Servicio IRepositorio<Servicio>.FindById(int id)
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
                        unS = new Ubicacion();
                        unS.Load(reader);
                    }
                }
            }
            return unS;
        }
    }
}
