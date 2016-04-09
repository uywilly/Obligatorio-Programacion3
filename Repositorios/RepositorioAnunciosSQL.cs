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
    public class RepositorioAnunciosSQL
    {
        public bool Add(Anuncio obj)
        {
            return obj != null && obj.Add();
        }

        public bool Delete(int id)
        {
            Anuncio a = this.FindById(id);
            return (a != null && a.Delete());

        }

        public List<Anuncio> FindAll()
        {
            string cadenaFindAll = "SELECT id,publicado,nombre,descripcion FROM Anuncio";
            List<Anuncio> listaAnuncios = new List<Anuncio>();
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
                            Anuncio a = new Anuncio();
                            a.Load(reader);
                            if (a.Validar())
                                listaAnuncios.Add(a);
                        }
                    }
                }
            }
            return listaAnuncios;
        }

        public Anuncio FindById(int id)
        {
            string cadenaFind = "SELECT id,publicado,nombre,descripcion FROM Anuncio WHERE id=@id";
            Anuncio a = null;
            using (SqlConnection cn = BdSQL.Conectar())
            {
                using (SqlCommand cmd = new SqlCommand(cadenaFind, cn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader != null && reader.Read())
                    {
                        a = new Anuncio();
                        a.Load(reader);
                    }
                }
            }
            return a;
        }

        public bool Update(Anuncio obj)
        {
            return obj != null && obj.Update();
        }
    }
}
