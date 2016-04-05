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
    public class RangoPrecio:IEntity
    {
        #region Properties
        public DateTime Fecha_ini { get; set; }
        public DateTime Fecha_fin { get; set; }
        public decimal Variacion_precio { get; set; }
        public int Id { get; set; }
        #endregion

        #region Cadenas de comando para ACTIVE RECORD
        private string cadenaInsert = "INSERT INTO Ubicacion VALUES (@fecha_ini,@fecha_fin,@variacion_precio)";
        private string cadenaUpdate = "UPDATE  Ubicacion SET fecha_ini = @fecha_ini, fecha_fin = @fecha_fin, variacion_precio = @variacion_precio WHERE id = @id";
        private string cadenaDelete = "DELETE  Ubicacion WHERE id = @id";
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
                        cmd.Parameters.AddWithValue("@variacion_precio", this.Variacion_precio);
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
                        cmd.Parameters.AddWithValue("@variacion_precio", this.Variacion_precio);
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
                this.Variacion_precio = Convert.ToDecimal(dr["variacion_precio"].ToString());

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
            return this.Id + " - " + this.Barrio;
        }
        #endregion
    }
}
