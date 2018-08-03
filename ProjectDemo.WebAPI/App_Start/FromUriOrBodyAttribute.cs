using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;

namespace ProjectDemo.WebAPI
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Parameter, Inherited = true, AllowMultiple = false)]
    public sealed class FromUriOrBodyAttribute : ParameterBindingAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public override HttpParameterBinding GetBinding(HttpParameterDescriptor parameter)
        {
            return new FromUriOrBodyParameterBinding(parameter);
        }

        /// <summary>
        /// 
        /// </summary>
        public class FromUriOrBodyParameterBinding : HttpParameterBinding
        {
            HttpParameterBinding _defaultUriBinding;
            HttpParameterBinding _defaultFormatterBinding;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="desc"></param>
            public FromUriOrBodyParameterBinding(HttpParameterDescriptor desc)
                : base(desc)
            {
                _defaultUriBinding = new FromUriAttribute().GetBinding(desc);
                _defaultFormatterBinding = new FromBodyAttribute().GetBinding(desc);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="metadataProvider"></param>
            /// <param name="actionContext"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public override Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext actionContext, CancellationToken cancellationToken)
            {
                if (actionContext.Request.Content != null && actionContext.Request.Content.Headers.ContentLength > 0)
                {
                    return _defaultFormatterBinding.ExecuteBindingAsync(metadataProvider, actionContext, cancellationToken);
                }
                else
                {
                    return _defaultUriBinding.ExecuteBindingAsync(metadataProvider, actionContext, cancellationToken);
                }
            }

        }
    }
}