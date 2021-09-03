using map360.Data;
using map360.Data.Implementations;
using map360.Service;
using map360.Service.Implementations;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace map360.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel _kernel;

        public NinjectControllerFactory()
        {
            _kernel = new StandardKernel();
            _kernel.Bind<IUserDbContext>().To<UserDbContext>();
            _kernel.Bind<ICompanyDbContext>().To<CompanyDbContext>();
            _kernel.Bind<IUserRoleDbContext>().To<UserRoleDbContext>();
            _kernel.Bind<IUserAppService>().To<UserAppService>();
            _kernel.Bind<ICompanyAppService>().To<CompanyAppService>();
            _kernel.Bind<IUserRoleAppService>().To<UserRoleAppService>();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)_kernel.Get(controllerType);
        }
    }
}