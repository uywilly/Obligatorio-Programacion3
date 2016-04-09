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
    public class RepositorioRangoFechasSQL : IRepositorioRangoFechas
    {
        public List<RangoFechas> FindAll()
        {
            string cadenaFindAll = "SELECT fecha_ini, fecha_fin FROM RangoFechas";
            List<RangoFechas> listaRangoFechass = new List<RangoFechas>();
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
                            RangoFechas unRF = new RangoFechas();
                            unRF.Load(reader);
                            if (unRF.Validar())
                                listaRangoFechass.Add(unRF);
                        }
                    }
                }
            }
            return listaRangoFechass;
        }

        public RangoFechas FindById(int id)
        {
            string cadenaFind = "SELECT fecha_ini, fecha_fin FROM RangoFechas WHERE id = @id";
            RangoFechas unRF = null;
            using (SqlConnection cn = BdSQL.Conectar())
            {
                using (SqlCommand cmd = new SqlCommand(cadenaFind, cn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader != null && reader.Read())
                    {
                        unRF = new RangoFechas();
                        unRF.Load(reader);
                    }
                }
            }
            return unRF;
        }

        public bool Add(RangoFechas obj)
        {
            return obj != null && obj.Add();
        }

        public bool Delete(int id)
        {
            RangoFechas rf = this.FindById(id);
            return (rf != null && rf.Delete());

        }

        public bool Update(RangoFechas obj)
        {
            return obj != null && obj.Update();
        }
    }
}
