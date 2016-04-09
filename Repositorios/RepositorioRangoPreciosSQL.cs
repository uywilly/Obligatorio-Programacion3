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
    public class RepositorioRangoPreciosSQL : IRepositorioRangoPrecios
    {
        public List<RangoPrecio> FindAll()
        {
            string cadenaFindAll = "SELECT @fecha_inicio, fecha_fin, variacion_precio FROM RangoPrecio";
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
                            RangoPrecio unRP = new RangoPrecio();
                            unRP.Load(reader);
                            if (unRP.Validar())
                                listaRangos.Add(unRP);
                        }
                    }
                }
            }
            return listaRangos;
        }

        public RangoPrecio FindById(int id)
        {
            string cadenaFind = "SELECT fecha_inicio, fecha_fin, variacion_precio FROM RangoPrecio WHERE id = @id";
            RangoPrecio unRP = null;
            using (SqlConnection cn = BdSQL.Conectar())
            {
                using (SqlCommand cmd = new SqlCommand(cadenaFind, cn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader != null && reader.Read())
                    {
                        unRP = new RangoPrecio();
                        unRP.Load(reader);
                    }
                }
            }
            return unRP;
        }

        public bool Add(RangoPrecio obj)
        {
            return obj != null && obj.Add();
        }

        public bool Delete(int id)
        {
            RangoPrecio rp = this.FindById(id);
            return (rp != null && rp.Delete());

        }

        public bool Update(RangoPrecio obj)
        {
            return obj != null && obj.Update();
        }
    }
}
