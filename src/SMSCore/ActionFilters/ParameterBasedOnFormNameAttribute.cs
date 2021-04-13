using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace SMSCore.ActionFilters
{
    public class ParameterBasedOnFormNameAttribute : ActionFilterAttribute
    {
        #region Fields

        private readonly string _name;
        private readonly string _actionParameterName;

        #endregion

        #region Constructor

        public ParameterBasedOnFormNameAttribute(string name, string actionParameterName)
        {
            _name = name;
            _actionParameterName = actionParameterName;
        }

        #endregion

        #region Methods

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.ActionArguments[_actionParameterName] = context.HttpContext
                .Request.Form.Any(x => x.Key.Equals(_name));
            base.OnActionExecuting(context);
        }

        #endregion
    }
}
