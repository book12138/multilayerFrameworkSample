using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MultilayerFrameworkSample.Dto
{
    /// <summary>
    /// 账户密码和id
    /// </summary>
    public class AccountPasswordDto
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
        public string Password { get; set; }
    }
}
