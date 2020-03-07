using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MultilayerFrameworkSample.Model.Entities
{
    /// <summary>
    /// 用户订单
    /// </summary>
    public class Order : Base.Entity
    {
        /// <summary>
        /// 用户id
        /// </summary>
        [Required]
        public string UserId { get; set; }
    }
}
