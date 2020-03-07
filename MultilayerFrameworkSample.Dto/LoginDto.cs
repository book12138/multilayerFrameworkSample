using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MultilayerFrameworkSample.Dto
{
    /// <summary>
    /// 登录请求数据传输模型
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// 邮箱
        /// </summary>
        [Required]
        public string EMail { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
