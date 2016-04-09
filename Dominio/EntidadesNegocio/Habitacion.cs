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
    public class Habitacion:IEntity
    {
        #region Properties
        public int Id { get; set; }
        //public List<Servicio> Servicios { get; set; }
        public bool Baño_Privado { get; set; }
        public int Camas { get; set; }
        public decimal Precio_base { get; set; }
        //public Alojamiento Alojamiento { get; set; }
        #endregion

        #region Cadenas de comando para ACTIVE RECORD
        private string cadenaInsert = "INSERT INTO Habitacion VALUES (@baño_privado,@camas,@precio_base)";
        private string cadenaUpdate = "UPDATE  Habitacion SET baño_privado = @baño_privado, camas = @camas, precio_base = @precio_base WHERE id = @id";
        private string cadenaDelete = "DELETE  Habitacion WHERE id = @id";
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
                        cmd.Parameters.AddWithValue("@baño_privado", this.Baño_Privado);
                        cmd.Parameters.AddWithValue("@camas", this.Camas);
                        cmd.Parameters.AddWithValue("@precio_base", this.Precio_base);
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
                        cmd.Parameters.AddWithValue("@baño_privado", this.Baño_Privado);
                        cmd.Parameters.AddWithValue("@camas", this.Camas);
                        cmd.Parameters.AddWithValue("@precio_base", this.Precio_base);
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
                this.Baño_Privado = Convert.ToBoolean(dr["baño_privado"].ToString());
                this.Camas = Convert.ToInt32(dr["camas"].ToString());
                this.Precio_base = Convert.ToDecimal(dr["precio_base"].ToString());
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
            return this.Id.ToString() + " - BañoPriv: " + this.Baño_Privado.ToString() + " - Camas: " + this.Camas.ToString() + " - Precio_base: " + this.Precio_base.ToString();
        }
        #endregion
    }
}