using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using MultilayerFrameworkSample.BLL;
using MultilayerFrameworkSample.DAL;
using MultilayerFrameworkSample.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Xunit;

namespace ControllerTestProject
{
    public class AccountControllerTests
    {
        /// <summary>
        /// 获取控制器实例
        /// </summary>
        /// <returns></returns>
        private AccountController GetControllerInstance()
        {
            IDbConnection connection = new SqlConnection("server=.;uid=sa;pwd=123456;database=MultilayerFrameworkSampleDb");
            var loggerMoq = new Mock<ILogger<AccountController>>();
            var logger = loggerMoq.Object;
            return new AccountController(new UserInfoBll(new UserInfoDal(context, connection)), logger);
        }

        [Fact]
        public void AccountLoginTest()
        {
            var controller = this.GetControllerInstance();
            var result = controller.Get(new MultilayerFrameworkSample.Dto.LoginDto() { EMail = "12@12.com", Password = "234567" });
            result.Should().NotBeNull().And.BeOfType(typeof(JsonResult));
        }

        /// <summary>
        /// 对账户注册进行测试
        /// </summary>
        [Fact]
        public void AccountRegisterTest()
        {
            var controller = this.GetControllerInstance();
            var result = controller.Post(new MultilayerFrameworkSample.Dto.RegisterDto() { EMail = "as@as.com", Password = "sasasa" });
            result.Should().NotBeNull().And.BeOfType(typeof(JsonResult));
        }
    }
}
