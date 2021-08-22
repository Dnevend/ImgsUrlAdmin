using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using Unity;

namespace FilesUrl.Common.DI
{
    public class UnityResolver : IDependencyResolver
    {

        /// <summary>
        /// The Unity Container (Unity) is a full featured, extensible dependency injection container. 
        /// It facilitates building loosely coupled applications and provides developers with host of other useful features.
        /// </summary>
        protected IUnityContainer container;

        public UnityResolver(IUnityContainer container)
        {

            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            this.container = container;

        }

        public IDependencyScope BeginScope()
        {
            var child = container.CreateChildContainer();
            return new UnityResolver(child);
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type servicesType)
        {
            try
            {
                return container.ResolveAll(servicesType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }


        public void Dispose()
        {
            container.Dispose();
        }

    }
}