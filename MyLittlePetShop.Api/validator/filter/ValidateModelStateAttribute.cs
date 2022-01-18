using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyLittlePetShop.Api.Models.error;

namespace MyLittlePetShop.Api.validator.filter
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(new ErrorFormat()
                {
                    Error = "Bad Request",
                    Status = 400,
                    Message = context.ModelState.Values.SelectMany(x => x.Errors)
                                .Select(x => x.ErrorMessage)
                });
            }
        }
    }
}
