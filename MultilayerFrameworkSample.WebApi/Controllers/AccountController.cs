using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MultilayerFrameworkSample.Common.ExceptionExtend;
using MultilayerFrameworkSample.Dto;
using MultilayerFrameworkSample.IBLL;
using MultilayerFrameworkSample.WebApi.Model;

namespace MultilayerFrameworkSample.WebApi.Controllers
{
    /// <summary>
    /// 账户登录与注册服务
    /// </summary>
    [Route("v1/[controller]")]
    [ApiController]
    [EnableCors(PolicyName = "_myAllowSpecificOrigins")]
    public class AccountController : ControllerBase
    {
        /// <summary>
        /// 用户业务操作对象
        /// </summary>
        private readonly IUserInfoBll userInfoBll;
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// 构造注入
        /// </summary>
        /// <param name="userInfoBll"></param>
        /// <param name="logger"></param>
        public AccountController(
            IUserInfoBll userInfoBll,
            ILogger<AccountController> logger
            )
        {
            this.userInfoBll = userInfoBll;
            this.logger = logger;
        }

        /// <summary>
        /// 用户登录，获取token
        /// </summary>        
        /// <param name="loginDto">登录账户信息</param>
        /// <returns></returns>
        [HttpPost("Login")]
        public ActionResult<LoginResult> Login([FromBody] LoginDto loginDto)
        {            
            bool result = this.userInfoBll.CheckAccountExsit(loginDto);
            if (result)
                return new LoginResult { IsSuccess = true , Token = "1212121212" , Message = "登录成功"};
            else
                return new LoginResult { IsSuccess = false, Token = "", Message = "用户名或密码错误" };
        }            

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="registerDto">待注册账户信息</param>
        /// <returns></returns>
        [HttpPost("Register")]
        public RegisterResult Register([FromBody] RegisterDto registerDto)
        {
            if (this.userInfoBll.HasAlreadyBeenRegistered(registerDto.EMail))
                return new RegisterResult { IsSuccess = false, Token = "", Message = "该邮箱已被注册" };
            bool result = this.userInfoBll.RegisterAccount(registerDto);
            if (result)
                return new RegisterResult { IsSuccess = true, Token = "12121212122", Message = "注册成功" };
            else
                throw new UserInfoOperationException("注册失败");
        }

        /// <summary>
        /// 密码比对
        /// </summary>
        /// <returns></returns>
        [HttpGet("Password/Compare/{id}")]
        public PasswordCompareResult Compare(string id, [FromQuery] string password)
            => new PasswordCompareResult { IsTure = this.userInfoBll.CompareAccountPassword(new AccountPasswordDto { Id  =id , Password = password}) };

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="accountPasswordUpdateDto">密码修改相关信息</param>
        /// <returns></returns>
        [HttpPut("Password/Update")]
        public void Update([FromBody] AccountPasswordUpdateDto accountPasswordUpdateDto)
        {
            if (!this.userInfoBll.UpdateAccountPassword(accountPasswordUpdateDto))
                throw new UserInfoOperationException("密码修改失败");
        }
    }
}