using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades;
using System.Data;
using System.Data.SqlClient;

namespace Dominio.EntidadesNegocio
{
    public class RangoFechas
    {

        #region Properties
        public DateTime Fecha_ini { get; set; }
        public DateTime Fecha_fin { get; set; }
        public int Id { get; set; }
        #endregion

        #region Cadenas de comando para ACTIVE RECORD
        private string cadenaInsert = "INSERT INTO RangoFechaAnuncio VALUES (@fecha_ini,@fecha_fin)";
        private string cadenaUpdate = "UPDATE  RangoFechaAnuncio SET fecha_ini = @fecha_ini, fecha_fin = @fecha_fin WHERE id = @id";
        private string cadenaDelete = "DELETE  RangoFechaAnuncio WHERE id = @id";
        #endregion

        #region Métodos ACTIVE RECORD
        public bool Add()
        {
            if (this.Validar())
            {
                using (SqlConnection cn = BdSQL.Conectar())
                {
                    using (SqlCommand cmd = new SqlCommand(cadenaInsert, cn))
                    {
                        cmd.Parameters.AddWithValue("@fecha_ini", this.Fecha_ini);
                        cmd.Parameters.AddWithValue("@fecha_fin", this.Fecha_fin);
                        cn.Open();
                        int afectadas = cmd.ExecuteNonQuery();
                        return afectadas == 1;
                    }
                }
            }
            return false;
        }
        public bool Update()
        {
            if (this.Validar())
            {
                using (SqlConnection cn = BdSQL.Conectar())
                {
                    using (SqlCommand cmd = new SqlCommand(cadenaUpdate, cn))
                    {
                        cmd.Parameters.AddWithValue("@fecha_ini", this.Fecha_ini);
                        cmd.Parameters.AddWithValue("@fecha_fin", this.Fecha_fin);
                        cmd.Parameters.AddWithValue("@id", this.Id);
                        cn.Open();
                        int afectadas = cmd.ExecuteNonQuery();
                        return afectadas == 1;
                    }
                }
            }
            return false;
        }
        public bool Delete()
        {
            using (SqlConnection cn = BdSQL.Conectar())
            {
                using (SqlCommand cmd = new SqlCommand(cadenaDelete, cn))
                {

                    cmd.Parameters.AddWithValue("@id", this.Id);
                    cn.Open();
                    int afectadas = cmd.ExecuteNonQuery();
                    return afectadas == 1;
                }
            }
        }
        public void Load(IDataRecord dr)
        {
            if (dr != null)
            {
                //@fecha_ini,@fecha_fin,@variacion_precio
                this.Fecha_ini = Convert.ToDateTime(dr["fecha_ini"].ToString());
                this.Fecha_fin = Convert.ToDateTime(dr["fecha_fin"].ToString());
                this.Id = Convert.ToInt32(dr["id"]);
            }
        }
        #endregion

        #region Validaciones
        public bool Validar()
        {
            return true;
        }
        #endregion

        #region Redefiniciones de object
        public override string ToString()
        {
            return "";
        }
        #endregion
    }
}
