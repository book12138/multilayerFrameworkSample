using FluentAssertions;
using Microsoft.Data.SqlClient;
using MultilayerFrameworkSample.DAL;
using MultilayerFrameworkSample.DAL.Base;
using MultilayerFrameworkSample.IDAL;
using MultilayerFrameworkSample.IDAL.Base;
using MultilayerFrameworkSample.Model.Base;
using MultilayerFrameworkSample.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DalTestProject.dalApiTest
{
    public class TestUserInfoAdd
    {
        //private readonly IUserInfoDal userInfoDal;
        //public TestUserInfoAdd() { }
        //public TestUserInfoAdd(IUserInfoDal userInfoDal)
        //    => this.userInfoDal = userInfoDal;

        [Fact]
        public void Add()
        {
            IUserInfoDal userInfoDal = new UserInfoDal( new SqlConnection("server=.;uid=sa;pwd=123456;database=MultilayerFrameworkSampleDb"));
            var result = userInfoDal.RegisterAccount(new MultilayerFrameworkSample.Dto.RegisterDto { EMail = "12345@12.com", Password = "123456" });
            result.Should().BeTrue();
        }
        
        [Fact]
        public void Update()
        {
            IUserInfoDal userInfoDal = new UserInfoDal(new SqlConnection("server=.;uid=sa;pwd=123456;database=MultilayerFrameworkSampleDb"));
            var result = userInfoDal.Modify(new UserInfo { Id = "53cd0b6a63336783", EMail = "23@12.com", Password = "123456" });
            result.Should().BeTrue();
        }

        [Fact]
        public void Remove()
        {
            IUserInfoDal userInfoDal = new UserInfoDal(new SqlConnection("server=.;uid=sa;pwd=123456;database=MultilayerFrameworkSampleDb"));
            var result = userInfoDal.Remove("53cd0b6a63336783");
            result.Should().BeTrue();
        }
    }
}
