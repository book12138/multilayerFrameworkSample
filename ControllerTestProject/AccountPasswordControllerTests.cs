using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using MultilayerFrameworkSample.BLL;
using MultilayerFrameworkSample.DAL;
using MultilayerFrameworkSample.Model.DbContext;
using MultilayerFrameworkSample.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Xunit;

namespace ControllerTestProject
{
    public class AccountPasswordControllerTests
    {
        /// <summary>
        /// 获取数据库操作对象和连接对象
        /// </summary>
        /// <returns></returns>
        private Tuple<SqlDbContext, IDbConnection> GetDbInfo()
        {
            var options = new DbContextOptionsBuilder<SqlDbContext>()
                .UseSqlServer("server=.;uid=sa;pwd=123456;database=MultilayerFrameworkSampleDb");
            var connection = new SqlConnection("server=.;uid=sa;pwd=123456;database=MultilayerFrameworkSampleDb");
            return new Tuple<SqlDbContext, IDbConnection>(new SqlDbContext(options.Options), connection);
        }

        /// <summary>
        /// 获取控制器实例
        /// </summary>
        /// <returns></returns>
        private AccountPasswordController GetControllerInstance()
        {
            (SqlDbContext context, IDbConnection connection) = this.GetDbInfo();
            var loggerMoq = new Mock<ILogger<AccountPasswordController>>();
            var logger = loggerMoq.Object;
            return new AccountPasswordController(new UserInfoBll(new UserInfoDal(context, connection)), logger);
        }

        [Fact]
        public void AccountPasswordCheckTest()
        {
            var controller = this.GetControllerInstance();
            var result = controller.Get(new MultilayerFrameworkSample.Dto.AccountPasswordDto() { Id = "12@12.com", Password = "123456" });
            result.Should().NotBeNull().And.BeOfType(typeof(JsonResult));
        }

        [Fact]
        public void AccountPasswordUpdateTest()
        {
            var controller = this.GetControllerInstance();
            controller.Put(new MultilayerFrameworkSample.Dto.AccountPasswordUpdateDto() {Id = "9b9e772cc1529268", Password = "121212" });
        }
    }
}
