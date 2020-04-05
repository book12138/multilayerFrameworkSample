using FluentAssertions;
using Microsoft.Data.SqlClient;
using Moq;
using MultilayerFrameworkSample.BLL;
using MultilayerFrameworkSample.BLL.Interface;
using MultilayerFrameworkSample.DAL;
using MultilayerFrameworkSample.DAL.Interface;
using System;
using System.Data;
using Xunit;

namespace BllTestProject
{
    public class UnitTest1
    {
        [Fact]
        public void RegisterTest()
        {
            //var conn = new Mock<IDbConnection>();            
            var dal = new Mock<IUserInfoDal>();  //new UserInfoDal(conn.Object);            
            IUserInfoBll bll = new UserInfoBll(dal.Object);
            var result = bll.RegisterAccount(new MultilayerFrameworkSample.Dto.RegisterDto() { EMail = "21@21.com", Password = "123456" });
            result.Should().BeTrue();
        }

        [Fact]
        public void ChangePasswordTest()
        {
            var dal = new Mock<IUserInfoDal>();  //new UserInfoDal(conn.Object);
            var param = new MultilayerFrameworkSample.Dto.AccountPasswordUpdateDto() { Id = "1b3f7f3b35356904", Password = "121212" };
            dal.Setup(u => u.UpdateAccountPassword(param)).Returns(true);
            IUserInfoBll bll = new UserInfoBll(dal.Object);
            var result = bll.UpdateAccountPassword(param);
            result.Should().BeTrue();
        }

        [Fact]
        public void CheckPasswordTest()
        {
            var param = new MultilayerFrameworkSample.Dto.AccountPasswordDto() { Id = "1b3f7f3b35356904", Password = "123456" };
            IUserInfoBll bll = new UserInfoBll(new UserInfoDal(new SqlConnection("server=.;uid=sa;pwd=123456;database=MultilayerFrameworkSampleDb")));
            var result = bll.CompareAccountPassword(param);
            result.Should().BeTrue();
        }

        [Fact]
        public void CheckAccountTest()
        {
            IUserInfoBll bll = new UserInfoBll(new UserInfoDal(new SqlConnection("server=.;uid=sa;pwd=123456;database=MultilayerFrameworkSampleDb")));
            var result = bll.CheckAccountExsit(new MultilayerFrameworkSample.Dto.LoginDto() { EMail = "12345@12.com" , Password  ="123456"});
            result.Should().BeTrue();
        }
    }
}
