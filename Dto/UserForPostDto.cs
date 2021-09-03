using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace map360.Dto
{
    public class UserForPostDto
    {
        public string NameAndSurname { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public int CompanyId { get; set; }
    }
}