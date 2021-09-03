using map360.Models;
using map360.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace map360.Controllers
{
    public class CompanyController : Controller
    {

        private readonly IUserRoleAppService _roleService;
        private readonly ICompanyAppService _service;

        public CompanyController() { }

        public CompanyController(IUserRoleAppService roleService, ICompanyAppService service) {
            _roleService = roleService;
            _service = service;
        }

        // GET: Company
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult PopulateModal(string id)
        {
            Company company = new Company();
            int my_id = Convert.ToInt32(id);

            if (Request.HttpMethod == "GET")
            {
                company = _service.GetById(my_id);

                return Json(
                    new
                    {
                        succes = true,
                        data = company
                    }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                error = true,
                message = "Something bad happened"
            });
        }

        [HttpGet]
        public JsonResult GetCompanies()
        {
            List<Company> cmp = new List<Company>();

            if (Request.HttpMethod == "GET")
            {
                cmp = _service.GetAll();

                return Json(
                    new
                    {
                        succes = true,
                        data = cmp
                    }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                error = true,
                message = "Something bad happened"
            });
        }


        [HttpPost]
        public JsonResult Create(Company company)
        {
            Company returned = new Company();

            if (company != null)
            {
                returned = _service.Add(company);

                if (returned != null)
                {
                    return Json(new { succes = true, data = returned }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { error = true, data = false, message = "Something bad happened" });
        }


        public JsonResult Delete(string id)
        {
            bool ok;

            int my_id = Convert.ToInt32(id);

            ok = _service.Delete(my_id);

            if (ok)
            {
                return Json(new
                {
                    succes = true,
                    data = ok
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                error = true,
                data = ok,
                message = "Something bad happened"
            });
        }


        public JsonResult GetUserRoles()
        {
            List<UserRole> ur = new List<UserRole>();


            if (Request.HttpMethod == "GET")
            {
                ur = _roleService.GetAll();

                return Json(
                    new
                    {
                        succes = true,
                        data = ur
                    }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                error = true,
                message = "Something bad happened"
            });
        }

        public JsonResult Update(Company company)
        {
            Company returned = new Company();

            if (company != null)
            {
                returned = _service.Update(company);

                if (returned != null)
                {
                    return Json(new { succes = true, data = returned }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { error = true, data = false, message = "Something bad happened" });
        }
    }
}
