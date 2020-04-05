using System;
using System.Collections.Generic;
using System.Text;
using MultilayerFrameworkSample.Model.Base;

namespace MultilayerFrameworkSample.DAL.Interface.Base
{
    /// <summary>
    /// 数据操作基接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseDal<T> where T : Entity
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
        bool Add(T entity,string creator = "0");
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">修改后的实体</param>
        /// <param name="mender">修改者</param>
        bool Modify(T entity, string mender = "0");
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="mender">删除者</param>
        bool Remove(string id,string mender = "0");
    }
}
