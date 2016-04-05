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
    public class RepositorioReservasSQL : IRepositorioReservas
    {
        public List<Reserva> FindAll()
        {
            string cadenaFindAll = "SELECT fecha_ini, fecha_fin FROM Reserva";
            List<Reserva> listaReservas = new List<Reserva>();
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
                            Reserva  unaR = new Reserva();
                            unaR.Load(reader);
                            if (unaR.Validar())
                                listaReservas.Add(unaR);
                        }
                    }
                }
            }
            return listaReservas;
        }

        public Reserva FindById(int id)
        {
            string cadenaFind = "SELECT fecha_ini, fecha_fin FROM Reserva WHERE id = @id";
            Reserva unaR = null;
            using (SqlConnection cn = BdSQL.Conectar())
            {
                using (SqlCommand cmd = new SqlCommand(cadenaFind, cn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader != null && reader.Read())
                    {
                        unaR = new Reserva();
                        unaR.Load(reader);
                    }
                }
            }
            return unaR;
        }
    }
}
