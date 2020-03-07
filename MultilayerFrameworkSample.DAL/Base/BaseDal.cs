using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using Dapper;
using MultilayerFrameworkSample.IDAL.Base;
using MultilayerFrameworkSample.Model.Base;
using MultilayerFrameworkSample.Common;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq;
using System.Dynamic;
using Microsoft.Data.SqlClient;

namespace MultilayerFrameworkSample.DAL.Base
{
    /// <summary>
    /// 数据操作类基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseDal<T> where T : Entity
    {
        /// <summary>
        /// 数据库连接对象
        /// </summary>
        protected readonly IDbConnection dbConnection;

        /// <summary>
        /// 构造注入工作单元对象实例和数据库连接对象
        /// </summary>
        /// <param name="unitOfWork">工作单元实例</param>
        /// <param name="dbConnection">数据库连接对象</param>
        public BaseDal(IDbConnection dbConnection)
            => this.dbConnection = dbConnection;

        /// <summary>
        /// 查找
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> Find()
            => this.dbConnection.Query<T>($"SELECT * FROM {nameof(T).ToPlural()}");

        /// <summary>
        /// 查找单个
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public virtual T Find(string id)
            => this.dbConnection.QueryFirstOrDefault<T>($"SELECT * FROM {nameof(T).ToPlural()} WHERE [Id] = @id", id);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="creator">添加者</param>
        /// <returns></returns>
        public virtual bool Add(T entity, string creator = "0")
        {
            bool result = false;
            entity.Creator = creator ?? "0";
            var properties = typeof(T).GetRuntimeProperties();
            string filedsString = string.Join(',', properties.Select(u => $"[{u.Name}]"));
            string values = string.Join(',', properties.Select(u => {
                var temp = u.GetValue(entity);
                if (temp.GetType() == typeof(bool)) // 布尔类型的值
                    return $"'{(Convert.ToBoolean(temp) ? "1" : "0")}'";
                else return $"'{temp}'";
            }));
            string sql = $"INSERT INTO [{typeof(T).GetTypeInfo().Name.ToPlural()}] ({filedsString}) VALUES ({values})";

            this.dbConnection.Open();
            IDbTransaction transaction = this.dbConnection.BeginTransaction();             
            try
            {                
                result = this.dbConnection.Execute(sql,null,transaction) > 0;
                transaction.Commit();                
            }
            catch(Exception e)
            {
                transaction.Rollback();
                throw e;
            }
            finally { this.dbConnection.Close(); }
            return result;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">修改后的实体</param>
        /// <param name="mender">修改者</param>
        /// <returns></returns>
        public virtual bool Modify(T entity, string mender = "0")
        {
            bool result = false;
            entity.Creator = mender ?? "0";
            entity.ModifyTime = DateTime.Now;
            var properties = typeof(T)
                .GetRuntimeProperties()
                .Where(
                    u => 
                        u.Name != nameof(Entity.Id) && 
                        u.Name != nameof(Entity.CreateTime) && 
                        u.Name != nameof(Entity.Creator) &&
                        u.Name != nameof(Entity.Status));
            string set = string.Join(',', properties.Select(u => {
                var temp = u.GetValue(entity);
                if (temp.GetType() == typeof(bool)) // 布尔类型的值
                    return $"[{u.Name}] = '{(Convert.ToBoolean(temp) ? "1" : "0")}'";
                else return $"[{u.Name}] = '{temp}'";
                //return $"[{u.Name}] = '{u.GetValue(entity)}'";
            }));
            string sql = $"UPDATE [{typeof(T).GetTypeInfo().Name.ToPlural()}] SET {set} WHERE [{nameof(Entity.Id)}] = '{entity.Id}'";

            this.dbConnection.Open();
            IDbTransaction transaction = this.dbConnection.BeginTransaction();
            try
            {
                result = this.dbConnection.Execute(sql, null, transaction) > 0;
                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw e;
            }
            finally { this.dbConnection.Close(); }
            return result;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="mender">删除者</param>
        /// <returns></returns>
        public virtual bool Remove(string id, string mender = "0")
        {
            bool result = false;
            string sql = $"UPDATE [{typeof(T).GetTypeInfo().Name.ToPlural()}] SET " +
                $"{nameof(Entity.ModifyTime)} = '{DateTime.Now}'," +
                $"{nameof(Entity.Mender)} = '{mender ?? "0"}'," +
                $"{nameof(Entity.Status)} = '0'" +
                $" WHERE [{nameof(Entity.Id)}] = '{id}'";

            this.dbConnection.Open();
            IDbTransaction transaction = this.dbConnection.BeginTransaction();
            try
            {
                result = this.dbConnection.Execute(sql, null, transaction) > 0;
                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw e;
            }
            finally { this.dbConnection.Close(); }
            return result;
        }
    }
}
