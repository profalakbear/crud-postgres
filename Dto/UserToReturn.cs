using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace map360.Dto
{
    public class UserToReturn
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public long RoleId { get; set; }
        public string NameAndSurname { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
        public string Name { get; set; }
        public string TaxNo { get; set; }
        public long Phone { get; set; }
        public string Address { get; set; }
    }
}