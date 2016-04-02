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
    public class Alojamiento:IEntity
    {
        #region Properties
        public int Id { get; set; }
        public string Tipo { get; set; }
        #endregion

        #region Cadenas de comando para ACTIVE RECORD
        private string cadenaInsert = "INSERT INTO Alojamiento VALUES (@tipo)";
        private string cadenaUpdate = "UPDATE  Alojamiento SET nombre=@tipo WHERE id=@id";
        private string cadenaDelete = "DELETE  Alojamiento WHERE id=@id";
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
                        // acá va el resto de parametros que vamos a insertar...
                        cn.Open();
                        int afectadas = cmd.ExecuteNonQuery();
                        // retorna la comparacion de afectadas con 1 :) true/false
                        return afectadas == 1;
                        // no hace falta el close y el dispose porque usamos el using :)
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
        public bool Validar() // esto es cualquier cosa :)
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
