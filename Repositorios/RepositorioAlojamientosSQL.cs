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
        //public bool Add(Departamento obj)
        //{
        //    return obj != null && obj.Add();
        //}

        //public bool Delete(int id)
        //{
        //    Departamento d = this.FindById(id);
        //    return (d != null && d.Delete());

        //}

        //public List<Departamento> FindAll()
        //{
        //    string cadenaFindAll = "SELECT id,nombre FROM Departamento";
        //    List<Departamento> listaDeptos = new List<Departamento>();
        //    using (SqlConnection cn = BdSQL.Conectar())
        //    {
        //        using (SqlCommand cmd = new SqlCommand(cadenaFindAll, cn))
        //        {
        //            cn.Open();
        //            SqlDataReader reader = cmd.ExecuteReader();
        //            if (reader != null)
        //            {
        //                while (reader.Read())
        //                {
        //                    Departamento d = new Departamento();
        //                    d.Load(reader);
        //                    if (d.Validar())
        //                        listaDeptos.Add(d);
        //                }
        //            }
        //        }
        //    }
        //    return listaDeptos;
        //}

        //public Departamento FindById(int id)
        //{
        //    string cadenaFind = "SELECT id,nombre FROM Departamento WHERE id=@id";
        //    Departamento d = null;
        //    using (SqlConnection cn = BdSQL.Conectar())
        //    {
        //        using (SqlCommand cmd = new SqlCommand(cadenaFind, cn))
        //        {
        //            cmd.Parameters.AddWithValue("@id", id);
        //            cn.Open();
        //            SqlDataReader reader = cmd.ExecuteReader();
        //            if (reader != null && reader.Read())
        //            {
        //                d = new Departamento();
        //                d.Load(reader);
        //            }
        //        }
        //    }
        //    return d;
        //}

        //public bool Update(Departamento obj)
        //{
        //    return obj != null && obj.Update();
        //}
    }
}
