using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MultilayerFrameworkSample.Dto
{
    /// <summary>
    /// 账户注册相关信息
    /// </summary>
    public class RegisterDto
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
