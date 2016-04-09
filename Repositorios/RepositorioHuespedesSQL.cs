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
    public class RepositorioHuespedesSQL
    {
        public List<Huesped> FindAll()
        {
            string cadenaFindAll = "SELECT fecha_ini, fecha_fin FROM Reserva";
            List<Huesped> listaHuespedes = new List<Huesped>();
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
                            Huesped unH = new Huesped();
                            unH.Load(reader);
                            if (unH.Validar())
                                listaHuespedes.Add(unH);
                        }
                    }
                }
            }
            return listaHuespedes;
        }

        public Huesped FindById(int id)
        {
            string cadenaFind = "SELECT fecha_ini, fecha_fin FROM Reserva WHERE id = @id";
            Huesped unH = null;
            using (SqlConnection cn = BdSQL.Conectar())
            {
                using (SqlCommand cmd = new SqlCommand(cadenaFind, cn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader != null && reader.Read())
                    {
                        unH = new Huesped();
                        unH.Load(reader);
                    }
                }
            }
            return unH;
        }

        public bool Add(Huesped obj)
        {
            return obj != null && obj.Add();
        }

        public bool Delete(int id)
        {
            Huesped h = this.FindById(id);
            return (h != null && h.Delete());

        }

        public bool Update(Huesped obj)
        {
            return obj != null && obj.Update();
        }
    }
}
