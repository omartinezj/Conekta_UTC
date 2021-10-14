using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConektaUTC.Models
{
    public class Evento
    {
        public string id { get; set; }
        public string type { get; set; }
        public string idCustomer { get; set; }
        public string status { get; set; }
        public string description { get; set; }
        public string failureCode { get; set; }
        public string failureMessage { get; set; }
        public double amount { get; set; }
        public string serviceName { get; set; }
        public string serviceNumber { get; set; }
        public string spei { get; set; }
        public string reference { get; set; }
        
    }
}