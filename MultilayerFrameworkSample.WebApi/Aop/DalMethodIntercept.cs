using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MultilayerFrameworkSample.Model.Base;

namespace MultilayerFrameworkSample.WebApi.Aop
{
    /// <summary>
    /// dal层方法拦截
    /// </summary>
    public class DalMethodIntercept : IInterceptor
    {
        /// <summary>
        /// 拦截dal层的所有方法，针对参数进行验证
        /// </summary>
        /// <param name="invocation"></param>
        public void Intercept(IInvocation invocation)
        {
            var argus = invocation.Arguments;//拿到所有参数

            /*  */
            for (int i = 0; i < argus.Length; i++)
            {
                Type type = argus[i].GetType();
                if (typeof(Entity).IsAssignableFrom(type))//检查该类是否继承自Entity
                {
                    /* 如果是，则进行实体验证 */
                    //invocation.TargetType
                }
            }

            invocation.Proceed();//继续执行
        }
    }
}
