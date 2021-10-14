using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using System.Configuration;

namespace ConektaUTC.Models
{
    public class consumeApiUrl
    {

        public string requestConekta(string url,string compania)
        {
            RestClient client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "application/vnd.conekta-v2.0.0+json");
            if (ConfigurationManager.AppSettings["env"].ToString().Contains("1"))
            {
                if (compania.Contains("UTC"))
                    request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["api_key_public_utc_prod"].ToString());
                else
                    request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["api_key_public_cetc_prod"].ToString());
            }
            else {
                if (compania.Contains("UTC"))
                    request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["api_key_public_utc_dev"].ToString());
                else
                    request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["api_key_public_cetc_dev"].ToString());
            }
           


            IRestResponse response = client.Execute(request);
            var content = response.Content;
            return content.ToString();
        }
    }
}