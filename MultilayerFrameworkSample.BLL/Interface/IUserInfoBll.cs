using MultilayerFrameworkSample.Dto;
using MultilayerFrameworkSample.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultilayerFrameworkSample.BLL.Interface
{
    /// <summary>
    /// 用户业务处理操作类
    /// </summary>
    public interface IUserInfoBll : Base.IBaseBll<UserInfo>
    {
        /// <summary>
        /// 检测账户是否存在
        /// </summary>
        /// <param name="loginDto">账户相关信息</param>
        /// <returns></returns>
        bool CheckAccountExsit(LoginDto loginDto);
        /// <summary>
        /// 注册账户
        /// </summary>
        /// <param name="registerDto">账户相关信息</param>
        /// <returns></returns>
        bool RegisterAccount(RegisterDto registerDto);
        /// <summary>
        /// 检测是否已被注册
        /// </summary>
        /// <param name="email">待检测的目标邮箱</param>
        /// <returns></returns>
        bool HasAlreadyBeenRegistered(string email = "");
        /// <summary>
        /// 检测密码是为吻合
        /// </summary>
        /// <param name="accountPasswordDto">用户密码和id信息</param>
        /// <returns></returns>
        bool CompareAccountPassword(AccountPasswordDto accountPasswordDto);
        /// <summary>
        /// 修改账户密码
        /// </summary>
        /// <param name="accountPasswordUpdateDto">账户id与新密码</param>
        /// <returns></returns>
        bool UpdateAccountPassword(AccountPasswordUpdateDto accountPasswordUpdateDto);
    }
}
