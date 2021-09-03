using map360.Data;
using map360.Dto;
using map360.Models;
using map360.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Web;
using System.Web.Mvc;

namespace map360.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserAppService _service;
        private readonly ICompanyAppService _companyService;
        public UserController() { }

        public UserController(IUserAppService service, ICompanyAppService companyService)
        {
            _service = service;
            _companyService = companyService;
        }
        
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult PopulateModal(string id)
        {
            User user = new User();
            int my_id = Convert.ToInt32(id);

            if (Request.HttpMethod == "GET")
            {
                user = _service.GetById(my_id);

                return Json(
                    new
                    {
                        succes = true,
                        data = user
                    }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                error = true,
                message = "Something bad happened"
            });
        }

        public JsonResult Update(User user)
        {
            User returned = new User();

            if (user != null)
            {
                returned = _service.Update(user);

                if (returned != null)
                {
                    return Json(new { succes = true, data = returned }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { error = true, data = false, message = "Something bad happened" });
        }


        [HttpPost]
        public JsonResult Create(User user)
        {
            User returned = new User();

            if(user != null)
            {
                returned = _service.Add(user);

                if(returned != null)
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
                return Json(new {
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

        public JsonResult GetUsers()
        {
            List<User> users = new List<User>();

            if (Request.HttpMethod == "GET")
            {
                users = _service.GetAll();

                return Json(
                    new
                    {
                        succes = true,
                        data = users
                    }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                error = true,
                message = "Something bad happened"
            });
        }
    }
}
