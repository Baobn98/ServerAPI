using Microsoft.AspNetCore.Mvc.Filters;
using ServerAPI.Attribute;
using ServerAPI.Attributes;
using ServerAPI.Models.Context;
using System.Diagnostics.CodeAnalysis;

namespace ServerAPI.Filter
{
    public class AuthorizeActionFilter : IAsyncActionFilter
    {
        private readonly AppDbContext _appDbContext;

        public AuthorizeActionFilter(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var globalApi = context.ActionDescriptor.FilterDescriptors.FirstOrDefault(x => x.Filter is GlobalApiAttribute);
            if (globalApi != null)
            {
                next();
                return;
            }
            var adminApi = context.ActionDescriptor.FilterDescriptors.FirstOrDefault(x => x.Filter is AdminApiAttribute);
            if (adminApi != null)
            {
                next();
                return;
            }
            var userApi = context.ActionDescriptor.FilterDescriptors.FirstOrDefault(x=> x.Filter is UserApiAttribute);
            if (userApi != null)
            {
                next();
                return;
            }
            return;
        }
    }
}
