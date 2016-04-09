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
    public class Persona : IEntity
    {
        #region Properties
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Ci { get; set; }
        public string Email { get; set; }
        //public List<Rol> Roles { get; set; }
        #endregion

        #region Cadenas de comando para ACTIVE RECORD
        private string cadenaInsert = "INSERT INTO Persona VALUES (@nombre,@apellido,@ci,@email)";
        private string cadenaUpdate = "UPDATE Persona SET nombre = @nombre, apellido = apellido, ci = @ci, email = @email WHERE id = @id";
        private string cadenaDelete = "DELETE Persona WHERE id = @id";

        #endregion

        #region Métodos ACTIVE RECORD
        public bool Add()
        {
            if (this.Validar())
            {
                //BdSQL retorna una SQLConnection con el string de conexion en el ConnectionString del WEbconfig 
                using (SqlConnection cn = BdSQL.Conectar())
                {
                    using (SqlCommand cmd = new SqlCommand(cadenaInsert, cn))
                    {
                        //Asigna el valor a los parametros usados en la consulta
                        cmd.Parameters.AddWithValue("@nombre", this.Nombre);
                        cmd.Parameters.AddWithValue("@apellido", this.Apellido);
                        cmd.Parameters.AddWithValue("@ci", this.Ci);
                        cmd.Parameters.AddWithValue("@email", this.Email);
                        //Abro la conexion
                        cn.Open();
                        //Guardo la cantidad de lineas afectadas por la consulta
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
                        cmd.Parameters.AddWithValue("@nombre", this.Nombre);
                        cmd.Parameters.AddWithValue("@apellido", this.Apellido);
                        cmd.Parameters.AddWithValue("@ci", this.Ci);
                        cmd.Parameters.AddWithValue("@email", this.Email);
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
                //@nombre,@apellido,@ci,@email
                this.Nombre = dr["nombre"].ToString();
                this.Apellido = dr["apellido"].ToString();
                this.Ci = Convert.ToInt32(dr["ci"].ToString());
                this.Email = dr["email"].ToString();
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
            return this.Id + " - Nombre: " + this.Nombre + " - Apellido: " + this.Apellido;
        }
        #endregion
    }
}
