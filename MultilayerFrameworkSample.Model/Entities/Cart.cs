using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MultilayerFrameworkSample.Model.Entities
{
    /// <summary>
    /// 购物车
    /// </summary>
    public class Cart : Base.Entity
    {
        /// <summary>
        /// 用户id
        /// </summary>
        [Required]
        public string UserId { get; set; }        
    }
}
