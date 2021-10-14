using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ConektaUTC.Models
{
    public class Orden
    {
        public string referencia { get; set; }
        public float monto { get; set; }
        public string status { get; set; }

        public string fecha { get; set; }
        public string idCliente { get; set; }
        public int envioBanner { get; set; }
        public string nombre { get; set; }
        public string compania { get; set; }

        public void insertarOrden() {
            try
            {
                string nuevaOrden = "insert into Oxxo_orden(referencia,monto,status,idCliente,envioBanner,name,fecha,compania)  values('" + this.referencia + "','" + this.monto + "','" + this.status + "','" + this.idCliente + "',0,'" + this.nombre + "','"+this.fecha+"','"+this.compania+"')";
                var connectionString = ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString;
                var conexionSQL = new SqlConnection(connectionString);
                SqlCommand consultaSQL = new SqlCommand(nuevaOrden, conexionSQL);
                conexionSQL.Open();
                consultaSQL.ExecuteNonQuery();
                conexionSQL.Close();
            }
            catch (Exception e) {
                Logs logs = new Logs();
                logs.writeLog("ERROR:", e.Message);
            }
            
            

        }
    }
}