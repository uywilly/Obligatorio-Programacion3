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
    public class RepositoriosRangoPreciosSQL : IRepositorioRangoPrecios
    {
        public List<RangoPrecio> FindAll()
        {
            string cadenaFindAll = "SELECT fecha_ini, fecha_fin, variacion_precio FROM RangoPrecio";
            List<RangoPrecio> listaRangos = new List<RangoPrecio>();
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
                            RangoPrecio unR = new RangoPrecio();
                            unR.Load(reader);
                            if (unR.Validar())
                                listaRangos.Add(unR);
                        }
                    }
                }
            }
            return listaRangos;
        }

        public RangoPrecio FindById(int id)
        {
            string cadenaFind = "SELECT fecha_ini, fecha_fin, variacion_precio FROM RangoPrecio WHERE id = @id";
            RangoPrecio unR = null;
            using (SqlConnection cn = BdSQL.Conectar())
            {
                using (SqlCommand cmd = new SqlCommand(cadenaFind, cn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader != null && reader.Read())
                    {
                        unR = new RangoPrecio();
                        unR.Load(reader);
                    }
                }
            }
            return unR;
        }
    }
}
