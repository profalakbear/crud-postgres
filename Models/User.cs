using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace map360.Models
{
    public class User
    {
        //public User()
        //{
        //    UserRole = new UserRole();
        //    Company = new Company();
        //}

        public long Id { get; set; }
        //public UserPostDto Details { get; set; }

        // asagdakilari sil
        public string NameAndSurname { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public int CompanyId { get; set; }

        //public UserRole UserRole { get; set; }
        //public Company Company { get; set; }
    }
}