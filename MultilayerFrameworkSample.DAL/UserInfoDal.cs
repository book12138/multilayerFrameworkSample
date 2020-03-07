using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using Microsoft.VisualBasic;
using MultilayerFrameworkSample.Common;
using MultilayerFrameworkSample.Common.ValueMapping;
using MultilayerFrameworkSample.Dto;
using MultilayerFrameworkSample.IDAL;
using MultilayerFrameworkSample.Model.Entities;

namespace MultilayerFrameworkSample.DAL
{
    /// <summary>
    /// 用户信息数据操作类
    /// </summary>
    public class UserInfoDal : Base.BaseDal<UserInfo> , IUserInfoDal
    {
        /// <summary>
        /// 构造注入EF 上下文，与数据库连接对象
        /// </summary>
        /// <param name="unitOfWork"></param>
        public UserInfoDal(IDbConnection dbConnection)
            : base(dbConnection)
        { }

        /// <summary>
        /// 检测账户是否存在
        /// </summary>
        /// <param name="loginDto">账户相关信息</param>
        /// <returns></returns>
        public bool CheckAccountExsit(LoginDto loginDto)         
            => base.dbConnection.QueryFirstOrDefault<int>($"SELECT COUNT(*) FROM [{nameof(UserInfo).ToPlural()}] WHERE [{nameof(UserInfo.EMail)}] = '{loginDto.EMail}' AND [{nameof(UserInfo.Password)}] = '{MD5encryption.UserMd5($"happy_{loginDto.Password}")}'") > 0;
         

        /// <summary>
        /// 注册账户
        /// </summary>
        /// <param name="registerDto">账户相关信息</param>
        /// <returns></returns>
        public bool RegisterAccount(RegisterDto registerDto)
        {
            UserInfo userInfo = new UserInfo() { Id = new CombineIdHelper().CreateId() };
            registerDto.ValueMapping(userInfo);
            userInfo.Password = MD5encryption.UserMd5($"happy_{userInfo.Password}");// 密码加密
            
            return base.Add(userInfo);
        }

        /// <summary>
        /// 检测是否已被注册
        /// </summary>
        /// <param name="email">待检测的目标邮箱</param>
        /// <returns></returns>
        public bool HasAlreadyBeenRegistered(string email = "")
            => base.dbConnection.QueryFirstOrDefault<int>($"SELECT COUNT(*) FROM [{nameof(UserInfo).ToPlural()}] WHERE [{nameof(UserInfo.EMail)}] = '{email}'") > 0;

        /// <summary>
        /// 检测密码是为吻合
        /// </summary>
        /// <param name="accountPasswordDto">用户密码和id信息</param>
        /// <returns></returns>
        public bool CompareAccountPassword(AccountPasswordDto accountPasswordDto)
            => base.dbConnection.QueryFirstOrDefault<int>($"SELECT COUNT(*) FROM [{nameof(UserInfo).ToPlural()}] WHERE [{nameof(UserInfo.Id)}] = '{accountPasswordDto.Id}' AND [{nameof(UserInfo.Password)}] = '{MD5encryption.UserMd5($"happy_{accountPasswordDto.Password}")}'") > 0;

        /// <summary>
        /// 修改账户密码
        /// </summary>
        /// <param name="accountPasswordUpdateDto">账户id与新密码</param>
        /// <returns></returns>
        public bool UpdateAccountPassword(AccountPasswordUpdateDto accountPasswordUpdateDto)
        {
            bool result = false;
            base.dbConnection.Open();
            IDbTransaction transaction = base.dbConnection.BeginTransaction();
            try
            {
                result = base.dbConnection.Execute(
                    $"UPDATE [{nameof(UserInfo).ToPlural()}] SET [{nameof(UserInfo.Password)}] = '{MD5encryption.UserMd5($"happy_{accountPasswordUpdateDto.Password}")}' WHERE [{nameof(UserInfo.Id)}] = '{accountPasswordUpdateDto.Id}'",
                    null,
                    transaction
                    ) > 0;
                transaction.Commit();                
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw e;
            }
            finally { base.dbConnection.Close(); }
            return result;
        }
    }
}
