using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using MultilayerFrameworkSample.Common.ExceptionExtend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultilayerFrameworkSample.WebApi.Filter
{
    public class GlobalException : IExceptionFilter
    {
        private readonly ILogger<GlobalException> logger;

        public GlobalException(ILogger<GlobalException> logger)
        {
            this.logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if(typeof(IException).IsAssignableFrom(context.Exception.GetType()))
            {
                /* 发生的错误错误类型是自定义的，即已知的错误 */
                context.Result = new BadRequestObjectResult(new { message = context.Exception.Message });
                this.logger.LogError(context.Exception.Message);
            }
            else /* 未知的错误 */
            {
                context.Result = new BadRequestObjectResult(new { message = "发生了未知的内部错误" });
                this.logger.LogError(context.Exception,"发生了未知的内部错误");
            }
            context.ExceptionHandled = true;
        }
    }
}
