using MultilayerFrameworkSample.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultilayerFrameworkSample.IBLL.Base
{
    public interface IBaseBll<T> where T : Entity
    {
        /// <summary>
        /// 查找
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> Find();
        /// <summary>
        /// 查找单个
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        T Find(string id);
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="creator">添加者</param>
        void Add(T entity, string creator = "0");
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">修改后的实体</param>
        /// <param name="mender">修改者</param>
        void Modify(T entity, string mender = "0");
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="mender">删除者</param>
        void Remove(string id, string mender = "0");
    }
}
