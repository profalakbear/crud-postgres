using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace map360.Models
{
    public class Company   
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string TaxNo { get; set; }
        public long Phone { get; set; }
        public string Address { get; set; }
    }
}