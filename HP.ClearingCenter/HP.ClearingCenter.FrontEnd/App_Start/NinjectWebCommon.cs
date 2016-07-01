using System.Web.Mvc;
using System;
using System.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    
[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(HP.ClearingCenter.FrontEnd.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(HP.ClearingCenter.FrontEnd.App_Start.NinjectWebCommon), "Stop")]

namespace HP.ClearingCenter.FrontEnd.App_Start
{   
    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Mvc;
    using Ninject.Extensions.Conventions;
    using HP.ClearingCenter.FrontEnd.Infrastructure.Services;
    using HP.ClearingCenter.FrontEnd.Infrastructure.Web;
    using HP.ClearingCenter.Application.Services;
    using HP.ClearingCenter.Infrastructure.Contracts;
    using HP.ClearingCenter.Infrastructure;
    using HP.ClearingCenter.Infrastructure.Dispatchers;
    using HP.ClearingCenter.Infrastructure.Services;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterHandlers(kernel);
                ServicesConfig.RegisterServices(kernel);

                DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));

                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        private static void RegisterHandlers(StandardKernel kernel)
        {
            // http context
            kernel.Bind<HttpContext>().ToMethod(ctx => HttpContext.Current).InTransientScope();
            kernel.Bind<HttpContextBase>().ToMethod(ctx => new HttpContextWrapper(HttpContext.Current)).InTransientScope();
            kernel.Bind<Func<HttpContextBase>>().ToMethod(ctx => () => new HttpContextWrapper(HttpContext.Current)).InTransientScope();

            // dispatchers
            kernel.Bind<IQueryDispatcher>().To<QueryDispatcher>().WithConstructorArgument("kernel", kernel);
            kernel.Bind<ICommandDispatcher>().To<CommandDispatcher>().WithConstructorArgument("kernel", kernel);

            // handlers
            kernel.Bind(x => x
               .FromAssembliesMatching("HP.ClearingCenter.Application.dll")
               .SelectAllClasses().InheritedFrom(typeof(IQueryHandler<,>))
               .BindAllInterfaces());

            kernel.Bind(x => x
                .FromAssembliesMatching("HP.ClearingCenter.Application.dll")
                .SelectAllClasses().InheritedFrom(typeof(ICommandHandler<>))
                .BindAllInterfaces());

            kernel.Unbind<ModelValidatorProvider>();
        }
    }
}
