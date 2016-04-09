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
    public class RepositorioAnfitrionesSQL
    {
        public bool Add(Anfitrion obj)
        {
            return obj != null && obj.Add();
        }

        public bool Delete(int id)
        {
            Anfitrion a = this.FindById(id);
            return (a != null && a.Delete());

        }

        public List<Anfitrion> FindAll()
        {
            string cadenaFindAll = "SELECT * FROM Anfitrion";
            List<Anfitrion> listaAnfitriones = new List<Anfitrion>();
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
                            Anfitrion a = new Anfitrion();
                            a.Load(reader);
                            if (a.Validar())
                                listaAnfitriones.Add(a);
                        }
                    }
                }
            }
            return listaAnfitriones;
        }

        public Anfitrion FindById(int id)
        {
            string cadenaFind = "SELECT * FROM Anfitrion WHERE id=@id";
            Anfitrion a = null;
            using (SqlConnection cn = BdSQL.Conectar())
            {
                using (SqlCommand cmd = new SqlCommand(cadenaFind, cn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader != null && reader.Read())
                    {
                        a = new Anfitrion();
                        a.Load(reader);
                    }
                }
            }
            return a;
        }

        public bool Update(Anfitrion obj)
        {
            return obj != null && obj.Update();
        }
    }
}
