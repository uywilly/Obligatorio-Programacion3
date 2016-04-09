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
            //string cadenaFind = "SELECT id,tipo,cupo_max FROM Alojamiento WHERE id=@id";
            string cadenaFind = "SELECT Alojamiento.*, Ubicacion.ciudad, Ubicacion.barrio, Ubicacion.dirLinea1, Ubicacion.dirLinea2id,tipo,cupo_max FROM Alojamiento, Ubicacion WHERE Alojamiento.id_Ubicacion = Ubicacion.id AND Alojamiento.id = @id";
            SqlConnection cn = BdSQL.Conectar();
            List<RangoPrecio> precios_temporada = new List<RangoPrecio>();
            Alojamiento unA = null;
            try
            {
                
                SqlCommand cmd = new SqlCommand(cadenaFind, cn);
                cmd.Parameters.AddWithValue("@id", id);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader != null && reader.Read())
                {
                    unA = new Alojamiento();
                    unA.Load(reader);
                }
                //Cargo los elementos de la lista de rango precios
                cmd.CommandText = "SELECT * FROM RangoPrecio WHERE idAlojamiento = @id";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", id);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RangoPrecio unR = new RangoPrecio();
                    unA.loadRangoPrecio(unR,reader);
                    precios_temporada.Add(unR);
                }
                unA.Precios_temporada = precios_temporada;
                
                return unA;
            }
            catch(Exception ex)
            {
                //mostrar exception
                unA.Precios_temporada = null;
                return unA = null;
            }
            finally
            {
                cn.Close();
                cn.Dispose();
            }
        }

        public bool Update(Alojamiento obj)
        {
            return obj != null && obj.Update();
        }
    }
}
