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
    public abstract class Rol: IEntity
    {
        #region Properties
        public int Id { get; set; }
        public string Tipo { get; set; }
        #endregion

        #region Cadenas de comando para ACTIVE RECORD
        private string cadenaInsert = "INSERT INTO Rol VALUES (@tipo)";
        private string cadenaUpdate = "UPDATE  Rol SET tipo = @tipo WHERE id = @id";
        private string cadenaDelete = "DELETE  Rol WHERE id = @id";
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
                        cmd.Parameters.AddWithValue("@tipo", this.Tipo);
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
                        cmd.Parameters.AddWithValue("@tipo", this.Tipo);
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
            }
        }
        #endregion

        #region Validaciones
        public bool Validar() 
        {
            return this.Tipo.Length >= 3;
        }
        #endregion

        #region Redefiniciones de object
        public override string ToString()
        {
            return this.Id + " - " + this.Tipo;
        }
        #endregion
    }
}
