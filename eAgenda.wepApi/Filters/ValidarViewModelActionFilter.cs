using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace eAgenda.wepApi.Filters
{
    public class ValidarViewModelActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

            if (context.ModelState.IsValid == false)
            {
                context.Result = new BadRequestObjectResult(new
                {
                    sucesso = false,
                    erros = context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList()
                });

                return;
            }
        }
    }
}
