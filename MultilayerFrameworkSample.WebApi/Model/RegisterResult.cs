using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultilayerFrameworkSample.WebApi.Model
{
    /// <summary>
    /// 账户注册结果
    /// </summary>
    public class RegisterResult
    {
        /// <summary>
        /// 结果，是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// 注册成功后可以获得的token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 登录过程中的消息
        /// </summary>
        public string Message { get; set; }
    }
}
