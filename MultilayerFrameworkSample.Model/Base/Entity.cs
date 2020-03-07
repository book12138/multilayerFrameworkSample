using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MultilayerFrameworkSample.Model.Base
{
    /// <summary>
    /// 所有实体的父类
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// 主键id
        /// </summary>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// 创建时间（默认值为当前时间）
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 修改时间（默认值为当前时间）
        /// </summary>
        public DateTime ModifyTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 创建者（默认值为0，表示系统）
        /// </summary>
        public string Creator { get; set; } = "0";
        /// <summary>
        /// 修改者（默认值为0，表示系统）
        /// </summary>
        public string Mender { get; set; } = "0";
        /// <summary>
        /// 记录是否有效（默认值为true，因为数据都为软删除，所有用该字段来表示记录是否被删除）
        /// </summary>
        public bool Status { get; set; } = true;
    }
}
