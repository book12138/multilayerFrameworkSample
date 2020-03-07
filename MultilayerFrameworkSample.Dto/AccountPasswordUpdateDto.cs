using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MultilayerFrameworkSample.Dto
{
    /// <summary>
    /// 账号密码修改
    /// </summary>
    public class AccountPasswordUpdateDto
    {
        /// <summary>
        /// id
        /// </summary>
        [Required]
        public string Id { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [StringLength(10, MinimumLength = 6 , ErrorMessage = "密码长度需为6位到10位")]
        public string Password { get; set; }
    }
}
