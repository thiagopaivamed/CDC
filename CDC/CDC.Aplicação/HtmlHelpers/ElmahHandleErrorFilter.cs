using Elmah;
using System.Web.Mvc;

namespace CDC.Aplicação.HtmlHelpers
{
    public class ElmahHandleErrorFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.ExceptionHandled)
                ErrorSignal.FromCurrentContext().Raise(context.Exception);
        }
    }
}