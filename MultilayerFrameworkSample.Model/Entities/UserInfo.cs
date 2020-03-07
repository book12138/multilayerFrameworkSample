using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;

namespace MultilayerFrameworkSample.Model.Entities
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserInfo : Base.Entity
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
