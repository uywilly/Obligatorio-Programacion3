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
    public class RepositorioFotosSQL : IRepositorioFotos
    {
        public List<Foto> FindAll()
        {
            string cadenaFindAll = "SELECT ruta FROM Foto";
            List<Foto> listaFotos = new List<Foto>();
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
                            Foto unaF = new Foto();
                            unaF.Load(reader);
                            if (unaF.Validar())
                                listaFotos.Add(unaF);
                        }
                    }
                }
            }
            return listaFotos;
        }

        public Foto FindById(int id)
        {
            string cadenaFind = "SELECT fecha_ini, fecha_fin FROM Foto WHERE id = @id";
            Foto unaF = null;
            using (SqlConnection cn = BdSQL.Conectar())
            {
                using (SqlCommand cmd = new SqlCommand(cadenaFind, cn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader != null && reader.Read())
                    {
                        unaF = new Foto();
                        unaF.Load(reader);
                    }
                }
            }
            return unaF;
        }

        public bool Add(Foto obj)
        {
            return obj != null && obj.Add();
        }

        public bool Delete(int id)
        {
            Foto f = this.FindById(id);
            return (f != null && f.Delete());

        }

        public bool Update(Foto obj)
        {
            return obj != null && obj.Update();
        }
    }
}
