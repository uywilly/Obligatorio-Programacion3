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
    public class Ubicacion:IEntity
    {

        #region Properties
        public string Ciudad { get; set; }
        public string Barrio { get; set; }
        public string DireccionLinea1 { get; set; }
        public string DireccionLinea2 { get; set; }
        public int Id { get; set; }
        #endregion

        #region Cadenas de comando para ACTIVE RECORD
        private string cadenaInsert = "INSERT INTO Ubicacion VALUES (@ciudad,@barrio,@dirLinea1,@dirLinea2)";
        private string cadenaUpdate = "UPDATE  Ubicacion SET ciudad = @ciudad, barrio = @barrio, dirLinea1 = @dirLinea1, dirLinea2 = @dirLinea2 WHERE id = @id";
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
                        cmd.Parameters.AddWithValue("@ciudad", this.Ciudad);
                        cmd.Parameters.AddWithValue("@barrio", this.Barrio);
                        cmd.Parameters.AddWithValue("@dirLinea1", this.DireccionLinea1);
                        cmd.Parameters.AddWithValue("@dirLinea2", this.DireccionLinea2);
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
                        cmd.Parameters.AddWithValue("@ciudad", this.Ciudad);
                        cmd.Parameters.AddWithValue("@barrio", this.Barrio);
                        cmd.Parameters.AddWithValue("@dirLinea1", this.DireccionLinea1);
                        cmd.Parameters.AddWithValue("@dirLinea2", this.DireccionLinea2);
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
                //@ciudad,@barrio,@dirLinea1,@dirLinea2
                this.Ciudad = dr["ciudad"].ToString();
                this.Barrio = dr["barrio"].ToString();
                this.DireccionLinea1 = dr["dirLinea1"].ToString();
                this.DireccionLinea1 = dr["dirLinea2"].ToString();
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
