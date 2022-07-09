using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApiKalum_net_2022.Utilities{
    public class ActionFilter : IActionFilter
    {
        public ActionFilter(ILogger<ActionFilter> _Logger){
            this.Logger = _Logger;
        }
        
        public readonly ILogger<ActionFilter> Logger;

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Logger.LogInformation("Esto se ejecuta antes de la acción");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Logger.LogInformation("Esto se ejecuta despues de la acción realizada");
        }
    }
}