using MultilayerFrameworkSample.BLL.Base;
using MultilayerFrameworkSample.Dto;
using MultilayerFrameworkSample.IBLL;
using MultilayerFrameworkSample.IDAL;
using MultilayerFrameworkSample.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultilayerFrameworkSample.BLL
{
    /// <summary>
    /// 用户业务处理操作类
    /// </summary>
    public class UserInfoBll : BaseBll<UserInfo> , IUserInfoBll
    {
        private readonly IUserInfoDal userInfoDal;

        /// <summary>
        /// 构造注入数据操作类对象
        /// </summary>
        /// <param name="userInfoDal">用户数据操作类</param>
        public UserInfoBll(IUserInfoDal userInfoDal)
            :base(userInfoDal)
        {
            this.userInfoDal = userInfoDal;
        }

        /// <summary>
        /// 检测账户是否存在
        /// </summary>
        /// <param name="loginDto">账户相关信息</param>
        /// <returns></returns>
        public bool CheckAccountExsit(LoginDto loginDto)
            => this.userInfoDal.CheckAccountExsit(loginDto);
        /// <summary>
        /// 注册账户
        /// </summary>
        /// <param name="registerDto">账户相关信息</param>
        /// <returns></returns>
        public bool RegisterAccount(RegisterDto registerDto)
        {
            this.userInfoDal.RegisterAccount(registerDto);
            return true;
        }
        /// <summary>
        /// 检测是否已被注册
        /// </summary>
        /// <param name="email">待检测的目标邮箱</param>
        /// <returns></returns>
        public bool HasAlreadyBeenRegistered(string email = "")
            => this.userInfoDal.HasAlreadyBeenRegistered(email);
        /// <summary>
        /// 检测密码是为吻合
        /// </summary>
        /// <param name="accountPasswordDto">用户密码和id信息</param>
        /// <returns></returns>
        public bool CompareAccountPassword(AccountPasswordDto accountPasswordDto)
            => this.userInfoDal.CompareAccountPassword(accountPasswordDto);
        /// <summary>
        /// 修改账户密码
        /// </summary>
        /// <param name="accountPasswordUpdateDto">账户id与新密码</param>
        /// <returns></returns>
        public bool UpdateAccountPassword(AccountPasswordUpdateDto accountPasswordUpdateDto)
            => this.userInfoDal.UpdateAccountPassword(accountPasswordUpdateDto);
    }
}
