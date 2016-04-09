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
    public class RepositorioAlojamientosSQL
    {
        public bool Add(Alojamiento obj)
        {
            return obj != null && obj.Add();
        }

        public bool Delete(int id)
        {
            Alojamiento a = this.FindById(id);
            return (a != null && a.Delete());

        }

        public List<Alojamiento> FindAll()
        {
            string cadenaFindAll = "SELECT id,tipo,cupo_max FROM Alojamiento";
            List<Alojamiento> listaAlojamientos = new List<Alojamiento>();
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
                            Alojamiento a = new Alojamiento();
                            a.Load(reader);
                            if (a.Validar())
                                listaAlojamientos.Add(a);
                        }
                    }
                }
            }
            return listaAlojamientos;
        }

        public Alojamiento FindById(int id)
        {
            string cadenaFind = "SELECT id,tipo,cupo_max FROM Alojamiento WHERE id=@id";
            Alojamiento a = null;
            using (SqlConnection cn = BdSQL.Conectar())
            {
                using (SqlCommand cmd = new SqlCommand(cadenaFind, cn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader != null && reader.Read())
                    {
                        a = new Alojamiento();
                        a.Load(reader);
                    }
                }
            }
            return a;
        }

        public bool Update(Alojamiento obj)
        {
            return obj != null && obj.Update();
        }
    }
}
