using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using conekta;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;

namespace ConektaUTC.Controllers
{
    public class ConektaController : System.Web.Http.ApiController
    {
        // GET: api/Conekta
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        public object requestConekta(string url)
        {
            RestClient client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Basic a2V5X0VMemNSV0RybpMrcHd8YVYznTk2RHc6");
            IRestResponse response = client.Execute(request);
            var content = response.Content;
            return JObject.Parse(content);
        }


        public string crearHook() {

            return "";
        }

        public Customer cliente(string email, string name) {
            
            Customer customer;
          
                 customer = new conekta.Customer().create(@"{
                  ""name"": """ + name + @""",
                  ""email"": """ + email + @""",
                  ""phone"": """",
                  ""plan_id"": ""gold-plan"",
                  ""corporate"": true
                  }]
                }");

            return customer;
        }

        // GET: api/Conekta/5
        public string Get(string id, string title, float unit_price, string email, string external_reference, string name)
        {
            conekta.Api.apiKey = "key_eYvWV7gSDkNYXsmr";
            conekta.Api.version = "2.0.0";
            Customer customer = cliente(email,name);

              Order order = new conekta.Order().create(@"{
                  ""currency"":""MXN"",
                  ""customer_info"": {
                    ""customer_id"": """+customer.id+@""",
                    ""antifraud_info"": {
                      ""paid_transactions"": 4
                    }
                  },
                  ""line_items"": [{
                    ""name"": ""Box of Cohiba S1s"",
                    ""unit_price"": 35000,
                    ""quantity"": 1,
                    ""antifraud_info"": {
                      ""trip_id"": ""12345"",
                      ""driver_id"": ""5f671gaqg1"",
                      ""ticket_class"": ""X"",
                      ""driver_id"": ""5f671gaqg1"",
                      ""pickup_latlon"": ""19.4153209,-99.1804722"",
                      ""dropoff_latlon"": ""19.434606,-99.1639283""
                    }
                  }],
                  ""charges"": [{
                    ""payment_method"": {
                      ""type"": ""default""
                    }
                  }]
                }");

            return "";
        }

        // POST: api/Conekta
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Conekta/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Conekta/5
        public void Delete(int id)
        {
        }
    }
}
