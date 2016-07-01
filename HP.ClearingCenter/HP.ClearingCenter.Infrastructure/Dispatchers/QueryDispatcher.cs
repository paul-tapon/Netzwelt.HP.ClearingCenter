using System;
using HP.ClearingCenter.Infrastructure.Contracts;
using Ninject;
using System.Reflection;
using System.Collections.Generic;
using Ninject.Parameters;
using Ninject.Syntax;

namespace HP.ClearingCenter.Infrastructure.Dispatchers
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private static readonly IDictionary<string, MethodInfo> _objectFactory = new Dictionary<string, MethodInfo>();
        private readonly IKernel _kernel;

        public QueryDispatcher(IKernel kernel)
        {
            if (kernel == null)
            {
                throw new ArgumentNullException("kernel");
            }
            _kernel = kernel;
        }

        public TResult Dispatch<TResult>(IQuery<TResult> query)
        {   
            try
            {
                object queryHandler = this.ResolveQueryHandlerInstance<TResult>(query);
                MethodInfo retrieveMethod = queryHandler.GetType().GetMethod("Retrieve", BindingFlags.Public | BindingFlags.Instance);
                return (TResult)retrieveMethod.Invoke(queryHandler, new[] { query });                
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }

        public object ResolveQueryHandlerInstance<TResult>(IQuery<TResult> query)
        {
            Type queryType = query.GetType();

            if (!_objectFactory.ContainsKey(queryType.FullName))
            {

                Type resultType = typeof(TResult);
                Type queryHandlerType = typeof(IQueryHandler<,>).MakeGenericType(queryType, resultType);

                MethodInfo getMethod = typeof(ResolutionExtensions).GetMethod("Get", BindingFlags.Static | BindingFlags.Public, null, new Type[] { typeof(IResolutionRoot), typeof(IParameter[]) }, null);
                MethodInfo getGenericMethod = getMethod.MakeGenericMethod(queryHandlerType);
                _objectFactory.Add(queryType.FullName, getGenericMethod);
            }

            MethodInfo cachedGenericMethod = _objectFactory[queryType.FullName];
            return cachedGenericMethod.Invoke(null, new object[] { _kernel, new IParameter[0] });
        }
    }
}
