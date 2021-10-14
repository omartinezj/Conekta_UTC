using ConektaUTC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace ConektaUTC.Controllers
{
    public class ApiController : Controller
    {
        // GET: api/Webhook
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Webhook/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Webhook
        [System.Web.Mvc.HttpPost]
        public ActionResult Webhook([FromBody]Object value)
        {

            
            Logs log = new Logs();
            dynamic dynamic = value;
            log.writeLog("información: ", value);
            int contador = 0;
            bool bandera = false;
            do {
                try
                {
                    if (dynamic[contador] != null)
                    {
                        log.writeLog("información: ", dynamic[contador].id);
                        log.writeLog("información: ", dynamic[contador].type);
                        log.writeLog("información: ", dynamic[contador].data);
                    }
                    else
                    {
                        bandera = true;
                    }
                    contador++;
                }
                catch (Exception e) {
                    bandera = true;
                }
                
                

            } while (!bandera);



            // conekta.Webhook webhook = new conekta.Webhook().find("");
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        // PUT: api/Webhook/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Webhook/5
        public void Delete(int id)
        {
        }
    }
}
