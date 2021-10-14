using ConektaUTC.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ConektaUTC.Controllers
{
    public class WebhookController : Controller
    {


        // GET: Webhook
        [HttpPost]
        public ActionResult Index()
        {
            Logs log = new Logs();

            Stream req = Request.InputStream;
            req.Seek(0, System.IO.SeekOrigin.Begin);
            string json = new StreamReader(req).ReadToEnd();
            if (String.IsNullOrEmpty(json))
            {
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            log.writeLog("JSON: Notificacion: ", json);
            var obj = JObject.Parse(json);
            var type = obj.SelectToken("type");
            if (type.ToString().Contains("inbound_payment.lookup"))
            {
                return Json(new { payable = true, minAmount = 1000, maxAmount = 1000000 });
            }
            if (type.ToString().Contains("inbound_payment.payment_attempt"))
            {
                return Json(new { payable = true });
            }
            if (type.ToString().Contains("charge.pending_confirmation"))
            {
                return Json(new { });
            }
            if (type.ToString().Contains("charge.paid") )
            {
                
                var data = obj.SelectToken("data");
                var objeto = data.SelectToken("object");
                log.writeLog("EL CLIENTE QUE PAGO FUE:", objeto.SelectToken("customer_id"));
                var customer_id = objeto.SelectToken("customer_id");
                var amount = objeto.SelectToken("amount");
                var details = objeto.SelectToken("details");
                var name = details.SelectToken("name");
                consumeApiUrl consumeApiUrl = new consumeApiUrl();
                Orden orden = new Orden();
                string customer = consumeApiUrl.requestConekta("https://api.conekta.io/customers/" + customer_id.ToString(), "UTC");
                orden.compania = "UTC";
                if (customer.Contains("no ha sido encontrado"))
                {
                    orden.compania = "CETC";
                    customer = consumeApiUrl.requestConekta("https://api.conekta.io/customers/" + customer_id.ToString(), "CETC");
                }
                var datosCliente = JObject.Parse(customer);
                var metadata = datosCliente.SelectToken("metadata");
                var reference = metadata.SelectToken("reference");
                log.writeLog("LA REFERENCIA FUE ", reference);
                orden.referencia = reference.ToString();
                orden.nombre = name.ToString();
                orden.idCliente = customer_id.ToString();
                orden.fecha = DateTime.Now.ToString("dd/MM/yyyy");
                orden.status = "PAGADO";
                log.writeLog("DATOS DEL PAGO", customer_id.ToString() + "---" + name.ToString() + "---" + amount.ToString() + "---" + reference.ToString());
                string monto = amount.ToString().Substring(0, amount.ToString().Length - 2) + "." + amount.ToString().Substring(amount.ToString().Length - 2, 2);
                orden.monto = float.Parse(monto);
                orden.insertarOrden();
                return Json(new { });
            }

            if (type.ToString().Contains("order.paid"))
            {

                var data = obj.SelectToken("data");
                
                var objeto = data.SelectToken("object");
                
                var objeto2 = objeto.SelectToken("customer_info");
                log.writeLog("EL CLIENTE QUE PAGO FUE:", objeto2.SelectToken("customer_id"));
                var customer_id = objeto2.SelectToken("customer_id");
                var amount = objeto.SelectToken("amount");
                var details = objeto.SelectToken("details");
                var name = objeto2.SelectToken("name");
                consumeApiUrl consumeApiUrl = new consumeApiUrl();
                Orden orden = new Orden();
                var charges = objeto.SelectToken("charges");
                var data2 = charges.SelectToken("data");
                var payment_method = data2[0].SelectToken("payment_method");

             
                string customer = consumeApiUrl.requestConekta("https://api.conekta.io/customers/" + customer_id.ToString(), "UTC");
                orden.compania = "UTC";
                if (customer.Contains("no ha sido encontrado"))
                {
                    orden.compania = "CETC";
                    customer = consumeApiUrl.requestConekta("https://api.conekta.io/customers/" + customer_id.ToString(), "CETC");
                }
                var reference = payment_method.SelectToken("reference");
                log.writeLog("LA REFERENCIA FUE ", reference);
                orden.referencia = reference.ToString();
                orden.nombre = name.ToString();
                orden.idCliente = customer_id.ToString();
                orden.fecha = DateTime.Now.ToString("dd/MM/yyyy");
                orden.status = "PAGADO";
                log.writeLog("DATOS DEL PAGO", customer_id.ToString() + "---" + name.ToString() + "---" + amount.ToString() + "---" + reference.ToString());
                string monto = amount.ToString().Substring(0, amount.ToString().Length - 2) + "." + amount.ToString().Substring(amount.ToString().Length - 2, 2);
                orden.monto = float.Parse(monto);
                orden.insertarOrden();
                return Json(new { });
            }
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

     
    }
}
