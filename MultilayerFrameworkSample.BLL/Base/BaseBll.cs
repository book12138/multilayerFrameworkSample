using MultilayerFrameworkSample.DAL.Interface.Base;
using MultilayerFrameworkSample.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultilayerFrameworkSample.BLL.Base
{
    public class BaseBll<T> where T : Entity
    {
        /// <summary>
        /// 数据层实例
        /// </summary>
        private IBaseDal<T> dal { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="baseDal"></param>
        public BaseBll(IBaseDal<T> baseDal)
            => this.dal = baseDal;

        /// <summary>
        /// 查找
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> Find()
            => this.dal.Find();
        /// <summary>
        /// 查找单个
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public T Find(string id)
            => this.dal.Find(id);
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="creator">添加者</param>
        public void Add(T entity, string creator = "0")
            => this.dal.Add(entity, creator);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">修改后的实体</param>
        /// <param name="mender">修改者</param>
        public void Modify(T entity, string mender = "0")
            => this.Modify(entity, mender);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="mender">删除者</param>
        public void Remove(string id, string mender = "0")
            => this.Remove(id, mender);
    }
}
