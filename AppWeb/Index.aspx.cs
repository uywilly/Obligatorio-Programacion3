using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace AppWeb
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager
                .ConnectionStrings["miConexion"]
                .ConnectionString;
            conexion.Open();
            /*if(conexion.State == ConnectionState.Open)
            {
                Response.Write("Conexión Abierta<br>");
            } else
            {
                Response.Write("Conexión Cerrada<br>");
            }*/
            // creo el objeto command... que va a tener el comando adentro :)
            SqlCommand comando = new SqlCommand();
            // le seteo el comando que voy a ejecutar..
            // cmd.CommandText = "insert into prueba values('Pedro')";
            // le seteamos el connection, con que conección va a realizar el command..
            comando.Connection = conexion;
            // ejecuto un comando de actualización. No retorna filas
            // int filasAfectadas = comando.ExecuteNonQuery(); // esta forma de ejecutar es porque no retorna filas.
            // Response.Write("Se insertaron filas: " + filasAfectadas);
            // filasAfectadas tiene el número de filas afectadas por ese command...
            // le cargo otro comando para ejecutar...
            // comando.CommandText = "delete prueba where id=1";
            // lo ejecuto y me guardo cuantas filas fueron afectadas..
            // filasAfectadas = cmd.ExecuteNonQuery();
            // Response.Write("Se borraron filas: " + filasAfectadas);
            // cargo otro comando..
            comando.CommandText = "select * from prueba";
            // como es un select lo que devuelve lo guardo en filasPrueba..
            SqlDataReader filasPrueba = comando.ExecuteReader();
            // mientras haya filas en filasPrueba..
            while (filasPrueba.Read())
            {
                ListBox1.Items.Add ("ID:" + filasPrueba["id"].ToString() + " Nombre:" + filasPrueba["nombre"].ToString());
            }
            // cierro la conexión
            conexion.Close();
            // libero los recursos (creo)
            conexion.Dispose();
        }
    }
}