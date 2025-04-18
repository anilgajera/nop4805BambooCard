using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Nop.Core.Domain.Customers;

namespace Nop.Plugin.Misc.WebApi.Frontend.Helpers;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : TypeFilterAttribute
{
    #region Ctor

    /// <summary>
    /// Create instance of the filter attribute
    /// </summary>
    /// <param name="ignore">Whether to ignore the execution of filter actions</param>
    public AuthorizeAttribute(bool ignore = false) : base(typeof(AuthorizeCustomerFilter))
    {
        IgnoreFilter = ignore;
        Arguments = new object[] { ignore };
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets a value indicating whether to ignore the execution of filter actions
    /// </summary>
    public bool IgnoreFilter { get; }

    #endregion

    #region Nested filter

    /// <summary>
    /// Represents a filter that confirms that the user is authorized (JWT)
    /// </summary>
    private class AuthorizeCustomerFilter : IAsyncAuthorizationFilter
    {
        #region Fields

        private readonly bool _ignoreFilter;

        #endregion

        #region Ctor

        public AuthorizeCustomerFilter(bool ignoreFilter)
        {
            _ignoreFilter = ignoreFilter;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called early in the filter pipeline to confirm request is authorized
        /// </summary>
        /// <param name="context">Authorization filter context</param>
        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {

            if (context.HttpContext.Items[WebApiFrontendDefaults.CustomerKey] is not Customer)
                // not logged in
                context.Result = new JsonResult(new { success= false, message = "Unauthorized", data = string.Empty }) { StatusCode = StatusCodes.Status401Unauthorized };
            return Task.CompletedTask;
        }

        #endregion
    }

    #endregion
}