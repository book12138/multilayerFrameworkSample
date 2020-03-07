using System;
using System.Collections.Generic;
using System.Text;

namespace MultilayerFrameworkSample.Common.ExceptionExtend
{
    /// <summary>
    /// 关于用户信息操作的错误异常
    /// </summary>
    public class UserInfoOperationException : Exception , IException
    {
        public UserInfoOperationException() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">错误信息</param>
        public UserInfoOperationException(string? message)
            : base(message)
        { }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="innerException"></param>
        public UserInfoOperationException(string? message, Exception? innerException)
            :base(message,innerException)
        { }
    }
}
