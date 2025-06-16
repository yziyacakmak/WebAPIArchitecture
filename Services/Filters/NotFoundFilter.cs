using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using App.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace App.Services.Filters
{
    public class NotFoundFilter<T, TId>(IGenericRepository<T, TId> genericRepository) : Attribute, IAsyncActionFilter where T : class where TId : struct
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
           _ = context.ActionArguments.TryGetValue("id", out var idValue);

            if (idValue is not TId id)
            {
                await next();
                return;
            }

            var anyEntity = await genericRepository.AnyAsync(id);

            if (anyEntity)
            {
                await next();
                return;
            }


            var entityName = typeof(T).Name;

            var actionName = context.ActionDescriptor.RouteValues["action"];

            var result = ServiceResult.Fail($"{actionName}  {entityName} with id {id} not found.",HttpStatusCode.NotFound);
            context.Result = new NotFoundObjectResult(result);
            return;



        }
    }
}
