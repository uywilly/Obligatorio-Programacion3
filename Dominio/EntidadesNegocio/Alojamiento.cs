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
    public class Alojamiento : IEntity
    {
        #region Properties
        public int Id { get; set; }
        public string Tipo { get; set; }
        public int Cupo_max { get; set; }
        public Ubicacion Ubicacion { get; set;}
        public List<RangoPrecio> Precios_temporada { get; set; }
        #endregion

        #region Cadenas de comando para ACTIVE RECORD
        private string cadenaInsert = "INSERT INTO Alojamiento VALUES (@tipo,@cupo_max); SELECT CAST(SCOPE_IDENTIY() AS INT);";
        private string cadenaUpdate = "UPDATE  Alojamiento SET tipo=@tipo, cupo_max=@cupo_max WHERE id=@id";
        private string cadenaDelete = "DELETE  Alojamiento WHERE id=@id";
        #endregion

        #region Métodos ACTIVE RECORD
        public bool Add()
        {
            if (this.Validar())
            {
                SqlConnection cn = BdSQL.Conectar();
                SqlTransaction trn = null;
                try
                {
                    SqlCommand cmd = new SqlCommand(cadenaInsert, cn);

                    // acá va el resto de parametros que vamos a insertar...
                    cmd.Parameters.AddWithValue("@tipo", this.Tipo);
                    cmd.Parameters.AddWithValue("@cupo_max", this.Cupo_max);
                    //abrimos la coneccion
                    cn.Open();

                    //iniciamos la transaccion
                    trn = cn.BeginTransaction();
                    cmd.Transaction = trn;
                    int idAlojamiento = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.CommandText = "INSERT INTO RangoPrecio VALUES (@fecha_inicio,@fecha_fin,@variacion_precio,@id_alojamiento)";
                    foreach (RangoPrecio unR in this.Precios_temporada)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@id_alojamiento", idAlojamiento);
                        cmd.Parameters.AddWithValue("@fecha_inicio", unR.Fecha_inicio);
                        cmd.Parameters.AddWithValue("@fecha_fin", unR.Fecha_fin);
                        cmd.Parameters.AddWithValue("@variacion_precio", unR.Variacion_precio);
                        cmd.ExecuteNonQuery();
                    }
                    // Agregar la ubicacion 
                    
                    cmd.CommandText = "INSERT INTO Ubicacion VALUES (@id_alojamiento,@ciudad,@barrio,@dirLinea1,@dirLinea2)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@id_alojamiento", idAlojamiento);
                    cmd.Parameters.AddWithValue("@ciudad", this.Ubicacion.Ciudad);
                    cmd.Parameters.AddWithValue("@barrio", this.Ubicacion.Barrio);
                    cmd.Parameters.AddWithValue("@dirLinea1", this.Ubicacion.DireccionLinea1);
                    cmd.Parameters.AddWithValue("@dirLinea2", this.Ubicacion.DireccionLinea2);
                    cmd.ExecuteNonQuery();
                    trn.Commit();
                    return true;

                }//fin del try
                catch (Exception ex)
                {
                    //falta hacer algo con la excepcion
                    trn.Rollback();
                    return false;

                }//fin del catch
                finally
                {
                    trn.Dispose();
                    cn.Close();
                    cn.Dispose();
                }
            }
            else
            {
                return false;
            }
        }
        public bool Update()
        {
            if (this.Validar())
            {
                using (SqlConnection cn = BdSQL.Conectar())
                {
                    using (SqlCommand cmd = new SqlCommand(cadenaUpdate, cn))
                    {
                        cmd.Parameters.AddWithValue("@tipo", this.Tipo);
                        cmd.Parameters.AddWithValue("@cupo_max", this.Cupo_max);
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
                this.Tipo = dr["tipo"].ToString();
                this.Id = Convert.ToInt32(dr["id"]);
                this.Cupo_max = Convert.ToInt32(dr["cupo_max"]);
            }
        }
        public void loadRangoPrecio(RangoPrecio unR, IDataRecord dr)
        {
            if (dr != null)
            {
                unR.Fecha_inicio = dr.GetDateTime(dr.GetOrdinal("fecha_inicio"));
                unR.Fecha_fin = dr.GetDateTime(dr.GetOrdinal("fecha_fin"));
                unR.Variacion_precio = dr.GetDecimal(dr.GetOrdinal("variacion_precio"));
                this.Id = Convert.ToInt32(dr["id"]);
            }
        }
        #endregion

        #region Validaciones
        public bool Validar() // esto es cualquier cosa :)
        {
            return this.Tipo.Length >= 3;
        }
        #endregion

        #region Redefiniciones de object
        public override string ToString()
        {
            return this.Id + " - Tipo: " + this.Tipo + " - CupoMax: " + this.Cupo_max.ToString();
        }
        #endregion
    }
}
