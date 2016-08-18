using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using System.Web.Security;

namespace UpDown.Models
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }


        private void AddBindings()
        {
            //ninjectKernel.Bind<IUsersRepository>().To<EFUsersRepository>();
            //ninjectKernel.Bind<ManianaEntities>().ToSelf().WithConstructorArgument(""
            ninjectKernel.Inject(Membership.Provider);
        }
    }
}