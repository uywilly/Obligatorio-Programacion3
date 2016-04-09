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
    public class RepositorioRolesSistemaSQL
    {
        public List<RolesSistema> FindAll()
        {
            string cadenaFindAll = "SELECT id, nombre FROM RolesSistema";
            List<RolesSistema> listaRolesSistemas = new List<RolesSistema>();
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
                            RolesSistema unRS = new RolesSistema();
                            unRS.Load(reader);
                            if (unRS.Validar())
                                listaRolesSistemas.Add(unRS);
                        }
                    }
                }
            }
            return listaRolesSistemas;
        }

        public RolesSistema FindById(int id)
        {
            string cadenaFind = "SELECT id, nombre FROM RolesSistema WHERE id = @id";
            RolesSistema unRS = null;
            using (SqlConnection cn = BdSQL.Conectar())
            {
                using (SqlCommand cmd = new SqlCommand(cadenaFind, cn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader != null && reader.Read())
                    {
                        unRS = new RolesSistema();
                        unRS.Load(reader);
                    }
                }
            }
            return unRS;
        }

        public bool Add(RolesSistema obj)
        {
            return obj != null && obj.Add();
        }

        public bool Delete(int id)
        {
            RolesSistema RS = this.FindById(id);
            return (RS != null && RS.Delete());

        }

        public bool Update(RolesSistema obj)
        {
            return obj != null && obj.Update();
        }
    }
}
